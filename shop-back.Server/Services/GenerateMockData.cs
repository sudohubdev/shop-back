//generate the following data:
// 2 identity users with default password "password"
// 1 prodct with feedback
// 1 product with order
// 1 product with feedback and order
// 1 prodcut without anything

using shop_back.Server.Data;
using shop_back.Server.Entities;

namespace shop_back.Server.Services;
// For Entity Framework
public static class GenerateMockData
{
    public static void Init(MainContext context)
    {
        //delete database
        //context.Database.EnsureDeleted();

        context.Database.EnsureCreated();
        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }
        context.Users.AddRange(
            new Microsoft.AspNetCore.Identity.IdentityUser
            {
                Email = "test@test.com",
                UserName = "test"
            },
            new Microsoft.AspNetCore.Identity.IdentityUser
            {
                Email = "test2@test.com",
                UserName = "test2"
            });
        context.SaveChanges();

        var product = new ProductEntity
        {
            Name = "TestProduct",
            Characteristics = new List<string> { "TestCharacteristic" },
            Category = "TestCategory",
            Discount = 0,
            Images = new List<string> { "TestImage" },
            IsHit = false,
            IsAvailable = true,
            Price = 100,
            Quantity = 10
        };
        context.Products.Add(product);
        context.SaveChanges();

        var feedback = new FeedbackEntity
        {
            Product = product,
            Text = "TestFeedback",
            UserId = 1,
            Rating = 5,
            ProductId = product.Id
        };
        context.Feedbacks.Add(feedback);
        context.SaveChanges();

        var order = new OrderEntity
        {
            Product = product,
            Quantity = 1,
            PriceOrdered = 100,
            DateOrdered = DateTime.Now,
            UserId = "1",
            LegalName = "Alice",
            PhoneNumber = "+1 234 567 890",
            City = "Kyiv",
            PostOffice = "1",
            DeliveryMethod = "Nova Poshta",
            PaymentMethod = "Cash"
        };
        context.Orders.Add(order);
        context.SaveChanges();

        var product2 = new ProductEntity
        {
            Name = "TestProduct2",
            Characteristics = new List<string> { "TestCharacteristic2" },
            Category = "TestCategory2",
            Discount = 0,
            Images = new List<string> { "TestImage2" },
            IsHit = false,
            IsAvailable = true,
            Price = 100,
            Quantity = 10
        };
        context.Products.Add(product2);
        context.SaveChanges();

        var feedback2 = new FeedbackEntity
        {
            Product = product2,
            Text = "TestFeedback2",
            UserId = 1
        };
        context.Feedbacks.Add(feedback2);
        context.SaveChanges();

        var order2 = new OrderEntity
        {
            Product = product2,
            Quantity = 1,
            PriceOrdered = 100,
            DateOrdered = DateTime.Now,
            UserId = "1",
            LegalName = "Alice",
            PhoneNumber = "+1 234 567 890",
            City = "Kyiv",
            PostOffice = "1",
            DeliveryMethod = "Nova Poshta",
            PaymentMethod = "Cash"
        };
        context.Orders.Add(order2);
        context.SaveChanges();

        var product3 = new ProductEntity
        {
            Name = "TestProduct3",
            Characteristics = new List<string> { "TestCharacteristic3" },
            Category = "TestCategory3",
            Discount = 0,
            Images = new List<string> { "TestImage3" },
            IsHit = false,
            IsAvailable = true,
            Price = 100,
            Quantity = 10
        };
        context.Products.Add(product3);
        context.SaveChanges();

    }
}
