using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.DatabaseRepository.EfRepository;
using DataImporter.Entityes.Models;

namespace DataImporter.DataBaseMameger
{
    public class ProductManeger : IProductManeger
    {
        IProductRepository productRepository;
        public ProductManeger(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }
        public Product Get(string name)
        {
            return productRepository.Get(name);
        }

        public string GetSupplierUrl(string name)
        {
            return productRepository.GetSupplierUrl(name);
        }
    }
}
