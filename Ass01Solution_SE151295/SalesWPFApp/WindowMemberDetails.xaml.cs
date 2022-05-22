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
        public WindowMemberDetails(IMemberRepository memberRepos, Member member, bool IsCreateMem=false, bool IsAdmin = false)
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

                txtMemberId.IsReadOnly = true;
            }
            txtMemberId.IsReadOnly = (IsAdmin == false) ? true : false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsValidate = true;
            try {

                txtMemberId.Text = txtMemberId.Text.Trim();
                txtEmail.Text = txtEmail.Text.Trim();
                txtCompanyName.Text = txtCompanyName.Text.Trim();
                txtCity.Text = txtCity.Text.Trim();
                txtCountry.Text = txtCountry.Text.Trim();
                txtPassword.Text = txtPassword.Text.Trim();
                if (txtMemberId.Text == string.Empty || txtEmail.Text == string.Empty ||
                   txtCompanyName.Text == string.Empty || txtCity.Text == string.Empty ||
                   txtCountry.Text == string.Empty || txtPassword.Text == string.Empty)
                {
                    IsValidate = false;
                    MessageBox.Show("The fields can not be empty!", "ERROR");
                }

                if (IsValidate)
                {
                    var NewMember = new Member
                    {
                        MemberId = int.Parse(txtMemberId.Text),
                        Email = txtEmail.Text,
                        CompanyName = txtCompanyName.Text,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                        Password = txtPassword.Text
                    };
                    if (IsCreateMember)
                    {
                        memberRepositoryDetail.AddMem(NewMember);
                        MessageBox.Show("Create succesfully", "CREATE");
                    }
                    else
                    {
                        memberRepositoryDetail.UpdateMem(NewMember);
                        MessageBox.Show("Update succesfully", "UPDATE");
                    }
                    this.Close();
                }
            } catch (Exception ex)
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
