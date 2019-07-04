namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04072019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChatMessageText = c.String(maxLength: 100),
                        TaskAssignId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskAssigns", t => t.TaskAssignId, cascadeDelete: true)
                .Index(t => t.ChatMessageText)
                .Index(t => t.TaskAssignId)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.TaskAssigns",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TaskAssignType = c.String(nullable: false),
                        TaskAssignTypeId = c.Long(nullable: false),
                        TaskName = c.String(nullable: false, maxLength: 100),
                        TaskShortName = c.String(),
                        Logo = c.String(),
                        Banner = c.String(),
                        TaskType = c.String(),
                        Priority = c.String(),
                        TaskDetails = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TaskName)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.CommentOnTasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TaskComment = c.String(maxLength: 100),
                        TaskAssignId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskAssigns", t => t.TaskAssignId, cascadeDelete: true)
                .Index(t => t.TaskComment)
                .Index(t => t.TaskAssignId)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.CompanyLists",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ShortName = c.String(nullable: false),
                        ContactPerson = c.String(),
                        ContactMobile = c.String(nullable: false),
                        RegisterNo = c.String(),
                        Address = c.String(),
                        Status = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ShortName = c.String(nullable: false),
                        Detail = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.EmployeeInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ProfilePhoto = c.String(),
                        SignaturePhoto = c.String(),
                        Mobile = c.String(maxLength: 100),
                        EmergencyMobile = c.String(),
                        Address = c.String(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Designation = c.String(nullable: false),
                        DepartmentId = c.Long(nullable: false),
                        CompanyListId = c.Long(nullable: false),
                        RankListId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyLists", t => t.CompanyListId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.RankLists", t => t.RankListId, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.Mobile)
                .Index(t => t.DepartmentId)
                .Index(t => t.CompanyListId)
                .Index(t => t.RankListId)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.RankLists",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Detail = c.String(),
                        Status = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.GroupDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GroupId = c.Long(nullable: false),
                        EmployeeInfoId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeInfoes", t => t.EmployeeInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.EmployeeInfoId)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortName = c.String(nullable: false),
                        Details = c.String(),
                        Logo = c.String(),
                        Banner = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        WhomEmployee = c.String(),
                        ProfilePhoto = c.String(),
                        SignaturePhoto = c.String(),
                        Mobile = c.String(),
                        EmergencyMobile = c.String(),
                        Address = c.String(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        RegistrationNo = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Designation = c.String(nullable: false),
                        DepartmentId = c.Long(nullable: false),
                        CompanyListId = c.Long(nullable: false),
                        RankListId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyLists", t => t.CompanyListId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.RankLists", t => t.RankListId, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.DepartmentId)
                .Index(t => t.CompanyListId)
                .Index(t => t.RankListId)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SoftwareUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(),
                        RecoveryEmail = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.TaskAttachments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TaskAttachmentName = c.String(maxLength: 100),
                        TaskAttachmentFileName = c.String(),
                        TaskAttachmentStatus = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TaskAttachmentName)
                .Index(t => t.Created)
                .Index(t => t.Modified);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Members", "RankListId", "dbo.RankLists");
            DropForeignKey("dbo.Members", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Members", "CompanyListId", "dbo.CompanyLists");
            DropForeignKey("dbo.GroupDetails", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupDetails", "EmployeeInfoId", "dbo.EmployeeInfoes");
            DropForeignKey("dbo.EmployeeInfoes", "RankListId", "dbo.RankLists");
            DropForeignKey("dbo.EmployeeInfoes", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EmployeeInfoes", "CompanyListId", "dbo.CompanyLists");
            DropForeignKey("dbo.CommentOnTasks", "TaskAssignId", "dbo.TaskAssigns");
            DropForeignKey("dbo.ChatMessages", "TaskAssignId", "dbo.TaskAssigns");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TaskAttachments", new[] { "Modified" });
            DropIndex("dbo.TaskAttachments", new[] { "Created" });
            DropIndex("dbo.TaskAttachments", new[] { "TaskAttachmentName" });
            DropIndex("dbo.SoftwareUsers", new[] { "Modified" });
            DropIndex("dbo.SoftwareUsers", new[] { "Created" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Members", new[] { "Modified" });
            DropIndex("dbo.Members", new[] { "Created" });
            DropIndex("dbo.Members", new[] { "RankListId" });
            DropIndex("dbo.Members", new[] { "CompanyListId" });
            DropIndex("dbo.Members", new[] { "DepartmentId" });
            DropIndex("dbo.Members", new[] { "Name" });
            DropIndex("dbo.Groups", new[] { "Modified" });
            DropIndex("dbo.Groups", new[] { "Created" });
            DropIndex("dbo.GroupDetails", new[] { "Modified" });
            DropIndex("dbo.GroupDetails", new[] { "Created" });
            DropIndex("dbo.GroupDetails", new[] { "EmployeeInfoId" });
            DropIndex("dbo.GroupDetails", new[] { "GroupId" });
            DropIndex("dbo.RankLists", new[] { "Modified" });
            DropIndex("dbo.RankLists", new[] { "Created" });
            DropIndex("dbo.RankLists", new[] { "Name" });
            DropIndex("dbo.EmployeeInfoes", new[] { "Modified" });
            DropIndex("dbo.EmployeeInfoes", new[] { "Created" });
            DropIndex("dbo.EmployeeInfoes", new[] { "RankListId" });
            DropIndex("dbo.EmployeeInfoes", new[] { "CompanyListId" });
            DropIndex("dbo.EmployeeInfoes", new[] { "DepartmentId" });
            DropIndex("dbo.EmployeeInfoes", new[] { "Mobile" });
            DropIndex("dbo.EmployeeInfoes", new[] { "Name" });
            DropIndex("dbo.Departments", new[] { "Modified" });
            DropIndex("dbo.Departments", new[] { "Created" });
            DropIndex("dbo.Departments", new[] { "Name" });
            DropIndex("dbo.CompanyLists", new[] { "Modified" });
            DropIndex("dbo.CompanyLists", new[] { "Created" });
            DropIndex("dbo.CompanyLists", new[] { "Name" });
            DropIndex("dbo.CommentOnTasks", new[] { "Modified" });
            DropIndex("dbo.CommentOnTasks", new[] { "Created" });
            DropIndex("dbo.CommentOnTasks", new[] { "TaskAssignId" });
            DropIndex("dbo.CommentOnTasks", new[] { "TaskComment" });
            DropIndex("dbo.TaskAssigns", new[] { "Modified" });
            DropIndex("dbo.TaskAssigns", new[] { "Created" });
            DropIndex("dbo.TaskAssigns", new[] { "TaskName" });
            DropIndex("dbo.ChatMessages", new[] { "Modified" });
            DropIndex("dbo.ChatMessages", new[] { "Created" });
            DropIndex("dbo.ChatMessages", new[] { "TaskAssignId" });
            DropIndex("dbo.ChatMessages", new[] { "ChatMessageText" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TaskAttachments");
            DropTable("dbo.SoftwareUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Members");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupDetails");
            DropTable("dbo.RankLists");
            DropTable("dbo.EmployeeInfoes");
            DropTable("dbo.Departments");
            DropTable("dbo.CompanyLists");
            DropTable("dbo.CommentOnTasks");
            DropTable("dbo.TaskAssigns");
            DropTable("dbo.ChatMessages");
        }
    }
}
