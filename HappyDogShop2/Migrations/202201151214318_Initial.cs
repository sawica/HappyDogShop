namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Is_hidden = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.ParentId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 60),
                        Price = c.Int(nullable: false),
                        Is_hidden = c.Boolean(nullable: false),
                        Stock_count = c.Int(nullable: false),
                        Image1 = c.String(nullable: false),
                        Image2 = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.CategoryId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Start_date = c.DateTime(nullable: false),
                        End_date = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Value_in_percent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        First_name = c.String(nullable: false, maxLength: 50),
                        Last_name = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 10),
                        Is_admin = c.Boolean(nullable: false),
                        SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Products", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Users", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "SaleId" });
            DropIndex("dbo.Products", new[] { "SaleId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "SaleId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
