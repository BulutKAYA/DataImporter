namespace DataImporter.Entityes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "Supplier_SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Product", new[] { "Supplier_SupplierId" });
            RenameColumn(table: "dbo.Product", name: "Supplier_SupplierId", newName: "SupplierId");
            AlterColumn("dbo.Product", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "SupplierId");
            AddForeignKey("dbo.Product", "SupplierId", "dbo.Suppliers", "SupplierId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Product", new[] { "SupplierId" });
            AlterColumn("dbo.Product", "SupplierId", c => c.Int());
            RenameColumn(table: "dbo.Product", name: "SupplierId", newName: "Supplier_SupplierId");
            CreateIndex("dbo.Product", "Supplier_SupplierId");
            AddForeignKey("dbo.Product", "Supplier_SupplierId", "dbo.Suppliers", "SupplierId");
        }
    }
}
