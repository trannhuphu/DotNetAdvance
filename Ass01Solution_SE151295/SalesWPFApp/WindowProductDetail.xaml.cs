using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowProductDetail.xaml
    /// </summary>
    public partial class WindowProductDetail : Window
    {
        public IProductRepository productRepositoryDetail { set; get; }
        public bool IsCreateProduct = false;

      
        public WindowProductDetail(IProductRepository productRepos, Product product, bool IsCreatePro = false)
        {
            InitializeComponent();
            productRepositoryDetail = productRepos;
            IsCreateProduct= IsCreatePro;
            if (product != null)
            {
                
                txtProductId.Text = product.ProductId.ToString();
                txtProductName.Text = product.ProductName.ToString();
                txtUnitPrice.Text = product.UnitPrice.ToString();
                txtUnitsInStock.Text = product.UnitsInStock.ToString();
                txtWeight.Text = product.Weight.ToString();
                txtCategoryId.Text = product.CategoryId.ToString();
            }
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var productNew = new Product
                {
                    ProductId = int.Parse(txtProductId.Text),
                    ProductName = txtProductName.Text,
                    UnitPrice = int.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitsInStock.Text),
                    Weight = txtWeight.Text,
                    CategoryId = int.Parse(txtCategoryId.Text)
                };
                if (IsCreateProduct)
                {
                    productRepositoryDetail.AddPro(productNew);
                }
                else
                {
                    productRepositoryDetail.UpdatePro(productNew);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Create/Update Information");
            }
        }

       
    }
}
