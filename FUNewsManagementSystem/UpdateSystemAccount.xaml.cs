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
    /// Interaction logic for UpdateSystemAccount.xaml
    /// </summary>
    public partial class UpdateSystemAccount : Window
    {
        private readonly ISystemAccountService iSystemAccountService;
        private SystemAccount _systemAccount;
        public UpdateSystemAccount(SystemAccount systemAccount)
        {
            InitializeComponent(); 
            iSystemAccountService = new SystemAccountService();
            _systemAccount = systemAccount;
            LoadAccountData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _systemAccount.AccountName = txtAccountName.Text;
            _systemAccount.AccountEmail = txtAccountEmail.Text;
            _systemAccount.AccountRole = (int)cboAccountRole.SelectedValue;
            _systemAccount.AccountPassword = txtAccountPassword.Password;
            if (!IsValidEmail(_systemAccount.AccountEmail))
            {
                MessageBox.Show("Email must have format: example@FUNewsManagement.org");
                return;
            }
            iSystemAccountService.UpdateSystemAccount(_systemAccount);
            this.Close();
            AccountManagement accountManagement = new AccountManagement();
            accountManagement.Show();
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

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAcountRole();
            LoadAccountData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AccountManagement accountManagement = new AccountManagement();
            accountManagement.Show();
        }

        private void LoadAccountData()
        {
            if (_systemAccount != null)
            {
                txtAccountID.Text = _systemAccount.AccountId.ToString();
                txtAccountName.Text = _systemAccount.AccountName;
                txtAccountEmail.Text = _systemAccount.AccountEmail;
                cboAccountRole.SelectedValue = _systemAccount.AccountRole;
                txtAccountPassword.Password = _systemAccount.AccountPassword;
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@FUNewsManagement\.org$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
