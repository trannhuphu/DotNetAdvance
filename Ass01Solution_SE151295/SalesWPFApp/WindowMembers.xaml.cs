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
    /// Interaction logic for WindowMembers.xaml
    /// </summary>
    public partial class WindowMembers : Window
    {
        public IMemberRepository wndMemberRepository {set ; get;}
        public WindowMembers(IMemberRepository member)
        {
            InitializeComponent();
            wndMemberRepository = member;
            LoadMemberList();
        }
        public void LoadMemberList(){
            dgvMember.ItemsSource=wndMemberRepository.GetMembers();
        }

        public void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var wndNewMem = new WindowMemberDetails(wndMemberRepository, null);
            wndNewMem.Show();
            this.Close();
        }
    }
}
