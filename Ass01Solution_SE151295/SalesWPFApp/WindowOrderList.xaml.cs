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
using DataAccess;
using BusinessObject.Models;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowOrderList.xaml
    /// </summary>
    public partial class WindowOrderList : Window
    {
        public IOrderRepository orderRepositoryDetail { set; get; }
        public bool IsCreateOrder = false;

        public WindowOrderList(IOrderRepository orderRepos, Order order, Member mem=null,bool IsCreateOrd = false, bool IsAdmin = false)
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
            else
            {
                if(mem != null)
                {
                    txtMemberId.Text = mem.MemberId.ToString();
                    txtMemberId.IsReadOnly = true;
                }
            }
            txtMemberId.IsReadOnly = (IsAdmin == false) ? true : false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsValidated = true;
            try
            {
                txtOrderId.Text = txtOrderId.Text.Trim();
                txtMemberId.Text = txtMemberId.Text.Trim();
                datetimeOrderDate.Text = datetimeOrderDate.Text.Trim();
                datetimeRequiredDate.Text = datetimeRequiredDate.Text.Trim();
                datetimeShippedDate.Text = datetimeShippedDate.Text.Trim();
                txtFreight.Text = txtFreight.Text.Trim();

                if (txtOrderId.Text == string.Empty || txtMemberId.Text == string.Empty ||
                 datetimeOrderDate.Text == string.Empty || datetimeRequiredDate.Text == string.Empty ||
                 datetimeShippedDate.Text == string.Empty || txtFreight.Text == string.Empty)
                {
                    IsValidated = false;
                    MessageBox.Show("The fields can not be empty!", "ERROR");
                }
                if (IsValidated)
                {
                    var orderNew = new Order
                    {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = datetimeOrderDate.SelectedDate.Value,
                    RequiredDate = datetimeRequiredDate.SelectedDate.Value,
                    ShippedDate = datetimeShippedDate.SelectedDate.Value,
                    Freight = decimal.Parse(txtFreight.Text)
                    };
                if (IsCreateOrder)
                    {
                    orderRepositoryDetail.AddOrd(orderNew);
                    MessageBox.Show("Create succesfully", "CREATE");
                    }
                else
                    {
                    orderRepositoryDetail.UpdateOrd(orderNew);
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
