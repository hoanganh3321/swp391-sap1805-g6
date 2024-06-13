using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels
{
    public class PromotionViewModel
    {
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly? StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly? EndDate { get; set; }
        [Required]
        [Range(0, 99.99)]
        public decimal? Discount { get; set; }
        [Required]
        public bool? Approved { get; set; }
    }
}
