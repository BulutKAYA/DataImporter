using DataImporter.Entityes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Entityes.Contexts
{
    public class DataImporterContext: DbContext
    {
        public DataImporterContext()
            :base("Server=LAPTOP-1CAUHSG4;Database=DataImporter;Trusted_Connection=True;"){}
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }

    }
}
