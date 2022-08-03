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
    /// Interaction logic for EditContact.xaml
    /// </summary>
    public partial class EditContact : Window
    {
        private readonly MainWindow _mainWindow;
        private List<String> contactList;
        private int index;

        public EditContact(MainWindow mainWindow, List<String> _contactList, int _index)
        {
            _mainWindow = mainWindow;
            index = _index;
            contactList = _contactList;

            InitializeComponent();
            LoadTxt();
        }

        private void LoadTxt()
        {
            nameTextBox.Text = contactList[0];
            emailTextBox.Text = contactList[1];
            numberTextBox.Text = contactList[2];
            birthdayTextBox.Text = contactList[3];
            streetTextBox.Text = contactList[4];
            cityTextBox.Text = contactList[5];
        }

       

        private void EditClick(object sender, MouseButtonEventArgs e)
        {
            if (nameTextBox.Text == "" || nameTextBox.Text == "Name")
            {
                MessageBox.Show("Name can't be empty");
                return;
            }

            List<String> _tempList = new List<String>();
            _tempList.Add(nameTextBox.Text);


            //email
            if (emailTextBox.Text == "" || emailTextBox.Text == "Email")
            {
                _tempList.Add("none");
            }
            else
            {
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

            _mainWindow.ApplyEditContact(_tempList, index);
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
