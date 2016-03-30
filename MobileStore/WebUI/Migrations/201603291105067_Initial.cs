namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartLines",
                c => new
                    {
                        CartLineId = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        MobilePhoneId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartLineId)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserIp = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.MobilePhones",
                c => new
                    {
                        MobilePhoneId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.MobilePhoneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartLines", "CartId", "dbo.Carts");
            DropIndex("dbo.CartLines", new[] { "CartId" });
            DropTable("dbo.MobilePhones");
            DropTable("dbo.Carts");
            DropTable("dbo.CartLines");
        }
    }
}
