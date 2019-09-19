using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.DatabaseRepository.EfRepository;
using DataImporter.Entityes.Models;

namespace DataImporter.DataBaseMameger
{
    public class StockManeger : IStockManeger
    {
        IStockRepository stockRepository;
        public StockManeger(IStockRepository _stockRepository)
        {
            this.stockRepository = _stockRepository;
        }
        public Stock Get(string barcode)
        {
            return stockRepository.Get(barcode);
        }

        public Stock Save(Stock stock)
        {
            return stockRepository.Save(stock);
        }

        public int Update(Stock stock)
        {
            return stockRepository.Update(stock);
        }
    }
}
