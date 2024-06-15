using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Services;
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

namespace FUNewsManagementSystem
{
    /// <summary>
    /// Interaction logic for AccountManagement.xaml
    /// </summary>
    public partial class AccountManagement : Window
    {
        private readonly ISystemAccountService iSystemAccountService;
        public AccountManagement()
        {
            InitializeComponent();
            iSystemAccountService = new SystemAccountService();
            int userRole = Login.UserRole;
            // resetInput();

            if (userRole == 3)
            {
                btnCreate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnCreate.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                dgData.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateSystemAccount createSystemAccount = new CreateSystemAccount();
            createSystemAccount.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedSystemAccount = dgData.SelectedItem as SystemAccount;
                if (selectedSystemAccount != null)
                {
                    selectedSystemAccount.AccountName = txtAccountName.Text;
                    selectedSystemAccount.AccountEmail = txtAccountEmail.Text;
                    if (cboAccountRole.SelectedItem != null)
                    {
                        selectedSystemAccount.AccountRole = Int32.Parse(cboAccountRole.SelectedValue.ToString());
                    }
                    selectedSystemAccount.AccountPassword = txtAccountPassword.Password;
                    selectedSystemAccount.AccountId = Int16.Parse(txtAccountID.Text.ToString());
                    iSystemAccountService.DeleteSystemAccount(selectedSystemAccount);
                    LoadSystemAccount();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadSystemAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedAccount = dgData.SelectedItem as SystemAccount;
                if (selectedAccount != null && Login.UserRole == 3)
                {
                    selectedAccount.AccountName = txtAccountName.Text;
                    selectedAccount.AccountEmail = txtAccountEmail.Text;
                    if (cboAccountRole.SelectedItem != null)
                    {
                        selectedAccount.AccountRole = Int32.Parse(cboAccountRole.SelectedValue.ToString());
                    }
                    selectedAccount.AccountPassword = txtAccountPassword.Password;

                   // iSystemAccountService.UpdateSystemAccount(selectedAccount);
                    UpdateSystemAccount updateSystemAccount = new UpdateSystemAccount(selectedAccount);
                    updateSystemAccount.Show();
                    this.Close();
                    LoadSystemAccount();
                } else if (selectedAccount != null && Login.UserId == Int32.Parse(txtAccountID.Text))
                {
                    selectedAccount.AccountName = txtAccountName.Text;
                    selectedAccount.AccountEmail = txtAccountEmail.Text;
                    if (cboAccountRole.SelectedItem != null)
                    {
                        selectedAccount.AccountRole = Int32.Parse(cboAccountRole.SelectedValue.ToString());
                    }
                    selectedAccount.AccountPassword = txtAccountPassword.Password;

                    LoadSystemAccount();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadSystemAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 

            
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            int userRole = Login.UserRole;
            if (userRole == 3)
            {
                AdminMenu menu = new AdminMenu();
                menu.Show();
                return;
            }
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();  
        }


        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedItem == null)
            {
                return;
            }

            var selectedAccount = dataGrid.SelectedItem as SystemAccount;
            if (selectedAccount != null)
            {
                txtAccountID.Text = selectedAccount.AccountId.ToString();
                txtAccountName.Text = selectedAccount.AccountName;
                txtAccountEmail.Text = selectedAccount.AccountEmail;
                cboAccountRole.SelectedValue = selectedAccount.AccountRole;
                txtAccountPassword.Password = selectedAccount.AccountPassword;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           LoadAcountRole();
           LoadSystemAccount();
        }

        public void LoadSystemAccount()
        {
            try
            {
                var newsArticleList = iSystemAccountService.GetSystemAccounts();
                dgData.ItemsSource = newsArticleList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SystemAccount> list = iSystemAccountService.GetSystemAccountByName(txtSearch.Text);
                dgData.ItemsSource = list;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
