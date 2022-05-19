﻿using System;
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
        IProductRepository wndProductRepositoryLogin = new ProductRepository();
        IOrderRepository wndOrderRepository = new OrderRepository();
        
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

                if(role == 1)
                {
                    var wndMain = new MainWindow( role, memberTemp, wndMemberRepositoryLogin, wndProductRepositoryLogin, wndOrderRepository);
                    wndMain.Show();
                }
                else 
                {
                    var wndMember = new WindowMemberDetails(wndMemberRepositoryLogin,memberTemp);
                    wndMember.Closed += WindowMemberDetailsClosed;
                    wndMember.Show();
                }
                this.Hide();
            } catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Login Error");
            }
        }
        private void WindowMemberDetailsClosed(Object sender, EventArgs e)
        {
            this.Show();
        }
        private void btnCancel_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
