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
        public bool IsUpdateFlow { set; get; }
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

                txtProductId.IsReadOnly = true;
            }
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsValidated = true;
            try
            {
                txtProductId.Text = txtProductId.Text.Trim();
                txtProductName.Text = txtProductName.Text.Trim();
                txtUnitPrice.Text = txtUnitPrice.Text.Trim();
                txtUnitsInStock.Text = txtUnitsInStock.Text.Trim();
                txtWeight.Text = txtWeight.Text.Trim();
                txtCategoryId.Text = txtCategoryId.Text.Trim();

                if (txtProductId.Text == string.Empty || txtProductName.Text == string.Empty ||
                  txtUnitPrice.Text == string.Empty || txtUnitsInStock.Text == string.Empty ||
                  txtWeight.Text == string.Empty || txtCategoryId.Text == string.Empty)
                {
                    IsValidated = false;
                    MessageBox.Show("The fields can not be empty!", "ERROR");
                }

                if (IsValidated)
                {
                    var productNew = new Product
                    {
                        ProductId = int.Parse(txtProductId.Text),
                        ProductName = txtProductName.Text,
                        UnitPrice = decimal.Parse(txtUnitPrice.Text),
                        UnitsInStock = int.Parse(txtUnitsInStock.Text),
                        Weight = txtWeight.Text,
                        CategoryId = int.Parse(txtCategoryId.Text)
                    };

                    if (IsCreateProduct)
                    {
                        productRepositoryDetail.AddPro(productNew);
                        MessageBox.Show("Create succesfully", "CREATE");
                    }
                    else
                    {
                        productRepositoryDetail.UpdatePro(productNew);
                        MessageBox.Show("Update succesfully", "UPDATE");
                    }
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Create/Update Information");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
