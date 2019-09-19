using DataImporter.Entityes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.DatabaseRepository.EfRepository
{
    public interface IProductRepository
    {
        Product Get(string name);
        String GetSupplierUrl(string name);

    }
}
