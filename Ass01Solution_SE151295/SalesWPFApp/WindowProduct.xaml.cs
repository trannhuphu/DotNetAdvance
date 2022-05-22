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

        public WindowProduct(IProductRepository pro, bool IsAdminLogin=false)
        {
            InitializeComponent();
            verifyPermission(IsAdminLogin);
            productRepository = pro;
            LoadProductList();
        }

        public void verifyPermission(bool IsAdminLogin)
        {
            if(IsAdminLogin)
            {
                btnCreate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }
            else
            {
                btnCreate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnUpdate.IsEnabled = false;
            }
        }

        public void LoadProductList()
        {
            dgvProduct.SelectedIndex = 0;
            dgvProduct.ItemsSource = productRepository.GetProducts();
           
        }

        public void WindowProductDetailsClosed(object sender, EventArgs e)
        {
            LoadProductList();
        }
        public void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var productNew = new WindowProductDetail(productRepository, null, true,true);
            productNew.Closed += WindowProductDetailsClosed;
            productNew.Show();
            //this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var proCurrent = productRepository.GetProByID(int.Parse(txtProductId.Text));
                var proUpdate = new WindowProductDetail(productRepository, proCurrent,false,true);
                proUpdate.Closed += WindowProductDetailsClosed;
                proUpdate.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input type is incorrect, please try again!", "Error Update");
            }
        }
        private Product GetProductObject()
        {
            Product pro = null;
            
                pro = new Product
                {
                    ProductId = int.Parse(txtProductId.Text),
                    ProductName = txtProductName.Text,
                    CategoryId = int.Parse(txtCategoryId.Text),
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitsInStock.Text)

                };
           
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
                MessageBox.Show("Product is not exist!", "Delete a product");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool IsSearch = true;
            var proList = productRepository.SearchProduct(txtSearchById.Text, txtSearchByName.Text,
                                                            txtSearchByPrice.Text, txtSearchByUnitsInStock.Text);
            if (proList != null)
            {
                if (proList.Count > 0)
                {
                    dgvProduct.ItemsSource = proList;
                }
                else
                {
                    IsSearch = false;
                }
            }
            else
            {
                IsSearch = false;
            }

            if (IsSearch == false)
            {

                MessageBox.Show("Product is not exist!", "Search");
            }

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProductList();
        }
    }
}
