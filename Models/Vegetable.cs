using System.ComponentModel.DataAnnotations;

namespace DailyVegetableTracker.Models
{
    public class Vegetable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = "kg";
    }
}