using DevExpress.Web;
using DevExpress.XtraPrinting;
using DotWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace DotWeb.UI
{
    /// <summary>
    /// <para>This is the creator of master grid in a grid view page, whether grid view is used in one table scenario,
    /// master-detail scenario, and master-multiple details scenario.</para>
    /// <para>The creation of grid view depends heavily on meta data (see <see cref="TableMeta"/> created from the generator
    /// in inspection phase.</para>
    /// </summary>
    public class MasterGridCreator
    {
        private TableMeta tableMeta;
        private Button buttonMeta;
        private ASPxGridViewExporter masterGridExport;
        public ASPxGridView masterGrid;
        public ASPxButton btnExport;
        private string connectionString;
        List<PermissionType> permissions;

        /// <summary>
        /// Custom constructor accepting an instance of <see cref="TableMeta"/> and connection string.
        /// </summary>
        /// <param name="tableMeta">An instance of <see cref="TableMeta"/>.</param>
        /// <param name="connectionString">String, a connection string to the database being rendered.</param>
        /// <param name="permissions"></param>
        public MasterGridCreator(TableMeta tableMeta, string connectionString, List<PermissionType> permissions)
        {
            this.tableMeta = tableMeta;
            this.connectionString = connectionString;
            this.permissions = permissions;
        }

        /// <summary>
        /// Entry point to create master grid view.
        /// </summary>
        /// <returns>An instance of <see cref="ASPxGridView"/>.</returns>
        public ASPxGridView CreateMasterGrid()
        {
            var masterGrid = new ASPxGridView();
            masterGrid.ID = tableMeta.Name.ToCamelCase() + "Grid";
            masterGrid.ClientInstanceName = "masterGrid";
            masterGrid.CssClass = "gridView";
            masterGrid.AutoGenerateColumns = false;
            masterGrid.SettingsPager.PageSize = tableMeta.App.PageSize;
            masterGrid.Paddings.Padding = new Unit("0px");
            masterGrid.Border.BorderWidth = new Unit("0px");
            masterGrid.BorderBottom.BorderWidth = new Unit("1px");
            masterGrid.Settings.ShowGroupPanel = true;
            masterGrid.AutoGenerateColumns = false;
            masterGrid.SettingsBehavior.ConfirmDelete = true;
            masterGrid.SettingsSearchPanel.Visible = true;
            //arf
            //masterGrid.Columns.Add(GridViewHelper.AddGridViewCommandColumns(true,true,true));
            masterGrid.Columns.Add(GridViewHelper.AddGridViewCommandColumns(
                permissions.Contains(PermissionType.Insert), permissions.Contains(PermissionType.Update), permissions.Contains(PermissionType.Delete)
            ));
            masterGrid.CustomColumnDisplayText += masterGrid_CustomColumnDisplayText;
            masterGrid.CellEditorInitialize += masterGrid_CellEditorInitialize;
            //arf
            masterGrid.SettingsDetail.ShowDetailRow = true;

            // Create grid view columns
            foreach (var column in tableMeta.Columns.OrderBy(c => c.OrderNo))
            {
                if (!column.DisplayInGrid)
                    continue;

                var dataColumn = GridViewHelper.AddGridViewDataColumn(column, connectionString);
                if (dataColumn != null)
                    masterGrid.Columns.Add(dataColumn);
            }
            masterGrid.KeyFieldName = string.Join(";", tableMeta.PrimaryKeys.Select(x => x.Name));
            masterGrid.DataSource = GridViewHelper.GetGridViewDataSource(tableMeta, connectionString);

            // Master-detail scenario
            if (tableMeta.Children.Where(c => c.IsRendered).Count() == 1)
            {
                masterGrid.SettingsDetail.ShowDetailRow = true;
                masterGrid.Templates.DetailRow = new DetailGridTemplate(tableMeta, connectionString, permissions);
            }
            // Master-multiple details scenario
            else if (tableMeta.Children.Where(c => c.IsRendered).Count() > 1)
            {
                masterGrid.SettingsDetail.ShowDetailRow = true;
                masterGrid.Templates.DetailRow = new MultipleDetailGridTemplate(tableMeta, connectionString, permissions);
            }

            return masterGrid;
        }

        /// <summary>
        /// This is for truncation of long text in a text-column.
        /// </summary>
        /// <param name="sender">The sender is an instance of <see cref="ASPxGridView"/>.</param>
        /// <param name="e">Event args, an instance of <see cref="ASPxGridViewColumnDisplayTextEventArgs"/></param>
        void masterGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            GridViewHelper.gridView_CustomColumnDisplayText(sender, e, tableMeta.App.GridTextColumnMaxLength);
        }

        void masterGrid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            //arf
            GridViewHelper.gridView_CellEditorInitialize(tableMeta, e);
            var gridView = (sender as ASPxGridView);
            var columnMeta = tableMeta.Columns.SingleOrDefault(c => c.Name.Equals(e.Column.FieldName, System.StringComparison.InvariantCultureIgnoreCase));
            if (columnMeta == null)
                throw new ArgumentException(string.Format("Column meta entry not found for column ", e.Column.FieldName));
            var sqlDataSource = e.Editor.DataSource as SqlDataSource;
            ColumnMeta filterColumnMeta = null;
            if (!string.IsNullOrEmpty(columnMeta.FilterColumn))
                filterColumnMeta = columnMeta.ReferenceTable.Columns.SingleOrDefault(c => c.Name.Equals(columnMeta.FilterColumn));
            if (filterColumnMeta != null)
            {
                sqlDataSource.SelectCommand += string.Format("WHERE {0} = @{0}", filterColumnMeta.Name);
                sqlDataSource.SelectParameters.Add(filterColumnMeta.Name, e.KeyValue.ToString());
            }
            e.Editor.DataBind();
        }
        void masterGrid_RowCommand(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

            if (e.ButtonID == "customBtn")
            {
                if (!string.IsNullOrEmpty(e.VisibleIndex.ToString()))
                {
                    var gridExpand = (ASPxGridView)sender;
                    gridExpand.DetailRows.ExpandRow(e.VisibleIndex);
                    gridExpand.CancelEdit();
                    //masterGridExport.WriteXlsxToResponse();
                }
            }

        }
        public ASPxGridViewExporter CreateGridExport()
        {

            masterGridExport = new ASPxGridViewExporter();

            masterGridExport.GridViewID = string.Concat(tableMeta.Children[0], "GridView");
            masterGridExport.ID = "GridExport";
            masterGridExport.ExportedRowType = GridViewExportedRowType.All;

            return masterGridExport;
        }

        public ASPxButton buttonExportGrid()
        {
            btnExport = new ASPxButton();
            btnExport.ID = "btnExportGrid";
            btnExport.Text = "Exprot to excel";
            btnExport.Click += btnExport_click;
            return btnExport;
        }
        void btnExport_click(object sender, EventArgs e)
        {
            //masterGridExport.WriteXlsToResponse(new de);
            masterGridExport.WriteXlsxToResponse();

        }

    }
}
