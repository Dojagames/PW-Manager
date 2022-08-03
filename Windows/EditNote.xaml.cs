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

namespace PW_Manager.Windows
{
    /// <summary>
    /// Interaction logic for EditNote.xaml
    /// </summary>
    /// 
    
    public partial class EditNote : Window
    {
        private readonly MainWindow _mainWindow;
        private List<String> noteList;
        private int index;

        public EditNote(MainWindow mainWindow, List<String> _noteList, int _index)
        {
            _mainWindow = mainWindow;
            index = _index;
            noteList = _noteList;
            InitializeComponent();

            LoadTxt();
        }

        private void LoadTxt()
        {
            titleTextBox.Text = noteList[0];
            textTextBox.Text = noteList[1];
            folderTextBox.Text = noteList[2];
        }

        private void titleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (titleTextBox.Text == "")
            {
                wrongTitleText.Visibility = Visibility.Visible;
            }
            else
            {
                wrongTitleText.Visibility = Visibility.Hidden;
            }
        }

        private void textTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textTextBox.Text == "")
            {
                wrongTextText.Visibility = Visibility.Visible;
            }
            else
            {
                wrongTextText.Visibility = Visibility.Hidden;
            }
        }

        private void EditClick(object sender, MouseButtonEventArgs e)
        {
            if (titleTextBox.Text == "" || titleTextBox.Text == "Title")
            {
                MessageBox.Show("Title can't be empty");
                return;
            }

            if (textTextBox.Text == "")
            {
                MessageBox.Show("Text can't be empty");
                return;
            }

            List<String> _tempList = new List<String>();
            _tempList.Add(titleTextBox.Text);
            _tempList.Add(textTextBox.Text);

            if (folderTextBox.Text == "" || folderTextBox.Text == "Folder")
            {
                _tempList.Add("none");
            }
            else
            {
                _tempList.Add(folderTextBox.Text);
            }

            _mainWindow.ApplyEditNote(_tempList, index);
            this.Close();
        }

        private void CancelClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mainWindow.Show();
        }
    }
}
