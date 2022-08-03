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
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        private readonly MainWindow _mainWindow;

        public AddContact(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        /* Name */

        bool nameClicked = false;
        private void TitleGetFocus(object sender, RoutedEventArgs e)
        {
            if (!nameClicked)
            {
                nameTextBox.Text = "";
                nameClicked = true;
            }
        }

        private void titleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                wrongNameText.Visibility = Visibility.Visible;
            }
            else
            {
                wrongNameText.Visibility = Visibility.Hidden;
            }
        }

        //email
        bool emailClicked = false;
        private void emailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!emailClicked)
            {
                emailTextBox.Text = "";
                emailClicked = true;
            }
        }

        //number
        bool numberClicked = false;
        private void numberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!numberClicked)
            {
                numberTextBox.Text = "";
                numberClicked = true;
            }
        }

        //birthday
        bool birthdayClicked = false;
        private void birthdayTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!birthdayClicked)
            {
                birthdayTextBox.Text = "";
                birthdayClicked = true;
            }
        }

        //street
        bool streetClicked = false;
        private void streetTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!streetClicked)
            {
                streetTextBox.Text = "";
                streetClicked = true;
            }
        }

        //city
        bool cityClicked = false;
        private void cityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!cityClicked)
            {
                cityTextBox.Text = "";
                cityClicked = true;
            }
        }


        private void AddClick(object sender, MouseButtonEventArgs e)
        {
            if (nameTextBox.Text == "" ||nameTextBox.Text == "Name")
            {
                MessageBox.Show("Name can't be empty");
                return;
            }

            List<String> _tempList = new List<String>();
            _tempList.Add(nameTextBox.Text);


            //email
            if (emailTextBox.Text == "" || emailTextBox.Text == "Email") {
                _tempList.Add("none");
            } else {
                _tempList.Add(emailTextBox.Text);
            }

            //Number
            if (numberTextBox.Text == "" || numberTextBox.Text == "Number")
            {
                _tempList.Add("none");
            }
            else
            {
                _tempList.Add(numberTextBox.Text);
            }

            //bDay
            if (birthdayTextBox.Text == "" || birthdayTextBox.Text == "Birthday")
            {
                _tempList.Add("none");
            }
            else
            {
                _tempList.Add(birthdayTextBox.Text);
            }

            //Street
            if (streetTextBox.Text == "" || streetTextBox.Text == "Street")
            {
                _tempList.Add("none");
            }
            else
            {
                _tempList.Add(streetTextBox.Text);
            }

            //City
            if (cityTextBox.Text == "" || cityTextBox.Text == "City")
            {
                _tempList.Add("none");
            }
            else
            {
                _tempList.Add(cityTextBox.Text);
            }

            _mainWindow.AddContact(_tempList);
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
