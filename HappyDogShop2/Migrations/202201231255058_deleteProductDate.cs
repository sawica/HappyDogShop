namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteProductDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ReleasedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ReleasedDate", c => c.Int(nullable: false));
        }
    }
}
