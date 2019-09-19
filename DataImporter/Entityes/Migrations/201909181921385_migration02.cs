namespace DataImporter.Entityes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration02 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StocksProducts", newName: "StockProducts");
            RenameColumn(table: "dbo.StockProducts", name: "Stocks_StockId", newName: "Stock_StockId");
            RenameIndex(table: "dbo.StockProducts", name: "IX_Stocks_StockId", newName: "IX_Stock_StockId");
            AddColumn("dbo.Product", "ProductName", c => c.String());
            AddColumn("dbo.Stocks", "Piece", c => c.Int(nullable: false));
            DropColumn("dbo.Product", "Barcode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Barcode", c => c.String());
            DropColumn("dbo.Stocks", "Piece");
            DropColumn("dbo.Product", "ProductName");
            RenameIndex(table: "dbo.StockProducts", name: "IX_Stock_StockId", newName: "IX_Stocks_StockId");
            RenameColumn(table: "dbo.StockProducts", name: "Stock_StockId", newName: "Stocks_StockId");
            RenameTable(name: "dbo.StockProducts", newName: "StocksProducts");
        }
    }
}
