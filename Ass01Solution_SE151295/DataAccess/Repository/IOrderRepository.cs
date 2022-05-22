using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrderListByMemID(int memberId);
        Order GetOrderByID(int ordId);
        void AddOrd(Order order);
        void Delete(Order order);
        void UpdateOrd(Order order);
    }
}
