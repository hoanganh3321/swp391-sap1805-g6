using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModels
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
