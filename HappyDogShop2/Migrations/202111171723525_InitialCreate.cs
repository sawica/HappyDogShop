namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Is_hidden = c.Boolean(nullable: false),
                        Parent_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.Parent_CategoryId)
                .Index(t => t.Parent_CategoryId);
            
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
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Parent_CategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Parent_CategoryId" });
            DropIndex("dbo.Products", new[] { "Category_CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
        }
    }
}
