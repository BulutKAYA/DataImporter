using DataImporter.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataImporter.Entityes.Models
{
    [Table("Product")]
    public class Product: IEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitInStock { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
