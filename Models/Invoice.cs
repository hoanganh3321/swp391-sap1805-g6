using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [Required]
        public int? OrderId { get; set; }
        [Required]
        public int? PromotionId { get; set; }

        public string? PromotionName { get; set; }
        [Required]
        public decimal? TotalPrice { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Promotion? Promotion { get; set; }
    }
}
