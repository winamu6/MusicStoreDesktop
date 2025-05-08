using Microsoft.EntityFrameworkCore;
using SkufMusic.Core.Services.CartServices.CartServicesInterfaces;
using SkufMusic.Data.Data;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly MusicStoreDbContext _db;

        public CartService(MusicStoreDbContext db)
        {
            _db = db;
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null) throw new Exception("Cart not found");

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });

            await _db.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int userId)
        {
            var cart = await _db.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            return cart?.Items.ToList() ?? new List<CartItem>();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart != null)
            {
                _db.CartItems.RemoveRange(cart.Items);
                await _db.SaveChangesAsync();
            }
        }
    }

}
