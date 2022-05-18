using BusinessObject.Models;
using DataAccess.Repository;
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
    /// Interaction logic for WindowOrderList.xaml
    /// </summary>
    public partial class WindowOrderList : Window
    {
        public IOrderRepository orderRepositoryDetail { set; get; }
        public bool IsCreateOrder = false;

        public WindowOrderList(IOrderRepository orderRepos, Order order, bool IsCreateOrd = false)
        {
            InitializeComponent();
            orderRepositoryDetail = orderRepos;
            IsCreateOrder = IsCreateOrd;
            if (order != null)
            {

                txtOrderId.Text = order.OrderId.ToString();
                txtMemberId.Text = order.MemberId.ToString();
                datetimeOrderDate.Text = order.OrderDate.ToString();
                datetimeRequiredDate.Text = order.RequiredDate.ToString();
                datetimeShippedDate.Text = order.ShippedDate.ToString();
                txtFreight.Text = order.Freight.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderNew = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = datetimeOrderDate.Value,
                    RequiredDate = datetimeRequiredDate.Value,
                    ShippedDate = datetimeShippedDate.Value,
                    Freight = decimal.Parse(txtFreight.Text)
                };
                if (IsCreateOrder)
                {
                    orderRepositoryDetail.AddOrd(orderNew);
                }
                else
                {
                    orderRepositoryDetail.UpdateOrd(orderNew);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Create/Update Information");
            }
        }
    }
}
