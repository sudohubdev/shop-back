using System.ComponentModel.DataAnnotations;
namespace shop_back.Server.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = false;
        [DataType(DataType.Url)]
        public string? ReturnUrl { get; set; } = "";
    }
}
