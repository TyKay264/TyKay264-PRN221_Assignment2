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
    /// Interaction logic for UpdateTag.xaml
    /// </summary>
    public partial class UpdateTag : Window
    {
        private ITagService iTagService;
        private Tag _tag;
        public UpdateTag(Tag tag)
        {
            InitializeComponent();
            iTagService = new TagService();
            _tag = tag;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _tag.TagName = txtTagName.Text;
            _tag.Note = txtNote.Text;
            iTagService.UpdateTag(_tag);
            this.Close();
            TagList tagList = new TagList();
            tagList.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            TagList tagList = new TagList();
            tagList.Show();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTag();
        }


        private void LoadTag()
        {
            if (_tag != null)
            {
                txtTagId.Text = _tag.TagId.ToString();
                txtTagName.Text = _tag.TagName;
                txtNote.Text = _tag.Note;
            }
        }
    }
}
