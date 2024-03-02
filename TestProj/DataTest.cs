//create a product for admin user with a feedback and order

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop_back.Server.Data;
using shop_back.Server.Entities;
using shop_back.Server.Services;

namespace shop_back.Server.Tests;

[TestClass]
public class DataTest
{
    private static MainContext? mainContext;
    public DataTest()
    {
        if (mainContext == null)
        {
            //change cwd to the project root
            Directory.SetCurrentDirectory("../../..");
            //change solution root for the factory

            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddJsonFile("appsettings.Test.json");
            InitDB.Init(builder);

            mainContext = builder.Build().Services.GetRequiredService<MainContext>();
            //delete database
            mainContext.Database.EnsureDeleted();
            //ensurecreated
            mainContext.Database.EnsureCreated();
        }
    }
    [TestMethod]
    public async Task CreateProduct()
    {
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
        mainContext!.Products.Add(product);
        await mainContext!.SaveChangesAsync();
        Assert.IsTrue(mainContext!.Products.Any(p => p.Name == "TestProduct"));
    }
    [TestMethod]
    public async Task CreateFeedback()
    {
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
        mainContext!.Products.Add(product);
        await mainContext!.SaveChangesAsync();
        var feedback = new FeedbackEntity
        {
            Product = product,
            Text = "TestFeedback",
            UserId = 1
        };
        mainContext!.Feedbacks.Add(feedback);
        await mainContext!.SaveChangesAsync();
        Assert.IsTrue(mainContext!.Feedbacks.Any(f => f.Text == "TestFeedback"));
        //get feedback's product
        var aaa = mainContext.Products.Include(p => p.Feedbacks).First();
        Assert.IsTrue(aaa.Feedbacks.Any(f => f.Text == "TestFeedback"));
    }

}

