using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.CartServices.CartServicesInterfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task ClearCartAsync(int userId);
    }

}
