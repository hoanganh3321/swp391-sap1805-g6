using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels
{
    public class DeleteCartViewModel
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
