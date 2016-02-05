namespace Ideal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CannabisModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Effects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Recipe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id)
                .Index(t => t.Recipe_Id);
            
            CreateTable(
                "dbo.Symptoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        Recipe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id)
                .Index(t => t.Recipe_Id);
            
            DropColumn("dbo.Users", "PhotoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PhotoId", c => c.String());
            DropForeignKey("dbo.Symptoms", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.Effects", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Symptoms", new[] { "Recipe_Id" });
            DropIndex("dbo.Effects", new[] { "Recipe_Id" });
            DropTable("dbo.Symptoms");
            DropTable("dbo.Effects");
            DropTable("dbo.Recipes");
        }
    }
}
