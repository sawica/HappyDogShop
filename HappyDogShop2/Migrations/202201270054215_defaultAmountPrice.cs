namespace HappyDogShop2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defaultAmountPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue:0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
