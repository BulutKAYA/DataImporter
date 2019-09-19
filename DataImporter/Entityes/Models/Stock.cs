using DataImporter.Core;
using DataImporter.Entityes.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataImporter.Entityes.Models
{
    public class Stock : IEntity
    {
        [Key]
        public int StockId { get; set; }
        public string Barcode { get; set; }
        public int Piece { get; set; }
        public List<Product> Products { get; set; }
    }
}
