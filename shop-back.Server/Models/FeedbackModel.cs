using System.ComponentModel.DataAnnotations;
namespace shop_back.Server.Models
{
    public class FeedbackModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public required string Text { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}

