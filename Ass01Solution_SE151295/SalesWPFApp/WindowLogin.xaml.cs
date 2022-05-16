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
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    /// 

    public partial class WindowLogin : Window
    {
        IMemberRepository wndMemberRepositoryLogin = new MemberRepository();
        IProductRepository product = new ProductRepository();
        
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            int role = 0;
            try {
                Member memberTemp = new Member();
                role = wndMemberRepositoryLogin.Login(txtEmail.Text, txtPassword.Text, ref memberTemp);
                var wndMain = new MainWindow(wndMemberRepositoryLogin, role, memberTemp);
                wndMain.Show();
                // if(role == 1)
                // {
                //    var wndMember = new WindowMembers(wndMemberRepositoryLogin);
                //    wndMember.Show();
                // }
                // else 
                // {
                //     var wndMember = new WindowMemberDetails(wndMemberRepositoryLogin,memberTemp);
                //     wndMember.Show();
                // }
                this.Close();
            } catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Login Error");
            }
        }
    }
}
