using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProByID(int proId);
        void AddPro(Product product);
        void Delete(Product product);
        void UpdatePro(Product product);
        List<Category> GetCategories();
    }
}
