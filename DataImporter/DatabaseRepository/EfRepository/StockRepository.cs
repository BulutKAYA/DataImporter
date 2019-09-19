using System.Linq;
using DataImporter.Entityes.Contexts;
using DataImporter.Entityes.Models;

namespace DataImporter.DatabaseRepository.EfRepository
{
    public class StockRepository : IStockRepository
    {
        DataImporterContext context = new DataImporterContext();
        public Stock Get(string barcode)
        {
            return context.Stocks.Where(x => x.Barcode == barcode).FirstOrDefault();
        }

        public Stock Save(Stock stock)
        {
            context.Stocks.Add(stock);
            context.SaveChanges();
            return stock;
        }

        public int Update(Stock stock)
        {
            Stock temlete = context.Stocks.Find(stock.StockId);
            temlete.Piece = stock.Piece;
            temlete.Barcode = stock.Barcode;
            return context.SaveChanges();
        }

       
    }
}
