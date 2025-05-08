using Microsoft.EntityFrameworkCore;
using SkufMusic.Core.Services.OrderServices.OrderServicesInterfaces;
using SkufMusic.Data.Data;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly MusicStoreDbContext _db;

        public OrderService(MusicStoreDbContext db)
        {
            _db = db;
        }

        public async Task<Order> PlaceOrderAsync(int userId)
        {
            var cart = await _db.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.Items.Any()) throw new Exception("Корзина пуста");

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                IsConfirmed = false,
                Items = cart.Items.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            _db.Orders.Add(order);
            _db.CartItems.RemoveRange(cart.Items); // очищаем корзину
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetPendingOrdersAsync()
        {
            return await _db.Orders.Include(o => o.User).Where(o => !o.IsConfirmed).ToListAsync();
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null) throw new Exception("Заказ не найден");

            order.IsConfirmed = true;
            await _db.SaveChangesAsync();
        }
    }

}
