using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProByID(int proId) => ProductDAO.INSTANCE.GetProByID(proId);
        public List<Product> GetProducts() => ProductDAO.INSTANCE.GetProductList();
        public void AddPro(Product product) => ProductDAO.INSTANCE.AddProduct(product);
        public void Delete(Product product) => ProductDAO.INSTANCE.RemoveProduct(product);
        public void UpdatePro(Product product) => ProductDAO.INSTANCE.UpdateProduct(product);
        public List<Category> GetCategories() => ProductDAO.INSTANCE.GetCategoryList();
    }
}
