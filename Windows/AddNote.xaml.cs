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
    public partial class AddNote : Window
    {
        private readonly MainWindow _mainWindow;
        public AddNote(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        /* Title */

        bool titleClicked = false;
        private void TitleGetFocus(object sender, RoutedEventArgs e)
        {
            if (!titleClicked)
            {
                titleTextBox.Text = "";
                titleClicked = true;
            }
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

        /* Text */

        bool textClicked = false;
        private void textTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!textClicked)
            {
                textTextBox.Text = "";
                textClicked = true;
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

        /* Folder */
        bool folderClicked = false;
        private void folderTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!folderClicked)
            {
                folderTextBox.Text = "";
                folderClicked = true;
            }
        }

        /* Buttons */
        private void AddClick(object sender, MouseButtonEventArgs e)
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

            _mainWindow.AddNote(_tempList);
            _mainWindow.AddFolder(_tempList[_tempList.Count - 1]);
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
