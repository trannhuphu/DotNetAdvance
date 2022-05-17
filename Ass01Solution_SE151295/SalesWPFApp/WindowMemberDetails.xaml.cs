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
    /// Interaction logic for WindowMemberDetails.xaml
    /// </summary>
    public partial class WindowMemberDetails : Window
    {
        public IMemberRepository memberRepositoryDetail {set;get;}
        public bool IsCreateMember = false;
        public WindowMemberDetails(IMemberRepository memberRepos, Member member, bool IsCreateMem=false)
        {
            InitializeComponent();
            memberRepositoryDetail = memberRepos;
            IsCreateMember = IsCreateMem;
            if(member != null)
            {
                txtMemberId.Text = member.MemberId.ToString();
                txtCompanyName.Text = member.CompanyName.ToString();
                txtCity.Text = member.City.ToString();
                txtEmail.Text = member.Email.ToString();
                txtCountry.Text = member.Country.ToString();
                txtPassword.Text = member.Password.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try {
                var NewMember = new Member
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
                if(IsCreateMember)
                {
                    memberRepositoryDetail.AddMem(NewMember);
                }
                else
                {
                    memberRepositoryDetail.UpdateMem(NewMember);
                }
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Create/Update Information");
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
