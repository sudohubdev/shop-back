using System.ComponentModel.DataAnnotations;
namespace shop_back.Server.Models
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
    }
}
