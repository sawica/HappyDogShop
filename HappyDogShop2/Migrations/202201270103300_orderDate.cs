namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false, defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Date");
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
        }
    }
}
