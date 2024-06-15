using BusinessObjects;
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
    /// Interaction logic for CreateNewsArticleWindow.xaml
    /// </summary>
    public partial class CreateNewsArticleWindow : Window
    {
        private readonly INewsArticlesService iNewsArticlesService;
        private readonly ICategoryService iCategoryService;
        public CreateNewsArticleWindow()
        {
            InitializeComponent();
            iNewsArticlesService = new NewsArticleService();
            iCategoryService = new CategoryService();
            txtModifiedDate.SelectedDate = DateTime.Today;
            txtCreatedById.Text = Login.UserId.ToString();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            NewsArticle newsArticle = new NewsArticle();
            try
            {
                newsArticle.NewsTitle = txtNewsTitle.Text;
                newsArticle.CreatedDate = txtCreatedDate.SelectedDate ?? DateTime.Now;
                newsArticle.NewsContent = txtNewsContent.Text;
                newsArticle.ModifiedDate = txtModifiedDate.SelectedDate ?? DateTime.Now;
                newsArticle.NewsStatus = true;
                newsArticle.CreatedById = Login.UserId;
                newsArticle.CategoryId = (short?)cboCategoryId.SelectedValue;

                iNewsArticlesService.SaveNewsArticle(newsArticle);
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } finally
            {
                MainWindow main = new MainWindow();
                main.Show();
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
        }
    }
}
