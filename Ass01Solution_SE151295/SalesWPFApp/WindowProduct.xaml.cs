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
    public partial class WindowProduct : Window
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

        public void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var productNew = new WindowProductDetail(productRepository, null);
            productNew.Show();
            this.Close();
        }
    }
}
