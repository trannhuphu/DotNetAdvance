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
        public MainWindow(IMemberRepository mainRepos, int role, Member mem)
        {
            InitializeComponent();

            if(role == 1)
            {
               tabMember.Content = new WindowMembers(mainRepos);
            }
            else 
            {
                tabMember.Content = new WindowMemberDetails(mainRepos,mem);
            }
        }
    }
}
