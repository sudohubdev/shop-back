//generate the following data:
// 2 identity users with default password "password"
// 1 prodct with feedback
// 1 product with order
// 1 product with feedback and order
// 1 prodcut without anything

using shop_back.Server.Data;
using shop_back.Server.Entities;
using shop_back.Server.Models;

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
            },
            new Microsoft.AspNetCore.Identity.IdentityUser
            {
                Email = "test3@test.com",
                UserName = "test3"
            },
            new Microsoft.AspNetCore.Identity.IdentityUser
            {
                Email = "test4@test.com",
                UserName = "test4"
            }

        );
        context.SaveChanges();
        MakeProduct(context, true, true);
        MakeProduct(context, true, false);
        MakeProduct(context, false, true);
        MakeProduct(context, false, false);

        for (int i = 0; i < 20; i++)
        {
            MakeProduct(context, false, Random.Shared.Next(0, 2) == 1);
        }
    }

    public static void MakeProduct(MainContext ctx, bool inclOrder = false, bool inclFeedback = false){
        string[] names = {"MacBook", "iPhone", "iPad", "iMac", "Apple Watch", "AirPods", "HomePod", "Apple TV"};
        string[] categories = {"Laptops", "Smartphones", "Tablets", "Desktops", "Watches", "Headphones", "Speakers", "TVs"};
        string[] images = {"macbook.jpg", "iphone.jpg", "ipad.jpg", "imac.jpg", "applewatch.jpg", "airpods.jpg", "homepod.jpg", "appletv.jpg"};
        string[] characteristics = {"Retina display", "Touch ID", "Face ID", "Apple M1 chip", "Always-on Retina display", "Active Noise Cancellation", "Spatial audio", "Dolby Atmos"};
        var product = new ProductEntity
        {
            Name = names[Random.Shared.Next(0, names.Length)],
            Characteristics = new List<string> { characteristics[Random.Shared.Next(0, characteristics.Length)] },
            Category = categories[Random.Shared.Next(0, categories.Length)],
            Discount = Random.Shared.Next(0, 100),
            Images = new List<string> { images[Random.Shared.Next(0, images.Length)] },
            IsHit = Random.Shared.Next(0, 2) == 1,
            IsAvailable = Random.Shared.Next(0, 10) > 0,
            Price = Random.Shared.Next(100, 10000),
            Quantity = Random.Shared.Next(0, 100)
        };
        ctx.Products.Add(product);
        ctx.SaveChanges();
        if (inclFeedback)
        {
            var rngActiveUsr = ctx.Users.ToArray()[Random.Shared.Next(0, ctx.Users.Count())];
            var feedbackM = new FeedbackModel
            {
                Text = characteristics[Random.Shared.Next(0, characteristics.Length)] + " is an awesome feature of " + product.Name,
                Rating = Random.Shared.Next(1, 6),
                ProductId = product.Id
            };
            var feedback = new FeedbackEntity(feedbackM, product, rngActiveUsr.Id);
            ctx.Feedbacks.Add(feedback);
            ctx.SaveChanges();
        }
        if (inclOrder)
        {
            var order = new OrderEntity
            {
                Product = product,
                Quantity = Random.Shared.Next(1, 10),
                PriceOrdered = product.Price * Random.Shared.Next(1, 10),
                DateOrdered = DateTime.Now,
                UserId = "1",
                LegalName = "Alice",
                PhoneNumber = "+1 234 567 890",
                City = "Kyiv",
                PostOffice = "1",
                DeliveryMethod = "Nova Poshta",
                PaymentMethod = "Cash"
            };
            ctx.Orders.Add(order);
            ctx.SaveChanges();
        }
    }
}
