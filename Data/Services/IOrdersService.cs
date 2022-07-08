using System.Collections.Generic;
using System.Threading.Tasks;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public  interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }

}