using SkufMusic.Core.Services.CartServices;
using SkufMusic.Core.Services.OrderServices;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Test.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        [TestMethod]
        public async Task PlaceOrderAsync_CreatesOrder()
        {
            var db = TestHelper.GetInMemoryDbContext("OrderTestDb");
            var user = new User
            {
                Username = "user1",
                PasswordHash = "123",
                Role = UserRole.Customer,
                Cart = new Cart()
            };
            var product = new Product { 
                Name = "Amp Peavey 5150",
                Description = "Heavy metall amp",
                Price = 150 };
            db.Users.Add(user);
            db.Products.Add(product);
            db.SaveChanges();

            var cartService = new CartService(db);
            await cartService.AddToCartAsync(user.Id, product.Id, 1);

            var orderService = new OrderService(db);
            var order = await orderService.PlaceOrderAsync(user.Id);

            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.Items.Count);
            Assert.AreEqual(product.Id, order.Items.ToList()[0].ProductId);
        }

        [TestMethod]
        public async Task ConfirmOrderAsync_SetsIsConfirmed()
        {
            var db = TestHelper.GetInMemoryDbContext("ConfirmOrderDb");
            var user = new User
            {
                Username = "confirmer",
                PasswordHash = "123",
                Role = UserRole.Customer,
                Cart = new Cart()
            };
            var order = new Order { 
                User = user, 
                IsConfirmed = false 
            };
            db.Orders.Add(order);
            db.SaveChanges();

            var orderService = new OrderService(db);
            await orderService.ConfirmOrderAsync(order.Id);

            var updated = await db.Orders.FindAsync(order.Id);
            Assert.IsTrue(updated.IsConfirmed);
        }
    }

}
