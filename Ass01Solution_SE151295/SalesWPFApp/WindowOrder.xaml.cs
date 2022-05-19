using System;
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
using DataAccess;
using BusinessObject.Models;



namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : UserControl
    {
        public IOrderRepository orderRepository { set; get; }
        public WindowOrder(IOrderRepository ord)
        {
            InitializeComponent();
            orderRepository = ord;
            LoadOrderList();
        }
        /// <summary>
        /// Load order
        /// </summary>
        public void LoadOrderList()
        {
            dgvOrder.SelectedIndex = 0;
            dgvOrder.ItemsSource = orderRepository.GetOrders();
        }

        /// <summary>
        /// Add event create order
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var orderCreate = new WindowOrderList(orderRepository, null, true);
            orderCreate.Closed += WindowOrderListClosed;
            orderCreate.Show();
        }
        /// <summary>
        /// Add event for closed windown OrderList
        /// </summary>
        public void WindowOrderListClosed(object sender, EventArgs e)
        {
            LoadOrderList();
        }

        /// <summary>
        /// Add event update order
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderCurrent = orderRepository.GetOrderByID(int.Parse(txtOrderId.Text));
                var orderUpdate = new WindowOrderList(orderRepository, orderCurrent);
                orderUpdate.Closed += WindowOrderListClosed;
                orderUpdate.Show();
            }catch (Exception ex)
            {
                MessageBox.Show("Input type is incorrect, please try again!", "Error Update");
            }
        }
        /// <summary>
        /// Implement get member
        /// </summary>
        private Order GetOrderObject()
        {
            Order ord = null;
            
                ord = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                  //  OrderDate = datetimeOrderDate.Value,
                  //  RequiredDate = datetimeRequiredDate.Value,
                  //  ShippedDate = datetimeShippedDate.Value,
                    Freight = decimal.Parse(txtFreight.Text)

                };
            
            return ord;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // if (MessageBox.Show("Are you sure?", "Member Management - Delete",
                // MessageBoxButton.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    var order = GetOrderObject();
                    orderRepository.Delete(order);
                    LoadOrderList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Order does not exist", "Delete a order");
            }
        }
    }
}
