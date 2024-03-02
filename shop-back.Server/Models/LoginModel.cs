using System.ComponentModel.DataAnnotations;
namespace shop_back.Server.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = false;
        [DataType(DataType.Url)]
        public string? ReturnUrl { get; set; } = "";
    }
}
