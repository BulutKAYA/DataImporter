namespace DataImporter.Entityes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Barcode = c.String(),
                        UnitInStock = c.Int(nullable: false),
                        Supplier_SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId)
                .Index(t => t.Supplier_SupplierId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Barcode = c.String(),
                    })
                .PrimaryKey(t => t.StockId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        SupplierUrl = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.StocksProducts",
                c => new
                    {
                        Stocks_StockId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Stocks_StockId, t.Product_ProductId })
                .ForeignKey("dbo.Stocks", t => t.Stocks_StockId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.Stocks_StockId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.StocksProducts", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.StocksProducts", "Stocks_StockId", "dbo.Stocks");
            DropIndex("dbo.StocksProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.StocksProducts", new[] { "Stocks_StockId" });
            DropIndex("dbo.Product", new[] { "Supplier_SupplierId" });
            DropTable("dbo.StocksProducts");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Stocks");
            DropTable("dbo.Product");
        }
    }
}
