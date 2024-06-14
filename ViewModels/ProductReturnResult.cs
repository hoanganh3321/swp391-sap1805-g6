using BackEnd.Models;

namespace BackEnd.ViewModels
{
    public class ProductReturnResult //lớp bao bọc để có thể chứa cả hai kiểu dữ liệu Product và ProductReturn.
    {
        public bool IsNewProduct { get; set; }
        public Product? Product { get; set; }
        public ProductReturn? ProductReturn { get; set; }
    }
}
