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
        string[] characteristicsKeys = {"Екран", "Пам'ять", "Процесор", "Камера", "Акумулятор", "Дизайн", "Звук", "Ціна"};
        string[] categories = {"Laptops", "Smartphones", "Tablets", "Desktops", "Watches", "Headphones", "Speakers", "TVs"};
        string[] images = {"https://www.apple.com/v/macbook-air/s/images/overview/keyboard/magic_keyboard__cs7rk0m14pkm_large_2x.jpg",
        "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/iphone-13-pro-family-hero",
        "https://www.apple.com/v/ipad-pro/am/images/overview/hero/hero_combo__fcqcc3hbzjyy_large_2x.jpg",
        "https://www.apple.com/v/mac-studio/f/images/overview/hero/static_front__fmvxob6uyxiu_large_2x.jpg",
        "https://www.apple.com/v/watch/bk/images/overview/series-9/tile_s9_avail__c104b8nuoec2_small_2x.jpg",
        "http://apple.com/v/airpods-2nd-generation/f/images/overview/fall22c/engraving__ualb7ydsu1uq_small_2x.png",
        "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/homepod-select-202210?wid=1080&hei=1080&fmt=jpeg",
        "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/apple-tv-4k-hero-select-202210?wid=1076&hei=1070&fmt=jpeg"};


        string[] characteristics = {"Retina display", "Touch ID", "Face ID", "Apple M1 chip", "Always-on Retina display", "Active Noise Cancellation", "Spatial audio", "Dolby Atmos", "HomeKit", "Apple Music", "Touch Bar", "macOS", "iOS", "iPadOS", "watchOS", "tvOS"};
        var product = new ProductEntity
        {
            Name = names[Random.Shared.Next(0, names.Length)],
            Characteristics = Enumerable.Range(0, Random.Shared.Next(1, 10)).Select(_ => characteristicsKeys[Random.Shared.Next(0, characteristicsKeys.Length)] + ": " + characteristics[Random.Shared.Next(0, characteristics.Length)]).ToList(),
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
