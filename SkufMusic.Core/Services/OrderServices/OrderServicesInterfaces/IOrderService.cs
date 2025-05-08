using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.OrderServices.OrderServicesInterfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(int userId);
        Task<List<Order>> GetPendingOrdersAsync();
        Task ConfirmOrderAsync(int orderId);
    }

}
