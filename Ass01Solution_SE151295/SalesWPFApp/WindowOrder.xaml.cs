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

        public bool IsAdminLoginGlobal = false;

        public Member memGlobal = null;

        public WindowOrder(IOrderRepository ord, bool IsAdminLogin=false, Member mem=null)
        {
            InitializeComponent();
            IsAdminLoginGlobal = IsAdminLogin;
            memGlobal = mem;
            orderRepository = ord;
            LoadOrderList();
        }

        /// <summary>
        /// Load order
        /// </summary>
        public void LoadOrderList()
        {
            dgvOrder.SelectedIndex = 0;
            if(IsAdminLoginGlobal)
            {
                dgvOrder.ItemsSource = orderRepository.GetOrders();
            }
            else
            {
                if(memGlobal != null)
                {
                    dgvOrder.ItemsSource = orderRepository.GetOrderListByMemID(memGlobal.MemberId);
                }
            }
        }

        /// <summary>
        /// Add event create order
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdminLoginGlobal)
            {
                memGlobal = null;
            }
            var orderCreate = new WindowOrderList(orderRepository, null, memGlobal, true, IsAdminLoginGlobal);
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
                var orderUpdate = new WindowOrderList(orderRepository, orderCurrent,memGlobal,false,IsAdminLoginGlobal);
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
                    OrderDate = datetimeOrderDate.SelectedDate.Value,
                    RequiredDate = datetimeRequiredDate.SelectedDate.Value,
                    ShippedDate = datetimeShippedDate.SelectedDate.Value,
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            OnWndOrderClosed();
        }

        public delegate void CloseWindowOrder();
        public event CloseWindowOrder _eventClosedOrder;
        public event CloseWindowOrder eventClosedOrder
        {
            add
            {
                _eventClosedOrder += value;
            }

            remove
            {
                _eventClosedOrder -= value;
            }
        }
        public void OnWndOrderClosed()
        {
            if (_eventClosedOrder != null)
            {
                _eventClosedOrder();
            }
        }
    }
}
