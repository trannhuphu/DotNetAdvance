using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetOrderList()
        {
            List<Order> ord;
            try
            {
                var myOrderDB = new FStoreDBContext();
                ord = myOrderDB.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ord;
        }

        public IEnumerable<Order> GetOrderListByMemID(int memberId)
        {
            List<Order> ord;
            try
            {
                var myOrderDB = new FStoreDBContext();
                ord = myOrderDB.Orders.Where(tmp => tmp.MemberId == memberId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ord;
        }

        public Order GetOrderByID(int ordID)
        {
            Order ord  = null;
            try
            {
                var myOrderDB = new FStoreDBContext();
                ord  = myOrderDB.Orders.SingleOrDefault(ord  => ord.OrderId == ordID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ord;
        }

        public void AddOrder(Order ord)
        {
            try
            {
                Order order = GetOrderByID(ord .OrderId);
                if (order == null)
                {
                    var myOrderDB = new FStoreDBContext();
                    myOrderDB.Orders.Add(ord );
                    myOrderDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The order is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(Order ord )
        {
            try
            {
                Order order = GetOrderByID(ord .OrderId);
                if (order != null)
                {
                    var myOrderDB = new FStoreDBContext();
                    myOrderDB.Entry<Order>(ord).State = EntityState.Modified;
                    myOrderDB.SaveChanges();
                }
             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveOrder(Order ord )
        {
            try
            {
                Order Order = GetOrderByID(ord .OrderId);
                if (Order != null)
                {
                    var myOrderDB = new FStoreDBContext();
                    myOrderDB.Orders.Remove(ord );
                    myOrderDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
