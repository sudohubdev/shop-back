using shop_back.Server.Models;
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
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceOrdered { get; set; }
        public DateTime DateOrdered { get; set; }
        public string UserId { get; set; }
        public string LegalName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string PostOffice { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }

        public OrderEntity(OrderModel order, ProductEntity product, string userId)
        {
            ProductId = order.ProductId;
            Quantity = order.Quantity;
            LegalName = order.LegalName;
            PhoneNumber = order.PhoneNumber;
            City = order.City;
            PostOffice = order.PostOffice;
            DeliveryMethod = order.DeliveryMethod;
            PaymentMethod = order.PaymentMethod;
            //
            Product = product;
            PriceOrdered = product.Price * Quantity;
            DateOrdered = DateTime.Now;
            UserId = userId;
        }


#pragma warning disable CS8618 // Required by Entity Framework
        public OrderEntity() { }
#pragma warning restore CS8618 // Required by Entity Framework

    }
}
