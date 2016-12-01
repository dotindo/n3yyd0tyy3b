namespace DotWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Organization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Code);
            
            AddColumn("dbo.Users", "OrganizationCode", c => c.String(maxLength: 20));
            AddColumn("dbo.Users", "AuthKey", c => c.String(maxLength: 50));
            CreateIndex("dbo.Users", "OrganizationCode");
            AddForeignKey("dbo.Users", "OrganizationCode", "dbo.Organizations", "Code");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "OrganizationCode", "dbo.Organizations");
            DropIndex("dbo.Users", new[] { "OrganizationCode" });
            DropColumn("dbo.Users", "AuthKey");
            DropColumn("dbo.Users", "OrganizationCode");
            DropTable("dbo.Organizations");
        }
    }
}
