using SkufMusic.Core.Services.CartServices;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Test.Services
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public async Task AddToCartAsync_AddsItem()
        {
            var db = TestHelper.GetInMemoryDbContext("CartTestDb");
            var user = new User
            {
                Username = "cartuser",
                PasswordHash = "123",
                Role = UserRole.Customer,
                Cart = new Cart()
            };
            var product = new Product { 
                Name = "Drums",
                Description = "Acoustic drum set",
                Price = 300, 
                Stock = 10 };

            db.Users.Add(user);
            db.Products.Add(product);
            db.SaveChanges();

            var service = new CartService(db);
            await service.AddToCartAsync(user.Id, product.Id, 2);

            var items = await service.GetCartItemsAsync(user.Id);
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(2, items[0].Quantity);
        }

        [TestMethod]
        public async Task ClearCartAsync_RemovesItems()
        {
            var db = TestHelper.GetInMemoryDbContext("ClearCartDb");
            var user = new User
            {
                Username = "clearuser",
                PasswordHash = "123",
                Role = UserRole.Customer,
                Cart = new Cart()
            };
            var product = new Product { 
                Name = "Mic",
                Description = "Cool micro",
                Price = 100 
            };

            db.Users.Add(user);
            db.Products.Add(product);
            db.SaveChanges();

            var cartService = new CartService(db);
            await cartService.AddToCartAsync(user.Id, product.Id, 1);
            await cartService.ClearCartAsync(user.Id);

            var items = await cartService.GetCartItemsAsync(user.Id);
            Assert.AreEqual(0, items.Count);
        }
    }

}
