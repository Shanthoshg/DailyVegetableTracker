
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyVegetableTracker.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public int VegetableId { get; set; }

        public Vegetable? Vegetable { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal Quantity { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = "kg";

        [Required]
        [Range(0.01, 1000000)]
        public decimal PricePerUnit { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.Today;

        [StringLength(200)]
        public string? ShopName { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [NotMapped]
        public decimal TotalCost => Quantity * PricePerUnit;
    }
}
