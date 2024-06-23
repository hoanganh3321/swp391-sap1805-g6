using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels
{
    public class ProductReturnViewModel
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "ReturnDate is required.")]
        public DateTime ReturnDate { get; set; }
        public string? ReturnReason { get; set; }


        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public required string ProductName { get; set; }

        [StringLength(50, ErrorMessage = "Barcode cannot exceed 50 characters.")]
        public string? Barcode { get; set; }
        [Required]
        public decimal? Weight { get; set; }
        [Required]
        public decimal? Price { get; set; }

        public decimal? ManufacturingCost { get; set; }
        [Required]
        public decimal? StoneCost { get; set; }
        [StringLength(255, ErrorMessage = "Warranty cannot exceed 255 characters.")]
        public string? Warranty { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int? Quantity { get; set; }

        public bool? IsBuyback { get; set; }
        [Required(ErrorMessage = "CategoryId is required.")]
        public int? CategoryId { get; set; }
        [Required]
        public int? StoreId { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public string? location {  get; set; }
    }
}
