namespace DotWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrgCodeChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.Users", new[] { "OrganizationId" });
            //RenameColumn(table: "dbo.Users", name: "OrganizationCode", newName: "OrganizationId");
            DropPrimaryKey("dbo.Organizations");
            AddColumn("dbo.Organizations", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "OrganizationId", c => c.Int());
            AddPrimaryKey("dbo.Organizations", "Id");
            CreateIndex("dbo.Users", "OrganizationId");
            AddForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations", "Id");
            DropColumn("dbo.Organizations", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organizations", "Code", c => c.String(nullable: false, maxLength: 20));
            DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.Users", new[] { "OrganizationId" });
            DropPrimaryKey("dbo.Organizations");
            AlterColumn("dbo.Users", "OrganizationId", c => c.String(maxLength: 20));
            DropColumn("dbo.Organizations", "Id");
            AddPrimaryKey("dbo.Organizations", "Code");
            RenameColumn(table: "dbo.Users", name: "OrganizationId", newName: "OrganizationCode");
            CreateIndex("dbo.Users", "OrganizationCode");
            AddForeignKey("dbo.Users", "OrganizationCode", "dbo.Organizations", "Code");
        }
    }
}
