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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly INewsArticlesService iNewsArticleService;
        private readonly ICategoryService iCategoryService;
        public MainWindow()
        {
            InitializeComponent();
            iNewsArticleService = new NewsArticleService();
            iCategoryService = new CategoryService();
               int userRole = Login.UserRole;


            if (userRole == 1 || userRole == 2)
                {
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnViewProfile.IsEnabled = true;
                btnViewHistory.IsEnabled = true;
                btnLogin.Visibility = Visibility.Collapsed;

            }
            else
             {
                btnCreate.Visibility = Visibility.Collapsed;
                btnUpdate.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnViewProfile.Visibility = Visibility.Collapsed;
                btnViewHistory.Visibility = Visibility.Collapsed;
             }

          }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateNewsArticleWindow createNewsArticleWindow = new CreateNewsArticleWindow();
            createNewsArticleWindow.ShowDialog();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedNewsArticle = dgData.SelectedItem as NewsArticle;
                if (selectedNewsArticle != null)
                {
                    selectedNewsArticle.NewsTitle = txtNewsTitle.Text;
                    selectedNewsArticle.CreatedDate = txtCreatedDate.SelectedDate ?? DateTime.Now; 
                    selectedNewsArticle.NewsContent = txtNewsContent.Text;
                    selectedNewsArticle.CategoryId = (short)cboCategoryId.SelectedValue;
                    UpdateNewsArticle updateNewsArticle = new UpdateNewsArticle(selectedNewsArticle);
                    updateNewsArticle.Show();
                    this.Close();
                    LoadNewsArticle();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadNewsArticle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedNewsArticle = dgData.SelectedItem as NewsArticle;
                if (selectedNewsArticle != null)
                {
                    selectedNewsArticle.NewsTitle = txtNewsTitle.Text;
                    selectedNewsArticle.CreatedDate = txtCreatedDate.SelectedDate ?? DateTime.Now;
                    selectedNewsArticle.NewsContent = txtNewsContent.Text;
                    selectedNewsArticle.ModifiedDate = txtModifiedDate.SelectedDate ?? DateTime.Now;
                    selectedNewsArticle.NewsStatus = true;
                    selectedNewsArticle.CreatedById = Login.UserId;
                    if (cboCategoryId.SelectedItem != null)
                    {
                        selectedNewsArticle.CategoryId = (short)cboCategoryId.SelectedValue;
                    }
                    iNewsArticleService.DeleteNewsArticle(selectedNewsArticle);
                    LoadNewsArticle();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadNewsArticle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainMenu menu = new MainMenu();
            menu.Show();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedItem == null)
            {
                return;
            }

            var newsArticle = dataGrid.SelectedItem as NewsArticle;
            if (newsArticle != null)
            {
                txtNewsArticleID.Text = newsArticle.NewsArticleId;
                txtNewsTitle.Text = newsArticle.NewsTitle;
                txtCreatedDate.Text = newsArticle.CreatedDate.ToString();
                txtNewsContent.Text = newsArticle.NewsContent;
                txtModifiedDate.Text = newsArticle.ModifiedDate.ToString();
                cboCategoryId.SelectedValue = newsArticle.CategoryId;
            }
        }

        public void LoadNewsArticle()
        {
            try
            {
                var newsArticleList = iNewsArticleService.getNewsArticles();
                dgData.ItemsSource = newsArticleList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNewsArticle();
            LoadCategoryList();
        }

        private void resetInput()
        {
            txtNewsArticleID.Text = "";
            txtNewsTitle.Text = "";
            txtCreatedDate.Text = "";
            txtNewsContent.Text = "";
            txtModifiedDate.Text = "";
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnViewProfile_Click(object sender, RoutedEventArgs e)
        {
            AccountManagement accountManagement = new AccountManagement();
            accountManagement.Show();
            this.Close();
        }

        public void LoadCategoryList()
        {
            try
            {
                var categoryList = iCategoryService.GetCategories();
                cboCategoryId.ItemsSource = categoryList;
                cboCategoryId.DisplayMemberPath = "CategoryName";
                cboCategoryId.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userId = Login.UserId;
                List<NewsArticle> articlesList = iNewsArticleService.GetNewsArticlesByWriterId(userId);

                ViewHistoryCreated viewHistoryCreated = new ViewHistoryCreated(articlesList);   
                viewHistoryCreated.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<NewsArticle> list = iNewsArticleService.GetNewsArticlesByTitle(txtSearch.Text);
                dgData.ItemsSource = list;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
