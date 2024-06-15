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
    /// Interaction logic for UpdateCategory.xaml
    /// </summary>
    public partial class UpdateCategory : Window
    {
        private readonly ICategoryService iCategoryService;
        private Category _category;
        public UpdateCategory(Category category)
        {
            InitializeComponent();
            _category = category;
            iCategoryService = new CategoryService();
            LoadCategory();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategory();
        }


        private void LoadCategory()
        {
            if (_category != null)
            {
                txtCategoryName.Text = _category.CategoryName;
                txtCategoryDescription.Text = _category.CategoryDesciption;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _category.CategoryName = txtCategoryName.Text;
            _category.CategoryDesciption = txtCategoryDescription.Text;
            iCategoryService.UpdateCategory(_category);
            this.Close();
            CategoryList categoryList = new CategoryList();
            categoryList.Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            CategoryList categoryList = new CategoryList();
            categoryList.Show();
        }
    }
}
