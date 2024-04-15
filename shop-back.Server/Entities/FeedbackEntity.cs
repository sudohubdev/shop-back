using Microsoft.AspNetCore.Identity;
using shop_back.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


/*
id відгука
id користувача
текст відгуку
рейтинг відгуку
*/
namespace shop_back.Server.Entities
{
    public class FeedbackEntity : BaseEntity
    {
        public string UserId { get; set; }
        [NotMapped]
        public string? UserName => User?.UserName;
        [NotMapped]
        public string? UserEmail => User?.Email;
        [JsonIgnore]
        public IdentityUser User { get; set; } = null!;
        public int ProductId { get; set; }
        [JsonIgnore]
        public ProductEntity Product { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }


#pragma warning disable CS8618 // Required by Entity Framework
        public FeedbackEntity() { }

        public FeedbackEntity(FeedbackModel feedbackData, ProductEntity product, string userId)
        {
            UserId = userId;
            ProductId = product.Id;
            Text = feedbackData.Text;
            Rating = feedbackData.Rating;
        }
#pragma warning restore CS8618 // Required by Entity Framework

    }
}
