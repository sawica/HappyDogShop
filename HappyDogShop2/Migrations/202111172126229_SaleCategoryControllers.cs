namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaleCategoryControllers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Categories", "Sale_SaleId", c => c.Int());
            AddColumn("dbo.Products", "Sale_SaleId", c => c.Int());
            AddColumn("dbo.Sales", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Sales", "Value_in_percent", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Sale_SaleId", c => c.Int());
            CreateIndex("dbo.Categories", "Sale_SaleId");
            CreateIndex("dbo.Products", "Sale_SaleId");
            CreateIndex("dbo.Users", "Sale_SaleId");
            AddForeignKey("dbo.Categories", "Sale_SaleId", "dbo.Sales", "SaleId");
            AddForeignKey("dbo.Products", "Sale_SaleId", "dbo.Sales", "SaleId");
            AddForeignKey("dbo.Users", "Sale_SaleId", "dbo.Sales", "SaleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Sale_SaleId", "dbo.Sales");
            DropForeignKey("dbo.Products", "Sale_SaleId", "dbo.Sales");
            DropForeignKey("dbo.Categories", "Sale_SaleId", "dbo.Sales");
            DropIndex("dbo.Users", new[] { "Sale_SaleId" });
            DropIndex("dbo.Products", new[] { "Sale_SaleId" });
            DropIndex("dbo.Categories", new[] { "Sale_SaleId" });
            DropColumn("dbo.Users", "Sale_SaleId");
            DropColumn("dbo.Sales", "Value_in_percent");
            DropColumn("dbo.Sales", "Name");
            DropColumn("dbo.Products", "Sale_SaleId");
            DropColumn("dbo.Categories", "Sale_SaleId");
            DropColumn("dbo.Categories", "Name");
        }
    }
}
