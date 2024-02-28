using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public int UserId { get; set; }
        [JsonIgnore]
        public IdentityUser User { get; set; } = null!;
        public int ProductId { get; set; }
        [JsonIgnore]
        public ProductEntity Product { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        

#pragma warning disable CS8618 // Required by Entity Framework
        public FeedbackEntity() { }
#pragma warning restore CS8618 // Required by Entity Framework

    }
}