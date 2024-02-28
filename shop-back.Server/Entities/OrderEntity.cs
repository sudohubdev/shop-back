using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


/*
товар
к-сть яку замовили
ціна на яку замовили
дата замовлення
id замовлення
id користувача який робив замовлення                                                                           
*/
namespace shop_back.Server.Entities
{
    public class OrderEntity : BaseEntity
    {
        [JsonIgnore]
        public ProductEntity Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceOrdered { get; set; }
        public string DateOrdered { get; set; }
        public int UserId { get; set; }
        

#pragma warning disable CS8618 // Required by Entity Framework
        public OrderEntity() { }
#pragma warning restore CS8618 // Required by Entity Framework

    }
}