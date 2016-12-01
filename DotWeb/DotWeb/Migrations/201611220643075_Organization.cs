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
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Users", "OrganizationId", c => c.Int());
            AddColumn("dbo.Users", "AuthKey", c => c.String(maxLength: 50));
            CreateIndex("dbo.Users", "OrganizationId");
            AddForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.Users", new[] { "OrganizationId" });
            DropColumn("dbo.Users", "AuthKey");
            DropColumn("dbo.Users", "OrganizationId");
            DropTable("dbo.Organizations");
        }
    }
}
