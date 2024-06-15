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
    /// Interaction logic for CreateTag.xaml
    /// </summary>
    public partial class CreateTag : Window
    {
        private readonly ITagService iTagService;
        public CreateTag()
        {
            InitializeComponent();
            iTagService = new TagService();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            TagList tagList = new TagList();
            tagList.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = new Tag();
            try
            {
                tag.TagName = txtTagName.Text;
                tag.Note = txtNote.Text;
                iTagService.SaveTag(tag);
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                TagList main = new TagList();
                main.Show();
            }
        }
    }
}
