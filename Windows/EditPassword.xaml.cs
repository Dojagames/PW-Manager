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
    /// Interaction logic for EditPassword.xaml
    /// </summary>
    public partial class EditPassword : Window
    {
        private readonly MainWindow _mainWindow;
        private List<String> passwordList;
        private int index;

        public EditPassword(MainWindow mainWindow, List<String> _pwList, int _index)
        {
            passwordList = _pwList;
            _mainWindow = mainWindow;
            index = _index;
            InitializeComponent();

            LoadTxt();
        }

        private void LoadTxt()
        {
            titleTextBox.Text = passwordList[0];
            userTextBox.Text = passwordList[1];
            pwTextBox.Text = passwordList[2];
            webTextBox.Text = passwordList[3];
            folderTextBox.Text = passwordList[4];
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

        private void userTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (userTextBox.Text == "" || pwTextBox.Text == "")
            {
                wrongPwText.Visibility = Visibility.Visible;
            }
            else
            {
                wrongPwText.Visibility = Visibility.Hidden;
            }
        }

        private void pwTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (userTextBox.Text == "" || pwTextBox.Text == "")
            {
                wrongPwText.Visibility = Visibility.Visible;
            }
            else
            {
                wrongPwText.Visibility = Visibility.Hidden;
            }

            pwSecurityText.Text = "Your Password Security is " + IsPasswordSecure(pwTextBox.Text);
        }

        private String IsPasswordSecure(string _pw)
        {


            if (_pw.Length > 8 && _pw.Length < 24 && _pw.Any(char.IsDigit) && _pw.Any(char.IsSymbol))
            {
                pwSecurityText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dbde21"));
                return "medium";
            }

            if (_pw.Length >= 24 && _pw.Any(char.IsDigit) && (_pw.Any(char.IsSymbol) || _pw.Any(char.IsControl) || _pw.Any(char.IsPunctuation)))
            {
                pwSecurityText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1ab512"));
                return "strong";
            }

            pwSecurityText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#db2b14"));
            return "unsecure";
        }

        private void webTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (webTextBox.Text == "")
            {
                wrongWebText.Visibility = Visibility.Visible;
            }
            else
            {
                wrongWebText.Visibility = Visibility.Hidden;
            }
        }

        private void EditButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (titleTextBox.Text == "" || titleTextBox.Text == "Title")
            {
                MessageBox.Show("Title can't be empty");
                return;
            }

            if (userTextBox.Text == "")
            {
                MessageBox.Show("Username can't be empty");
                return;
            }

            if (pwTextBox.Text == "")
            {
                MessageBox.Show("Password can't be empty");
                return;
            }

            if (webTextBox.Text == "" || webTextBox.Text == "Website")
            {
                MessageBox.Show("Website can't be empty");
                return;
            }


            List<String> _tempList = new List<String>();
            _tempList.Add(titleTextBox.Text);
            _tempList.Add(userTextBox.Text);
            _tempList.Add(pwTextBox.Text);
            _tempList.Add(webTextBox.Text);

            if (folderTextBox.Text == "" || folderTextBox.Text == "Folder")
            {
                _tempList.Add("none");
            }
            else
            {
                _tempList.Add(folderTextBox.Text);
            }

            _mainWindow.ApplyEditPw(_tempList, index);
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
