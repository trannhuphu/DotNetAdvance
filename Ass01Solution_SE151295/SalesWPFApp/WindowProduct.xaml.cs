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
    /// Interaction logic for WindowProduct.xaml
    /// </summary>
    public partial class WindowProduct : UserControl
    {
        public IProductRepository productRepository { set; get; }
        public WindowProduct(IProductRepository pro)
        {
            InitializeComponent();
            productRepository = pro;
            LoadProductList();
        }
        public void LoadProductList()
        {
            dgvProduct.ItemsSource = productRepository.GetProducts();
        }

        public void WindowProductDetailsClosed(object sender, EventArgs e)
        {
            LoadProductList();
        }
        public void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var productNew = new WindowProductDetail(productRepository, null);
            productNew.Show();
            //this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var proCurrent = productRepository.GetProByID(int.Parse(txtProductId.Text));
            var proUpdate = new WindowProductDetail(productRepository, proCurrent);
            proUpdate.Closed += WindowProductDetailsClosed;
            proUpdate.Show();
        }
        private Product GetProductObject()
        {
            Product pro = null;
            try
            {
                pro = new Product
                {
                    ProductId = int.Parse(txtProductId.Text),
                    ProductName = txtProductName.Text,
                    CategoryId = int.Parse(txtCategoryId.Text),
                    Weight = txtWeight.Text,
                    UnitPrice = int.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitsInStock.Text)

                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return pro;
        }
        /// <summary>
        /// Add event delete member
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                {
                    var pro = GetProductObject();
                    productRepository.Delete(pro);
                    LoadProductList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a product");
            }
        }
    }
}
