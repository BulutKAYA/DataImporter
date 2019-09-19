namespace DataImporter.Entityes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suppliers", "SupplierUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "SupplierUrl", c => c.Int(nullable: false));
        }
    }
}
