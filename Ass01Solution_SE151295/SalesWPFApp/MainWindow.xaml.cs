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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess;
using BusinessObject.Models;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(int role, Member mem, IMemberRepository memberRepos=null, 
        IProductRepository productRepos=null, IOrderRepository orderRepos=null)
        {
            InitializeComponent();
            bool IsAdminLogin = false;

            if(role == 1)
            {
               WindowMembers wndMem =  new WindowMembers(memberRepos);
               wndMem.eventClosedMem += CloseWindowMain;
               tabMember.Content = wndMem;
               IsAdminLogin = true;
            }
            else 
            {
                WindowUser wndUser = new WindowUser(memberRepos, mem);
                wndUser.eventClosedUser += CloseWindowMain;
                tabMember.Content = wndUser;
            }

            WindowOrder wndOrder = new WindowOrder(orderRepos, IsAdminLogin, mem);
            WindowProduct wndProduct = new WindowProduct(productRepos, IsAdminLogin);

            wndOrder.eventClosedOrder += CloseWindowMain;
            wndProduct.eventClosedPro += CloseWindowMain;

            tabOrder.Content = wndOrder;
            tabProduct.Content = wndProduct;
        }

        public void CloseWindowMain()
        {
            this.Close();
        }
    }
}
