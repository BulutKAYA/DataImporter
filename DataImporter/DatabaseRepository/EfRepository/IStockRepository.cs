using System;
using System.Collections.Generic;
using System.Linq;
using DataImporter.Entityes.Models;

namespace DataImporter.DatabaseRepository.EfRepository
{
    public interface IStockRepository
    {
        Stock Get(string barcode);
        Stock Save(Stock stock);
        int Update(Stock stock);
    }
}
