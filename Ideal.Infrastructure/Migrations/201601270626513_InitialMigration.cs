namespace Ideal.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Layout = c.Int(nullable: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModelPath = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Form_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.Form_Id)
                .Index(t => t.Form_Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        MachineName = c.String(),
                        Details = c.String(),
                        EventType = c.String(),
                        EventSequence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EventOccurrence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequestUrl = c.String(),
                        EventCode = c.Int(nullable: false),
                        ExceptionType = c.String(),
                        EventDetailCode = c.Int(nullable: false),
                        ApplicationPath = c.String(),
                        ApplicationVirtualPath = c.String(),
                        Tenant = c.String(),
                        Username = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tenant = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        PasswordChanged = c.DateTime(nullable: false),
                        IsAccountVerified = c.Boolean(nullable: false),
                        IsLoginAllowed = c.Boolean(nullable: false),
                        IsAccountClosed = c.Boolean(nullable: false),
                        AccountClosed = c.DateTime(),
                        LastLogin = c.DateTime(),
                        LastFailedLogin = c.DateTime(),
                        FailedLoginCount = c.Int(nullable: false),
                        VerificationKey = c.String(maxLength: 100),
                        VerificationKeySent = c.DateTime(),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        PhotoId = c.String(),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        Comment = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Type = c.String(nullable: false, maxLength: 150),
                        Value = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.UserId, t.Type, t.Value })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Fields", "Form_Id", "dbo.Forms");
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Fields", new[] { "Form_Id" });
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Logs");
            DropTable("dbo.Fields");
            DropTable("dbo.Forms");
        }
    }
}
