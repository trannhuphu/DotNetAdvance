using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderRepository :IOrderRepository
    {
        public Order GetOrderByID(int ordId) => OrderDAO.Instance.GetOrderByID(ordId);
        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrderList();
        public IEnumerable<Order> GetOrderListByMemID(int memberId) => OrderDAO.Instance.GetOrderListByMemID(memberId);
        public void AddOrd(Order order) => OrderDAO.Instance.AddOrder(order);
        public void Delete(Order order) => OrderDAO.Instance.RemoveOrder(order);
        public void UpdateOrd(Order order) => OrderDAO.Instance.UpdateOrder(order);

    }
}
