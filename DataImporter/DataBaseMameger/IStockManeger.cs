using DataImporter.Entityes.Models;

namespace DataImporter.DataBaseMameger
{
    public interface IStockManeger
    {
        Stock Get(string barcode);
        Stock Save(Stock stock);
        int Update(Stock stock);
    }
}
