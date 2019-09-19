using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Entityes.Contexts;
using DataImporter.Entityes.Models;

namespace DataImporter.DatabaseRepository.EfRepository
{
    public class ProductRepository : IProductRepository
    {
        DataImporterContext context = new DataImporterContext();
        public Product Get(string name)
        {
            return context.Products.Where(x => x.ProductName == name).FirstOrDefault();
        }

        public String GetSupplierUrl(string name)
        {
            //burada primary key yerine productname ile işlem yapmak zorunda kaldım var olan datalar yüzünden
            return (from a in context.Products
                    join b in context.Suppliers on a.SupplierId equals b.SupplierId
                    where a.ProductName == name
                    select b.SupplierUrl).ToString();
        }
    }
}
