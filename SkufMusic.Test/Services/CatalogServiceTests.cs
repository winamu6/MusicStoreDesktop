using SkufMusic.Core.Services.CatalogServices;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Test.Services
{
    [TestClass]
    public class CatalogServiceTests
    {
        [TestMethod]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            var db = TestHelper.GetInMemoryDbContext("CatalogTestDb");
            db.Products.AddRange(new List<Product>
        {
            new Product { Name = "Guitar", Description = "Electric", Price = 1000, Stock = 5 },
            new Product { Name = "Piano", Description = "Digital", Price = 2000, Stock = 3 }
        });
            db.SaveChanges();

            var service = new CatalogService(db);
            var products = await service.GetAllProductsAsync();

            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public async Task SearchProductsAsync_ReturnsMatching()
        {
            var db = TestHelper.GetInMemoryDbContext("SearchTestDb");
            db.Products.AddRange(new List<Product>
        {
            new Product { Name = "Guitar", Description = "Acoustic", Price = 500 },
            new Product { Name = "Violin", Description = "String", Price = 400 }
        });
            db.SaveChanges();

            var service = new CatalogService(db);
            var result = await service.SearchProductsAsync("Guitar");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Guitar", result[0].Name);
        }
    }
}
