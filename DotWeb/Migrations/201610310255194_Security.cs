namespace DotWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Security : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Groups", newName: "ModuleGroups");
            CreateTable(
                "dbo.AccessRights",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PrincipalId = c.String(nullable: false, maxLength: 128),
                        PrincipalType = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        SecuredObjectId = c.Int(nullable: false),
                        SecuredObjectType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        AppId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apps", t => t.AppId)
                .Index(t => t.AppId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PermissionType = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserGroupMembers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.UserGroups", t => t.GroupId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        AppId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apps", t => t.AppId)
                .Index(t => t.AppId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroupMembers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserGroupMembers", "GroupId", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroups", "AppId", "dbo.Apps");
            DropForeignKey("dbo.AccessRights", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "AppId", "dbo.Apps");
            DropIndex("dbo.UserGroups", new[] { "AppId" });
            DropIndex("dbo.UserGroupMembers", new[] { "GroupId" });
            DropIndex("dbo.UserGroupMembers", new[] { "UserId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Roles", new[] { "AppId" });
            DropIndex("dbo.AccessRights", new[] { "RoleId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
            DropTable("dbo.UserGroupMembers");
            DropTable("dbo.Permissions");
            DropTable("dbo.Roles");
            DropTable("dbo.AccessRights");
            RenameTable(name: "dbo.ModuleGroups", newName: "Groups");
        }
    }
}
