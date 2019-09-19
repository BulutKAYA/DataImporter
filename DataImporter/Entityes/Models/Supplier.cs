using DataImporter.Core;
using System.ComponentModel.DataAnnotations;

namespace DataImporter.Entityes.Models
{
    public class Supplier : IEntity
    {
        [Key]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierUrl { get; set; }
    }
}
