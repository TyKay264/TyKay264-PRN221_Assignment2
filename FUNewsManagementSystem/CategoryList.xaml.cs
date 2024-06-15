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
    /// Interaction logic for CategoryList.xaml
    /// </summary>
    public partial class CategoryList : Window
    {
        private readonly ICategoryService iCategoryService;
        public CategoryList()
        {
            InitializeComponent();
            iCategoryService = new CategoryService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
        }


        public void LoadCategoryList()
        {
            try
            {
                var categoryList = iCategoryService.GetCategories();
                dgData.ItemsSource = categoryList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateCategory createCategory = new CreateCategory();
            createCategory.Show();
            this.Close();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedItem == null)
            {
                return;
            }

            var selectedCategory = dataGrid.SelectedItem as Category;
            if (selectedCategory != null)
            {
                txtCategoryId.Text = selectedCategory.CategoryId.ToString();
                txtCategoryName.Text = selectedCategory.CategoryName;
                txtCategoryDescription.Text = selectedCategory.CategoryDesciption;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCategory = dgData.SelectedItem as Category;
                if (selectedCategory != null && Login.UserRole == 1)
                {
                    selectedCategory.CategoryName = txtCategoryName.Text;
                    selectedCategory.CategoryDesciption = txtCategoryDescription.Text;


                    // iSystemAccountService.UpdateSystemAccount(selectedAccount);
                    UpdateCategory updateCategory = new UpdateCategory(selectedCategory);
                    updateCategory.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCategory = dgData.SelectedItem as Category;
                if (selectedCategory != null)
                {
                    selectedCategory.CategoryName = txtCategoryName.Text;
                    selectedCategory.CategoryDesciption = txtCategoryDescription.Text;
                    iCategoryService.DeleteCategory(selectedCategory);
                    LoadCategoryList();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadCategoryList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Category> category = iCategoryService.GetCategoryByName(txtSearch.Text);
                dgData.ItemsSource = category;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
