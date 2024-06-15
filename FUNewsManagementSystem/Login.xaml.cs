using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.NativeInterop;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly ISystemAccountService iSystemAccountService;
        public static int UserRole { get; private set; }
        public static short UserId { get; private set; }
        private readonly IConfiguration configuration;
        private readonly string predefinedEmail;
        private readonly string predefinedPassword;

        public Login()
        {
            InitializeComponent();
            iSystemAccountService = new SystemAccountService();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration = builder.Build();

            predefinedEmail = configuration["SystemAccount:Admin:AccountEmail"];
            predefinedPassword = configuration["SystemAccount:Admin:AccountPassword"];
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string enteredEmail = txtUser.Text;
            string enteredPassword = txtPass.Password;
            
            if (enteredEmail.Equals(predefinedEmail, StringComparison.OrdinalIgnoreCase) &&
                enteredPassword.Equals(predefinedPassword))
            {
                UserRole = 3; // Assuming 1 indicates Admin
                UserId = 0; // Dummy UserId for Admin

                this.Hide();
                AdminMenu main = new AdminMenu();
                main.Show();
                return;
            }
            
            SystemAccount account = iSystemAccountService.GetSystemAccountByUsername((txtUser.Text));

            if (account != null && account.AccountPassword.Equals(txtPass.Password))
                {
                    UserRole = (int)account.AccountRole;
                    UserId = account.AccountId;
                    this.Hide();
                    MainMenu main = new MainMenu();
                    main.Show();
            }
                else
            {
                MessageBox.Show("Invalid credentials or you do not have permission to access this system.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
