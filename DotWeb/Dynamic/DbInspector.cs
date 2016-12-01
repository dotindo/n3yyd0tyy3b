using DotWeb.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace DotWeb
{
    /// <summary>
    /// <para>This class is the inspector engine; its main task is to inspect metadata of a database: tables, columns,
    /// primary keys, and foreign keys. The output of this inspection process is metadata which is stored in
    /// dotwebdb database.</para>
    /// <para>The engine can be run from a dedicated console app, or included in the web application itself
    /// to perform inspection on-demand when application start. The first method is preferable, but the second method is the quickest
    /// way to see the end result.</para>
    /// </summary>
    public class DbInspector
    {
        private DotWebDb dbConfig;
        private string dbName = "";
        private SchemaInfo schemaInfo;
        private int appId = 0;
        private List<PKInfo> pkInfo;
        private List<FKInfo> fkInfo;

        /// <summary>
        /// Default constructor, initiating schemaInfo object.
        /// </summary>
        public DbInspector()
        {
             schemaInfo = new SchemaInfo();
        }

        /// <summary>
        /// Inspection results are stored transiently to this property before calling next method to save it to the database.
        /// </summary>
        public SchemaInfo SchemaInfo
        {
            get { return schemaInfo; }
        }

        /// <summary>
        /// Ensure that appId key presents in the configuration file. Every web application must has corresponding appId.
        /// </summary>
        /// <param name="context">An instance of <see cref="DotWebDb"/>.</param>
        private void EnsureApp(DotWebDb context)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["appId"]))
                throw new ArgumentException("appId must be specified in config.");
            appId = int.Parse(ConfigurationManager.AppSettings["appId"].ToString());
            var app = context.Apps.SingleOrDefault(a => a.Id == appId);
            if (app == null)
            {
                app = new App();
                // App is initiated with a default value
                var appName = "Sample App " + DateTime.Today.ToShortDateString();
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["appName"]))
                    // If appName presents in configuration file, use it
                    appName = ConfigurationManager.AppSettings["appName"];

                app.Id = appId;
                app.Name = appName;
                app.Description = "This app was automatically generated from DbInspector, please change the name and description appropriately.";
                context.Apps.Add(app);
                context.SaveChanges();
            }
            schemaInfo.App = app;
        }

        /// <summary>
        /// The main process of this class => inspecting database metadata.
        /// </summary>
        /// <param name="connectionStringName">Connection string name stored in configuration file.</param>
        public void Inspect(string connectionStringName)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["appId"]))
                throw new ArgumentException("appId must be specified in config.");
            appId = int.Parse(ConfigurationManager.AppSettings["appId"].ToString());
            pkInfo = SqlHelper.GetPrimaryKeySchemaInfo(connectionStringName);
            fkInfo = SqlHelper.GetForeignKeySchemaInfo(connectionStringName);

            using (var connection = SqlHelper.OpenConnection(connectionStringName))
            {
                dbName = connection.Database;
                InspectTables(connection);
                foreach (var tableMeta in schemaInfo.Tables)
                {
                    InspectColumns(connection, tableMeta);
                    InspectPrimaryKeys(connection, tableMeta);
                    InspectForeignKeys(connection, tableMeta);
                }
            }
        }

        /// <summary>
        /// The first step of the inspection: collect metadata about tables.
        /// </summary>
        /// <param name="connection">Existing database connection.</param>
        private void InspectTables(DbConnection connection)
        {
            var tables = connection.GetSchema("Tables", new string[] { dbName, null, null, "BASE TABLE" });
            foreach (DataRow row in tables.Rows)
            {
                // Exclude entity framework generated__MigrationHistory table
                if (row["TABLE_NAME"].ToString() != "__MigrationHistory")
                {
                    schemaInfo.Tables.Add(new TableMeta()
                    {
                        Name = row["TABLE_NAME"].ToString(),
                        Caption = row["TABLE_NAME"].ToString().ToTitleCase(),
                        SchemaName = row["TABLE_SCHEMA"].ToString(),
                        AppId = appId
                    });
                }
            }
        }

        /// <summary>
        /// Second step of the inspection: collect metadata about columns.
        /// </summary>
        /// <param name="connection">Existing database connection.</param>
        /// <param name="tableMeta">The <see cref="TableMeta"/> in which this column belongs to.</param>
        private void InspectColumns(DbConnection connection, TableMeta tableMeta)
        {
            var columns = connection.GetSchema("Columns", new[] { dbName, null, tableMeta.Name });
            foreach (DataRow row in columns.Rows)
            {
                var columnMeta = new ColumnMeta() { Table = tableMeta };
                columnMeta.Name = row["COLUMN_NAME"].ToString();
                columnMeta.Caption = columnMeta.Name.ToTitleCase();
                columnMeta.DataType = MapDbTypeToClrType(row["DATA_TYPE"].ToString());

                columnMeta.IsRequired = row["IS_NULLABLE"].ToString() == "NO";
                int maxlength = 0;
                int.TryParse(row["CHARACTER_MAXIMUM_LENGTH"].ToString(), out maxlength);
                if (maxlength > 0)
                    columnMeta.MaxLength = maxlength;
                columnMeta.OrderNo = int.Parse(row["ORDINAL_POSITION"].ToString());

                tableMeta.Columns.Add(columnMeta);
            }
        }

        /// <summary>
        /// Inspect primary keys.
        /// </summary>
        /// <param name="connection">Existing database connection.</param>
        /// <param name="tableMeta">The <see cref="TableMeta"/> in which this column belongs to.</param>
        private void InspectPrimaryKeys(DbConnection connection, TableMeta tableMeta)
        {
            var pkColumns = pkInfo.Where(i => i.TableName.Equals(tableMeta.Name, StringComparison.InvariantCultureIgnoreCase) &&
                i.SchemaName.Equals(tableMeta.SchemaName, StringComparison.InvariantCultureIgnoreCase));
            foreach (var pkColumn in pkColumns)
            {
                var columnMeta = tableMeta.Columns.Single(c => c.Name.Equals(pkColumn.ColumnName, StringComparison.InvariantCultureIgnoreCase));
                if (columnMeta != null)
                {
                    columnMeta.IsPrimaryKey = true;
                    columnMeta.IsIdentity = pkColumn.IsIdentity;
                }
            }
        }

        /// <summary>
        /// Inspect foreign keys.
        /// </summary>
        /// <param name="connection">Existing database connection.</param>
        /// <param name="tableMeta">The <see cref="TableMeta"/> in which this column belongs to.</param>
        private void InspectForeignKeys(DbConnection connection, TableMeta tableMeta)
        {
            var foreignKeys = connection.GetSchema("ForeignKeys", new[] { dbName, null, tableMeta.Name });
            foreach (DataRow row in foreignKeys.Rows)
            {
                var constraintName = row[foreignKeys.Columns["CONSTRAINT_NAME"]].ToString();
                if (constraintName.Substring(0, 3) == "FK_")
                {
                    var fki = fkInfo.Single(fk => fk.ConstraintName == constraintName);
                    var foreignKey = fki.ColumnName;
                    var tableName = fki.TableName;
                    var refTableName = fki.RefTableName;
                    var refColName = fki.RefColumnName;

                    var foreignKeyColumn = tableMeta.Columns.SingleOrDefault(c => c.Name == foreignKey);
                    if (foreignKeyColumn != null)
                    {
                        foreignKeyColumn.IsForeignKey = true;
                        foreignKeyColumn.ReferenceTable = schemaInfo.Tables.SingleOrDefault(t => t.Name == refTableName);
                        string fkName = foreignKey.ToTitleCase().Replace(refColName, "").TrimEnd();
                        var relationName = string.Concat(tableMeta.Name, "_", foreignKeyColumn.ReferenceTable.Name, "_", fkName);
                        var relationCaption = string.Concat(tableName.ToTitleCase(), " - ", fkName);
                        foreignKeyColumn.ReferenceTable.Children.Add(
                            new TableMetaRelation() 
                            { 
                                Parent = foreignKeyColumn.ReferenceTable, 
                                Child = tableMeta,
                                ForeignKeyName = foreignKeyColumn.Name,
                                Name = relationName,
                                Caption = relationCaption.TrimEnd() 
                            }
                        );
                    }
                }
            }
        }

        /// <summary>
        /// This is to generate menu items and groups for navigation menu purpose.
        /// </summary>
        /// <param name="context">An instance of <see cref="DotWebDb"/>.</param>
        private void GenerateNavigationModules(DotWebDb context)
        {
            foreach (var tableMeta in schemaInfo.Tables)
            {
                string groupName = tableMeta.SchemaName == "dbo" ? schemaInfo.App.DefaultGroupName : tableMeta.SchemaName;
                var group = schemaInfo.App.Groups.SingleOrDefault(
                    g => g.AppId == appId && g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase));
                if (group == null)
                {
                    group = new ModuleGroup()
                    {
                        AppId = appId,
                        Name = groupName,
                        Title = groupName,
                        OrderNo = schemaInfo.App.Groups.Count == 0 ? 1 : schemaInfo.App.Groups.Count + 1,
                    };
                    schemaInfo.App.Groups.Add(group);
                }

                var dbModule = context.Modules.Include(m => m.Group).SingleOrDefault(m => m.TableName.Equals(tableMeta.Name, StringComparison.InvariantCultureIgnoreCase)
                    && m.Group.AppId == appId);
                
                if (dbModule == null)
                    group.Modules.Add(new Module()
                    {
                        TableName = tableMeta.Name,
                        Title = tableMeta.Name.ToTitleCase(),
                        OrderNo = group.Modules.Count == 0 ? 1 : group.Modules.Count + 1,
                        ModuleType = ModuleType.AutoGenerated
                    });
            }
        }

        /// <summary>
        /// Look up display column can be determined manually in DotWeb admin interface. But, upon auto-generation
        /// it will be deducted from column which name is Name or Title. It thera are no such column names, the 
        /// the first column will be used instead.
        /// </summary>
        /// <param name="context"></param>
        private void DetermineColumnForLookUpDisplay(DotWebDb context)
        {
            var tables = context.Tables.Include(t => t.Columns).Where(t => t.AppId == appId && t.LookUpDisplayColumnId == null);
            foreach (var table in tables)
            {
                // Determines lookup display column
                var lookUpColumns = table.Columns.Where(c => c.Name.Equals("Name", StringComparison.InvariantCultureIgnoreCase)
                    || c.Name.Equals("Title", StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (lookUpColumns.Count > 0)
                    table.LookUpDisplayColumnId = lookUpColumns[0].Id;
                else
                {
                    lookUpColumns = table.Columns.Where(c => c.DataType == TypeCode.String).ToList();
                    if (lookUpColumns.Count > 0)
                        table.LookUpDisplayColumnId = lookUpColumns[0].Id;
                    else
                        table.LookUpDisplayColumnId = table.Columns[0].Id;
                }
            }

        }

        /// <summary>
        /// Map database (SQL Server) type to CLR type.
        /// </summary>
        /// <param name="p">String representing database type.</param>
        /// <returns>TypeCode, as the mapping result.</returns>
        private TypeCode MapDbTypeToClrType(string p)
        {
            switch (p.ToUpper())
            {
                case "NVARCHAR":
                case "VARCHAR":
                case "NCHAR":
                case "CHAR":
                    return TypeCode.String;
                case "BIGINT":
                    return TypeCode.Int64;
                case "INT":
                    return TypeCode.Int32;
                case "FLOAT":
                    return TypeCode.Single;
                case "DECIMAL":
                    return TypeCode.Decimal;
                case "DATETIME":
                    return TypeCode.DateTime;
                case "BIT":
                    return TypeCode.Boolean;
                default:
                    return TypeCode.Object;

            }
        }

        /// <summary>
        /// Save the inspection result to database.
        /// </summary>
        /// <param name="sharedContext">Indicates that dbContext used in the process is the shared one.</param>
        public void SaveToConfig(bool sharedContext = false)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["appId"]))
                throw new ArgumentException("appId must be specified in config.");
            appId = int.Parse(ConfigurationManager.AppSettings["appId"].ToString());
            if (sharedContext && dbConfig == null)
                throw new ArgumentException("Using shared context requires initialization.");

            var context = dbConfig;
            if (!sharedContext)
                context = new DotWebDb();

            EnsureApp(context);

            // Save tables
            foreach (var tableMeta in SchemaInfo.Tables)
            {
                Console.WriteLine("Processing table " + tableMeta.Name);

                TableMeta dbTable = context.Tables
                    .Include(t => t.Columns)
                    .Include(t => t.Columns.Select(c => c.ReferenceTable))
                    .Include(t => t.Children)
                    .Include(t => t.Children.Select(c => c.Parent))
                    .Include(t => t.Children.Select(c => c.Child))
                    .Include(t => t.App)
                    .SingleOrDefault(t => t.Name == tableMeta.Name && t.AppId == appId);

                var newTable = false;
                if (dbTable == null)
                {
                    // Add table
                    dbTable = tableMeta;
                    newTable = true;
                }
                else
                {
                    // Update existing table
                    dbTable.SchemaName = tableMeta.SchemaName;
                }

                foreach (var columnMeta in tableMeta.Columns)
                {
                    Console.WriteLine("    Processing column " + columnMeta.Name + " of table " + tableMeta.Name);
                    ColumnMeta dbColumn = context.Columns.SingleOrDefault(c => c.TableId == dbTable.Id && c.Name == columnMeta.Name);
                    var newColumn = false;
                    if (dbColumn == null)
                    {
                        // Add columns
                        dbColumn = columnMeta;
                        dbColumn.Table = dbTable;
                        newColumn = true;
                    }
                    else
                    {
                        // Update existing column
                        dbColumn.DataType = columnMeta.DataType;
                        dbColumn.IsForeignKey = columnMeta.IsForeignKey;
                        dbColumn.IsIdentity = columnMeta.IsIdentity;
                        dbColumn.IsPrimaryKey = columnMeta.IsPrimaryKey;
                        dbColumn.IsRequired = columnMeta.IsRequired;
                        dbColumn.MaxLength = columnMeta.MaxLength;
                    }

                    if (columnMeta.ReferenceTable != null && (newColumn || dbColumn.ReferenceTable.Name != columnMeta.ReferenceTable.Name))
                    {
                        var dbRefTable = context.Tables.SingleOrDefault(t => tableMeta.AppId == appId
                            && t.Name.Equals(columnMeta.ReferenceTable.Name, StringComparison.InvariantCultureIgnoreCase));
                        if (dbRefTable != null)
                            dbColumn.ReferenceTable = dbRefTable;
                    }

                    if (newColumn && !newTable)
                        dbTable.Columns.Add(dbColumn);
                }

                // removes columns
                var removedColumns = new List<ColumnMeta>();
                foreach (var dbColumn in dbTable.Columns)
                {
                    ColumnMeta column = tableMeta.Columns.SingleOrDefault(c => c.Name.Equals(dbColumn.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (column == null)
                    {
                        Console.WriteLine("    Removing column " + dbColumn.Name + " from table " + tableMeta.Name);
                        // column has been deleted or renamed, so delete related metadata
                        removedColumns.Add(dbColumn);
                    }
                }
                if (removedColumns.Count > 0)
                    context.Columns.RemoveRange(removedColumns);

                // Processing table meta relations
                foreach (var tableMetaRelation in tableMeta.Children)
                {
                    Console.WriteLine("    Processing relation " + tableMetaRelation.Name + " of table " + tableMeta.Name);
                    TableMetaRelation dbRelation = context.TableRelations.Include(r => r.Child).Include(r => r.Parent)
                        .SingleOrDefault(r => r.Name.Equals(tableMetaRelation.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (dbRelation == null)
                    {
                        // Add relation
                        var dbChild = context.Tables.SingleOrDefault(t => t.AppId == appId && t.Name == tableMetaRelation.Child.Name);
                        if (dbChild != null)
                            tableMetaRelation.Child = dbChild;

                        var dbParent = context.Tables.SingleOrDefault(t => t.AppId == appId && t.Name == tableMetaRelation.Parent.Name);
                        if (dbParent != null)
                            tableMetaRelation.Parent = dbParent;
                        dbRelation = tableMetaRelation;
                        context.TableRelations.Add(dbRelation);
                    }
                    else
                    {
                        // Update existing relation
                        if (dbRelation.Child.Name != tableMetaRelation.Child.Name)
                        {
                            var dbChild = context.Tables.SingleOrDefault(t => t.AppId == appId && t.Name == tableMetaRelation.Child.Name);
                            if (dbChild != null)
                                dbRelation.Child = dbChild;
                        }

                        if (dbRelation.Parent.Name != tableMetaRelation.Parent.Name)
                        {
                            var dbParent = context.Tables.SingleOrDefault(t => t.AppId == appId && t.Name == tableMetaRelation.Parent.Name);
                            if (dbParent != null)
                                dbRelation.Parent = dbParent;
                        }
                    }
                }

                if (newTable)
                    context.Tables.Add(dbTable);

                if (context.ChangeTracker.HasChanges())
                    context.SaveChanges();
            }

            // Save navigation
            var newGroups = new List<ModuleGroup>();
            foreach (var group in schemaInfo.App.Groups)
            {
                var newModules = new List<Module>();
                var dbGroup = context.ModuleGroups.SingleOrDefault(g => g.Name == group.Name && g.AppId == appId);
                foreach (var module in group.Modules)
                {
                    var dbModule = context.Modules.Include(m => m.Group).SingleOrDefault(m => m.ModuleType == ModuleType.AutoGenerated
                        && m.TableName == module.TableName && m.Group.AppId == appId);
                    if (dbModule == null)
                    {
                        if (dbGroup != null)
                            module.Group = dbGroup;
                        newModules.Add(module);
                    }
                }

                if (newModules.Count > 0)
                {
                    context.Modules.AddRange(newModules);
                }
            }

            if (!sharedContext)
                context.Dispose();
        }

        /// <summary>
        /// Load meta data from dotwebdb.
        /// </summary>
        /// <param name="sharedContext">Indicates that dbContext used in the process is the shared one.</param>
        public void LoadFromConfig(bool sharedContext = false)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["appId"]))
                throw new ArgumentException("appId must be specified in config.");
            appId = int.Parse(ConfigurationManager.AppSettings["appId"].ToString());
            if (sharedContext && dbConfig == null)
                throw new ArgumentException("using shared context requires initialization.");

            var context = dbConfig;
            if (!sharedContext)
                context = new DotWebDb();

            schemaInfo.App = context.Apps
                .Include(a => a.Groups)
                .Include(a => a.Groups.Select(g => g.Modules))
                .SingleOrDefault(a => a.Id == appId);

            schemaInfo.Tables = context.Tables
                .Include(t => t.Columns)
                .Include(t => t.Children)
                .Include(t => t.App)
                .Where(t => t.AppId == appId).ToList();

            if (!sharedContext)
                context.Dispose();
        }

        /// <summary>
        /// Main entry for generation purpose.
        /// </summary>
        /// <param name="connectionStringName">Connection string name stored in configuration file.</param>
        public void GenerateFromDb(string connectionStringName)
        {
            dbConfig = new DotWebDb();
            EnsureApp(dbConfig);
            Inspect(connectionStringName);
            GenerateNavigationModules(dbConfig);
            SaveToConfig(true);
            DetermineColumnForLookUpDisplay(dbConfig);
            SaveToConfig(true);

            dbConfig.Dispose();
        }

    }
}
