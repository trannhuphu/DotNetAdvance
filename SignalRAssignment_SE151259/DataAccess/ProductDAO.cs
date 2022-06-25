using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO INSTANCE
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

        public List<Product> GetProductList()
        {
            List<Product> pro;
            var myProDB = new FStoreDBContext();
            pro = myProDB.Products
                .Include(o => o.Category)
                .ToList();
            return pro;
        }

        public Product GetProByID(int proID)
        {
            Product pro = null;
            var myProDB = new FStoreDBContext();
            pro = myProDB.Products
                .Include(o => o.Category)
                .SingleOrDefault(pro => pro.ProductId == proID);        
            return pro;
        }

        public void AddProduct(Product pro)
        {
            var myProDB = new FStoreDBContext();
            myProDB.Products.Add(pro);
            myProDB.SaveChanges();
        }

        public void UpdateProduct(Product pro)
        {
            var myProDB = new FStoreDBContext();
            myProDB.Entry<Product>(pro).State = EntityState.Modified;
            myProDB.SaveChanges();
        }

        public void RemoveProduct(Product pro)
        {
           var myProDB = new FStoreDBContext();
           myProDB.Products.Remove(pro);
           myProDB.SaveChanges();
        }

        public List<Category> GetCategoryList()
        {
            List<Category> Category;
            var myProDB = new FStoreDBContext();
            Category = myProDB.Categories
                .ToList();
            return Category;
        }
    }
}
