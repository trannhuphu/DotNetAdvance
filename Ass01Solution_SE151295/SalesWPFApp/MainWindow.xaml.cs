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
               tabMember.Content = new WindowMembers(memberRepos);
               IsAdminLogin = true;
            }
            else 
            {
                tabMember.Content = new WindowUser(memberRepos,mem);
            }

            tabOrder.Content = new WindowOrder(orderRepos, IsAdminLogin, mem);
            tabProduct.Content = new WindowProduct(productRepos, IsAdminLogin);
        }
    }
}
