using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels
{
    public class ProductReturnViewModel
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "ReturnDate is required.")]
        public DateTime ReturnDate { get; set; }
        [Required(ErrorMessage = "ReturnReason is required.")]
        public string? ReturnReason { get; set; }


        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public required string ProductName { get; set; }

        [StringLength(50, ErrorMessage = "Barcode cannot exceed 50 characters.")]
        public string? Barcode { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Price { get; set; }

        public decimal? ManufacturingCost { get; set; }
        public decimal? StoneCost { get; set; }
        [StringLength(255, ErrorMessage = "Warranty cannot exceed 255 characters.")]
        public string? Warranty { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int? Quantity { get; set; }

        public bool? IsBuyback { get; set; }

        public int? CategoryId { get; set; }

        public int? StoreId { get; set; }

        public string? Image { get; set; }
        [Required]
        public string? location {  get; set; }
    }
}
