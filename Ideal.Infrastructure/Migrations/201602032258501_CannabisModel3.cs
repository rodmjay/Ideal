namespace Ideal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CannabisModel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Effects", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.Symptoms", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Effects", new[] { "Recipe_Id" });
            DropIndex("dbo.Symptoms", new[] { "Recipe_Id" });
            CreateTable(
                "dbo.RecipeEffect",
                c => new
                    {
                        RecipeId = c.Int(nullable: false),
                        EffectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.EffectId })
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.Effects", t => t.EffectId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.EffectId);
            
            CreateTable(
                "dbo.RecipeSymptom",
                c => new
                    {
                        RecipeRefId = c.Int(nullable: false),
                        SymptomRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeRefId, t.SymptomRefId })
                .ForeignKey("dbo.Recipes", t => t.RecipeRefId, cascadeDelete: true)
                .ForeignKey("dbo.Symptoms", t => t.SymptomRefId, cascadeDelete: true)
                .Index(t => t.RecipeRefId)
                .Index(t => t.SymptomRefId);
            
            DropColumn("dbo.Effects", "Recipe_Id");
            DropColumn("dbo.Symptoms", "Recipe_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Symptoms", "Recipe_Id", c => c.Int());
            AddColumn("dbo.Effects", "Recipe_Id", c => c.Int());
            DropForeignKey("dbo.RecipeSymptom", "SymptomRefId", "dbo.Symptoms");
            DropForeignKey("dbo.RecipeSymptom", "RecipeRefId", "dbo.Recipes");
            DropForeignKey("dbo.RecipeEffect", "EffectId", "dbo.Effects");
            DropForeignKey("dbo.RecipeEffect", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.RecipeSymptom", new[] { "SymptomRefId" });
            DropIndex("dbo.RecipeSymptom", new[] { "RecipeRefId" });
            DropIndex("dbo.RecipeEffect", new[] { "EffectId" });
            DropIndex("dbo.RecipeEffect", new[] { "RecipeId" });
            DropTable("dbo.RecipeSymptom");
            DropTable("dbo.RecipeEffect");
            CreateIndex("dbo.Symptoms", "Recipe_Id");
            CreateIndex("dbo.Effects", "Recipe_Id");
            AddForeignKey("dbo.Symptoms", "Recipe_Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.Effects", "Recipe_Id", "dbo.Recipes", "Id");
        }
    }
}
