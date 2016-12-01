namespace DotWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        GridTextColumnMaxLength = c.Int(nullable: false),
                        PageSize = c.Int(nullable: false),
                        DefaultGroupName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        OrderNo = c.Int(nullable: false),
                        ShowInLeftMenu = c.Boolean(nullable: false),
                        AppId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apps", t => t.AppId, cascadeDelete: true)
                .Index(t => t.AppId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ModuleType = c.Int(nullable: false),
                        ScaffoldEntity = c.String(maxLength: 100),
                        TableName = c.String(maxLength: 100),
                        OrderNo = c.Int(nullable: false),
                        Url = c.String(maxLength: 100),
                        ShowInLeftMenu = c.Boolean(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ColumnMetas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(nullable: false, maxLength: 100),
                        DataType = c.Int(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        MaxLength = c.Int(),
                        OrderNo = c.Int(nullable: false),
                        DisplayInGrid = c.Boolean(nullable: false),
                        EnumTypeName = c.String(maxLength: 100),
                        IsForeignKey = c.Boolean(nullable: false),
                        IsPrimaryKey = c.Boolean(nullable: false),
                        IsIdentity = c.Boolean(nullable: false),
                        ReferenceTableId = c.Int(),
                        FilterColumn = c.String(),
                        TableId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableMetas", t => t.ReferenceTableId)
                .ForeignKey("dbo.TableMetas", t => t.TableId, cascadeDelete: true)
                .Index(t => t.ReferenceTableId)
                .Index(t => t.TableId);
            
            CreateTable(
                "dbo.TableMetas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(nullable: false, maxLength: 100),
                        SchemaName = c.String(nullable: false, maxLength: 100),
                        AppId = c.Int(nullable: false),
                        LookUpDisplayColumnId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apps", t => t.AppId, cascadeDelete: true)
                .Index(t => t.AppId);
            
            CreateTable(
                "dbo.TableMetaRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                        ForeignKeyName = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 100),
                        IsRendered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableMetas", t => t.ParentId)
                .ForeignKey("dbo.TableMetas", t => t.ChildId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ColumnMetas", "TableId", "dbo.TableMetas");
            DropForeignKey("dbo.ColumnMetas", "ReferenceTableId", "dbo.TableMetas");
            DropForeignKey("dbo.TableMetaRelations", "ChildId", "dbo.TableMetas");
            DropForeignKey("dbo.TableMetaRelations", "ParentId", "dbo.TableMetas");
            DropForeignKey("dbo.TableMetas", "AppId", "dbo.Apps");
            DropForeignKey("dbo.Modules", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "AppId", "dbo.Apps");
            DropIndex("dbo.TableMetaRelations", new[] { "ChildId" });
            DropIndex("dbo.TableMetaRelations", new[] { "ParentId" });
            DropIndex("dbo.TableMetas", new[] { "AppId" });
            DropIndex("dbo.ColumnMetas", new[] { "TableId" });
            DropIndex("dbo.ColumnMetas", new[] { "ReferenceTableId" });
            DropIndex("dbo.Modules", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "AppId" });
            DropTable("dbo.TableMetaRelations");
            DropTable("dbo.TableMetas");
            DropTable("dbo.ColumnMetas");
            DropTable("dbo.Modules");
            DropTable("dbo.Groups");
            DropTable("dbo.Apps");
        }
    }
}
