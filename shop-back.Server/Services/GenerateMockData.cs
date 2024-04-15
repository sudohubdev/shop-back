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
                UserName = "test",
                Id = "1"
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
    }

    public static void MakeProduct(MainContext ctx, bool inclOrder = false, bool inclFeedback = false){
        //fill real data
        ProductEntity[] productEntities = [
            new ProductEntity
            {
                Name = "MacBook Pro 16",
                Characteristics = new List<string> { "Екран: Retina display", "Пам'ять: 16 ГБ", "Процесор: Apple M1 Pro", "Камера: 1080p", "Акумулятор: 10000 мАч", "Дизайн: Space Gray", "Звук: Dolby Atmos"},
                Category = "Laptops",
                Discount = 0,
                Images = new List<string> {
                    "https://hotline.ua/img/tx/302/3023792065.jpg",
                    "https://macplanet.ua/images/product/10448/gallery/44654/original.jpg?_=3687564464",
                    "https://cdn.discordapp.com/attachments/1117119066772082708/1222672184586014880/1711578151404.jpg?ex=66171134&is=66049c34&hm=6ba37ee4fef78bac953fe254204c834763f17917a0098cd8482b9858a51ca941&",
                    "https://cdn.discordapp.com/attachments/1005813616039178260/1050108787660423310/rn_image_picker_lib_temp_02ab3724-32aa-45b4-9d4a-9acd70c9afdd.jpg?ex=6616d305&is=66045e05&hm=0d8ceecd78142149357ebfb76c1aeec7a31693d49214ab35ec2687d7ac9f4978&"

                 },
                IsHit = true,
                IsAvailable = true,
                Price = 90000,
                Quantity = 56
            },
            new ProductEntity
            {
                Name = "iPhone 13 Pro",
                Characteristics = new List<string> { "Екран: Super Retina XDR", "Пам'ять: 128 ГБ", "Процесор: Apple A15 Bionic", "Камера: 12 Мп", "Акумулятор: 3095 мАч", "Дизайн: Graphite", "Звук: Dolby Atmos"},
                Category = "Smartphones",
                Discount = 10,
                Images = new List<string> {
                    "https://scdn.comfy.ua/89fc351a-22e7-41ee-8321-f8a9356ca351/https://cdn.comfy.ua/media/catalog/product/i/p/iphone_13_q421_midnight_pdp_image_position-1a__ww-ru_1_.jpg/w_600",
                    "https://applehome.te.ua/wp-content/uploads/2021/09/iphone-13-starlight-select-2021.png",
                },
                IsHit = true,
                IsAvailable = true,
                Price = 40000,
                Quantity = 34
            },
            new ProductEntity
            {
                Name = "iPad Pro 12.9",
                Characteristics = new List<string> { "Екран: Liquid Retina XDR", "Пам'ять: 256 ГБ", "Процесор: Apple M1", "Камера: 12 Мп", "Акумулятор: 10000 мАч", "Дизайн: Silver", "Звук: Dolby Atmos"},
                Category = "Tablets",
                Discount = 0,
                Images = new List<string> {
                    "https://store.iland.ua/media/catalog/product/_/2/_2023-01-21_15.41.32_2_1_1.png?width=270&height=270&store=default&image-type=image",
                    "https://www.apple.com/v/ipad-pro/am/images/overview/hero/hero_combo__fcqcc3hbzjyy_large_2x.jpg"
                    },
                IsHit = true,
                IsAvailable = true,
                Price = 30000,
                Quantity = 45
            },
            new ProductEntity
            {
                Name = "iMac 27",
                Characteristics = new List<string> { "Екран: Retina 5K", "Пам'ять: 16 ГБ", "Процесор: Apple M1", "Камера: 1080p", "Акумулятор: 10000 мАч", "Дизайн: Silver", "Звук: Dolby Atmos"},
                Category = "Desktops",
                Discount = 0,
                Images = new List<string> { "https://cdsassets.apple.com/live/SZLF0YNV/images/sp/111913_sp821-imac-27.png" },
                IsHit = true,
                IsAvailable = true,
                Price = 70000,
                Quantity = 23
            },
            new ProductEntity
            {
                Name = "Apple Watch Series 9",
                Characteristics = new List<string> { "Екран: Always-on Retina display", "Пам'ять: 32 ГБ", "Процесор: Apple S9", "Камера: 12 Мп", "Акумулятор: 10000 мАч", "Дизайн: Space Gray", "Звук: Dolby Atmos"},
                Category = "Watches",
                Discount = 0,
                Images = new List<string> { "https://www.apple.com/v/watch/bk/images/overview/series-9/tile_s9_avail__c104b8nuoec2_small_2x.jpg" },
                IsHit = true,
                IsAvailable = true,
                Price = 20000,
                Quantity = 12
            },
            new ProductEntity
            {
                Name = "AirPods 2nd Generation",
                Characteristics = new List<string> {"Процесор: Apple H1", "Акумулятор: 2000 мАч", "Дизайн: White", "Звук: Active Noise Cancellation"},
                Category = "Headphones",
                Discount = 0,
                Images = new List<string> { "http://apple.com/v/airpods-2nd-generation/f/images/overview/fall22c/engraving__ualb7ydsu1uq_small_2x.png" },
                IsHit = true,
                IsAvailable = true,
                Price = 5000,
                Quantity = 67
            },
            new ProductEntity
            {
                Name = "HomePod",
                Characteristics = new List<string> { "Процесор: Apple A8","Акумулятор: 10000 мАч", "Дизайн: Black/White", "Звук: Spatial audio"},
                Category = "Speakers",
                Discount = 20,
                Images = new List<string> {
                     "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/homepod-select-202210?wid=1080&hei=1080&fmt=jpeg",
                     "https://s.ek.ua/jpg_zoom1/1128118.jpg"
                },
                IsHit = true,
                IsAvailable = true,
                Price = 10000,
                Quantity = 34
            },
            new ProductEntity
            {
                Name = "Apple TV 4K",
                Characteristics = new List<string> {  "Пам'ять: 16 ГБ", "Процесор: Apple A12", "Камера: 1080p", "Акумулятор: 10000 мАч", "Дизайн: Black", "Звук: Dolby Atmos"},
                Category = "TVs",
                Discount = 5,
                Images = new List<string> { "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/apple-tv-4k-hero-select-202210?wid=1076&hei=1070&fmt=jpeg" },
                IsHit = true,
                IsAvailable = true,
                Price = 15000,
                Quantity = 45
            },
            //some non Apple products. not hits of course
            new ProductEntity
            {
                Name = "Samsung Galaxy S22",
                Characteristics = new List<string> { "Екран: Dynamic AMOLED 2X", "Пам'ять: 128 ГБ", "Процесор: Exynos 2200", "Камера: 108 Мп", "Акумулятор: 5000 мАч", "Дизайн: Phantom Black", "Звук: Dolby Atmos"},
                Category = "Smartphones",
                Discount = 0,
                Images = new List<string> {
                    "https://scdn.comfy.ua/89fc351a-22e7-41ee-8321-f8a9356ca351/https://cdn.comfy.ua/media/catalog/product/s/m/sm-s901_galaxys22_front_green_211123_1.jpg/w_600",
                    "https://chekhol.com.ua/media/shop/be/e1/product-For-Samsung-Galaxy-S22-5G-TPU-Magsafe-Phone-Case-Transparent-_bee164b64dde7f2228e57d0e7d6223ed.jpg"
                 },
                IsHit = false,
                IsAvailable = true,
                Price = 35000,
                Quantity = 23
            },
            new ProductEntity
            {
                Name = "Xiaomi Redmi Note 11",
                Characteristics = new List<string> { "Екран: AMOLED", "Пам'ять: 64 ГБ", "Процесор: Snapdragon 680", "Камера: 48 Мп", "Акумулятор: 5000 мАч", "Дизайн: Onyx Gray", "Звук: 2 стереодинаміки"},
                Category = "Smartphones",
                Discount = 40,
                Images = new List<string> { "https://content.rozetka.com.ua/goods/images/original/355932993.jpg" },
                IsHit = false,
                IsAvailable = true,
                Price = 15000,
                Quantity = 45
            },
            new ProductEntity
            {
                Name = "GeForce RTX 3080",
                Characteristics = new List<string> { "Пам'ять: 10 ГБ", "Частота: 1440 МГц", "Шина: 320 біт", "Підключення: PCI-E", "Охолодження: 3 вентилятора", "Довжина: 320 мм"},
                Category = "PC",
                Discount = 0,
                Images = new List<string> { "https://hotline.ua/img/tx/296/2961181605.jpg" },
                IsHit = false,
                IsAvailable = true,
                Price = 50000,
                Quantity = 12
            },
            new ProductEntity
            {
                Name = "AMD Ryzen 9 5900X",
                Characteristics = new List<string> { "Ядра: 12", "Потоки: 24", "Частота: 3.7 ГГц", "Кеш: 70 МБ", "Техпроцес: 7 нм", "Підключення: AM4", "Охолодження: Wraith Prism"},
                Category = "PC",
                Discount = 0,
                Images = new List<string> { "https://hotline.ua/img/tx/239/2396483215.jpg" },
                IsHit = false,
                IsAvailable = false,
                Price = 30000,
                Quantity = 0
            },
            new ProductEntity
            {
                Name = "Дорогуща зарядка від айфона",
                Characteristics = new List<string> { "Потужність: 20 Вт", "Підключення: USB-C", "Колір: White", "Заряджає: iPhone, iPad, MacBook Pro"},
                Category = "Accessories",
                Discount = 0,
                Images = new List<string> { "https://m.media-amazon.com/images/I/61VHoMGP46L._SL1500_.jpg" },
                IsHit = true,
                IsAvailable = true,
                Price = 8000,
                Quantity = 34
            },
            new ProductEntity
            {
                Name = "Apple протирка для екрану",
                Characteristics = new List<string> { "Кількість: 1 штука", "Розмір: 10x10 см", "Матеріал: мікрофібра", "Колір: White"},
                Category = "Accessories",
                Discount = 10,
                Images = new List<string> { "https://content2.rozetka.com.ua/goods/images/big/230711770.jpg" },
                IsHit = false,
                IsAvailable = true,
                Price = 999,
                Quantity = 56
            },
            new ProductEntity
            {
                Name = "Windows 11 Pro LTSC",
                Characteristics = new List<string> { "Версія: 21H2", "Тип: OEM", "Мова: Українська", "Активація: 1 ПК", "Підтримка: 10 років", "Доставка: поштою"},
                Category = "Accessories",
                Discount = 50,
                Images = new List<string> { "https://i0.wp.com/officedigital.io/wp-content/uploads/2021/10/win11pro-officedigital-2021.png?w=999&ssl=1" },
                IsHit = false,
                IsAvailable = true,
                Price = 5000,
                Quantity = 23
            },
            new ProductEntity
            {
                Name = "Клавіатура Apple Magic Keyboard",
                Characteristics = new List<string> { "Підключення: Bluetooth", "Колір: Space Gray", "Матеріал: алюміній", "Комплектація: клавіатура, кабель", "Сумісність: Mac, iPad, iPhone"},
                Category = "Accessories",
                Discount = 0,
                Images = new List<string> {
                    "https://www.apple.com/v/macbook-air/s/images/overview/keyboard/magic_keyboard__cs7rk0m14pkm_large_2x.jpg",
                    "https://www.cnet.com/a/img/resize/6c1763521926548640c120c63c423d5c38acb938/hub/2022/03/15/e1ef0b2c-d23d-4aa1-9a6f-b45b1cddb4e7/ipad-air-2022-018-copy.jpg?auto=webp&fit=crop&height=675&width=1200",
                    "https://www.istore.ua/upload/iblock/b7c/xe22estet61a1agvpyvcazwlt7kq74tj/MQDP3_4_is.png",
                 },
                IsHit = true,
                IsAvailable = true,
                Price = 8000,
                Quantity = 45
            }
        ];


        foreach (var product in productEntities)
        {
            ctx.Products.Add(product);
        }
        ctx.SaveChanges();

        KeyValuePair<int,string>[] Feedbacks = [
            new KeyValuePair<int, string>(1, "Ніфіга не працює"),
            new KeyValuePair<int, string>(2, "Довго возився з налаштуваннями, але попався брак. Не раджу купувати"),
            new KeyValuePair<int, string>(3, "За таку ціну воно має само вмикатися. Сяомі топ а Apple - гівно"),
            new KeyValuePair<int, string>(4, "Купив для дружини, вона в захваті"),
            new KeyValuePair<int, string>(5, "Крута штука, завжди мріяв обзавестись"),
            //
            new KeyValuePair<int, string>(5, "Дуже гарний продукт, якісно зібраний"),
            new KeyValuePair<int, string>(3, "Не варто своїх грошей"),
            new KeyValuePair<int, string>(4, "Доставка була пізня, слава богу, що все прийшло ціле"),
            new KeyValuePair<int, string>(2, "Не працює, поміняв на інший самсунг після тижня"),
            new KeyValuePair<int, string>(5, "Після 2 місяців використання відмінно працює"),
            new KeyValuePair<int, string>(3, "Замовлення прийшло з відкритою упаковкою, незадоволений"),

            new KeyValuePair<int, string>(1, "Недоліки переважають переваги, не рекомендую"),
            new KeyValuePair<int, string>(4, "Після оновлення програмного забезпечення виникли проблеми з роботою"),
            new KeyValuePair<int, string>(2, "Не відповідає заявленим характеристикам, розчарування"),
            new KeyValuePair<int, string>(5, "Продукт відповідає опису, задоволений покупкою"),
            //
            new KeyValuePair<int, string>(4, "Гарний вибір за ці гроші, рекомендую"),
            new KeyValuePair<int, string>(3, "Очікував кращої якості матеріалів, трошки розчарований"),
            new KeyValuePair<int, string>(2, "Продукт не сумісний з моїм пристроєм, не вказано в описі"),
            new KeyValuePair<int, string>(1, "Погана упаковка, товар пошкоджений під час доставки"),
            new KeyValuePair<int, string>(5, "Все працює на відмінно, дякую за швидку доставку"),
            new KeyValuePair<int, string>(1, "Продукт виявився непрацездатним, розчарування."),

            new KeyValuePair<int, string>(3, "Значно переплатив за цей товар, очікував кращого."),
            new KeyValuePair<int, string>(4, "Не був задоволений якістю товару, але обслуговування клієнтів вражає."),
            new KeyValuePair<int, string>(2, "Замовив, але товар так і не прийшов. Рекомендую шукати інший магазин."),
            new KeyValuePair<int, string>(5, "Продукт виправдав очікування, відмінно працює."),
            //
            new KeyValuePair<int, string>(4, "Після короткого часу використання знайшов дефект, не задоволений."),
            new KeyValuePair<int, string>(2, "Розчарований якістю товару, не раджу придбавати."),
            new KeyValuePair<int, string>(5, "Товар досяг моїх очікувань, задоволений покупкою."),
            new KeyValuePair<int, string>(3, "Не можу налаштувати товар, інструкція неясна."),
            new KeyValuePair<int, string>(4, "Були проблеми з доставкою, але отримав компенсацію за затримку.")
        ];

        if (inclFeedback)
        {
            //choose random product from Feedbacks
            for (int i = 0; i < productEntities.Length * 2; i++)
            {
                KeyValuePair<int, string> feedback = Feedbacks[Random.Shared.Next(0, Feedbacks.Length)];
                var feedbackEntity = new FeedbackEntity
                {
                    ProductId = Random.Shared.Next(1, productEntities.Length),
                    Text = feedback.Value,
                    Rating = feedback.Key,
                    UserId = "1"
                };
                ctx.Feedbacks.Add(feedbackEntity);
                ctx.SaveChanges();
            }

        }
        if (inclOrder)
        {
            var order = new OrderEntity
            {
                Product = productEntities[0],
                Quantity = Random.Shared.Next(1, 10),
                PriceOrdered = productEntities[0].Price * Random.Shared.Next(1, 10),
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
