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
    /// Interaction logic for TagList.xaml
    /// </summary>
    public partial class TagList : Window
    {
        private readonly ITagService iTagService;
        public TagList()
        {
            InitializeComponent();
            iTagService = new TagService();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            CreateTag createTag = new CreateTag();  
            createTag.Show();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedItem == null)
            {
                return;
            }

            var selectedTag = dataGrid.SelectedItem as Tag;
            if (selectedTag != null)
            {
                txtTagId.Text = selectedTag.TagId.ToString();
                txtTagName.Text = selectedTag.TagName;
                txtNote.Text = selectedTag.Note;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selctedTag = dgData.SelectedItem as Tag;
                if (selctedTag != null)
                {
                    selctedTag.TagName = txtTagName.Text;
                    selctedTag.Note = txtNote.Text;


                    UpdateTag updateTag = new UpdateTag(selctedTag);
                    updateTag.Show();
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedTag = dgData.SelectedItem as Tag;
                if (selectedTag != null)
                {
                    selectedTag.TagName = txtTagName.Text;
                    selectedTag.Note = txtNote.Text;
                    iTagService.DeleteTag(selectedTag);
                    LoadTagList();
                }
                else
                {
                    MessageBox.Show("You must select an Account or You don't have an authorization to Update");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("The account has been modified or deleted by another user. Please reload and try again.", "Concurrency Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadTagList();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadTagList()
        {
            try
            {
                var tagList = iTagService.GetTags();
                dgData.ItemsSource = tagList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTagList();
        }
    }
}
