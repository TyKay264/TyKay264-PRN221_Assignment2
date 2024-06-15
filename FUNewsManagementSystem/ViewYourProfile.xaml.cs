using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FUNewsManagementSystem
{
    /// <summary>
    /// Interaction logic for ViewYourProfile.xaml
    /// </summary>
    public partial class ViewYourProfile : Window
    {
        private readonly ISystemAccountService _accountService;


        public ViewYourProfile()
        {
            InitializeComponent();
            _accountService = new SystemAccountService();
            LoadAccount();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainMenu menu = new MainMenu();
            menu.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string accountId = txtAccountID.Text;
                string accountName = txtAccountName.Text;
                string accountEmail = txtAccountEmail.Text;
                string accountPassword = txtAccountPassword.Password;

                SystemAccount updatedAccount = new SystemAccount
                {
                    AccountId = Int16.Parse(accountId),
                    AccountName = accountName,
                    AccountEmail = accountEmail,
                    AccountPassword = accountPassword,
                    AccountRole = 1
                }; 
                if (!IsValidEmail(accountEmail))
                {
                    MessageBox.Show("Email must have format: example@FUNewsManagement.org");
                    return;
                }
                _accountService.UpdateSystemAccount(updatedAccount);

                MessageBox.Show("Account information updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating account information: " + ex.Message);
            }

        }

        private void LoadAccount()
        {
            SystemAccount account = _accountService.GetSystemAccountById(Login.UserId);
            if (account != null)
            {
                txtAccountID.Text = account.AccountId.ToString();
                txtAccountName.Text = account.AccountName;
                txtAccountEmail.Text = account.AccountEmail;
                txtAccountPassword.Password = account.AccountPassword;
                cboAccountRole.SelectedValue = account.AccountRole;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAcountRole();
            LoadAccount();
        }

        public void LoadAcountRole()
        {
            var accountRoles = new List<dynamic>();
            accountRoles.Add(new { AccountRoleId = 1, AccountRoleName = "Staff" });
            accountRoles.Add(new { AccountRoleId = 2, AccountRoleName = "Lecture" });
            cboAccountRole.ItemsSource = accountRoles;
            cboAccountRole.DisplayMemberPath = "AccountRoleName";
            cboAccountRole.SelectedValuePath = "AccountRoleId";
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@FUNewsManagement\.org$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
