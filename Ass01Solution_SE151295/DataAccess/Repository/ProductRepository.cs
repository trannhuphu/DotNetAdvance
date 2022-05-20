using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProByID(int proId) => ProductDAO.Instance.GetProByID(proId);
        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProductList();
        public void AddPro(Product product) => ProductDAO.Instance.AddProduct(product);
        public void Delete(Product product) => ProductDAO.Instance.RemoveProduct(product);
        public void UpdatePro(Product product) => ProductDAO.Instance.UpdateProduct(product);

        public List<Product> SearchProduct(string productDataId, string productName, string price, string unitStock)
         => ProductDAO.Instance.Search(productDataId, productName, price, unitStock);
    }
}
