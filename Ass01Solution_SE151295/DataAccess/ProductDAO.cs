using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetProductList()
        {
            List<Product> pro;
            try
            {
                var myProDB = new FStoreDBContext();
                pro = myProDB.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public Product GetProByID(int proID)
        {
            Product pro = null;
            try
            {
                var myProDB = new FStoreDBContext();
                pro = myProDB.Products.SingleOrDefault(pro => pro.ProductId == proID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public void AddProduct(Product pro)
        {
            try
            {
                Product Product = GetProByID(pro.ProductId);
                if (Product == null)
                {
                    var myProDB = new FStoreDBContext();
                    myProDB.Products.Add(pro);
                    myProDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The product is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product pro)
        {
            try
            {
                Product product = GetProByID(pro.ProductId);
                if (product != null)
                {
                    var myProDB = new FStoreDBContext();
                    myProDB.Entry<Product>(pro).State = EntityState.Modified;
                    myProDB.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveProduct(Product pro)
        {
            try
            {
                Product Product = GetProByID(pro.ProductId);
                if (Product != null)
                {
                    var myProDB = new FStoreDBContext();
                    myProDB.Products.Remove(pro);
                    myProDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
