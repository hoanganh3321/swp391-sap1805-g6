using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        [Required]
        public int? OrderId { get; set; }
        [Required]
        public int? PromotionId { get; set; }

        public string? PromotionName { get; set; }
        [Required]
        public decimal? TotalPrice { get; set; }

        public int? StaffId { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Promotion? Promotion { get; set; }

        public virtual Staff? Staff { get; set; }
    }
}
