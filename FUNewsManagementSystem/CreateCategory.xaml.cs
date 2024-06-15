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
    /// Interaction logic for CreateCategory.xaml
    /// </summary>
    public partial class CreateCategory : Window
    {
        private readonly ICategoryService iCategoryService;
        public CreateCategory()
        {
            InitializeComponent();
            iCategoryService = new CategoryService();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category();
            try
            {
                category.CategoryName = txtCategoryName.Text;
                category.CategoryDesciption = txtCategoryDescription.Text;
                iCategoryService.SaveCategory(category);
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CategoryList main = new CategoryList();
                main.Show();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            CategoryList category = new CategoryList(); 
            category.Show();
        }
    }
}
