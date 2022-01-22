namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsHidden = c.Boolean(nullable: false, defaultValue:false),
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
                        IsHidden = c.Boolean(nullable: false, defaultValue:false),
                        StockCount = c.Int(nullable: false),
                        ReleasedDate = c.Int(nullable: false),
                        MediaTypeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.MediaTypeId)
                .Index(t => t.CategoryId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.MediaTypes",
                c => new
                    {
                        MediaTypeId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ImagePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MediaTypeId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        ValueInPercent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 10),
                        IsAdmin = c.Boolean(nullable: false, defaultValue:false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
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
            DropForeignKey("dbo.Products", "MediaTypeId", "dbo.MediaTypes");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "SaleId" });
            DropIndex("dbo.Products", new[] { "SaleId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "MediaTypeId" });
            DropIndex("dbo.Categories", new[] { "SaleId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.MediaTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
