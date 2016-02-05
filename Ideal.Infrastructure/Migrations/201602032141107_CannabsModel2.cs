namespace Ideal.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CannabsModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "PartsCBD", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "PartsTHC", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "PartsTHC");
            DropColumn("dbo.Recipes", "PartsCBD");
        }
    }
}
