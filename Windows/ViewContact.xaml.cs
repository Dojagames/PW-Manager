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
    /// Interaction logic for ViewContact.xaml
    /// </summary>
    public partial class ViewContact : Window
    {
        public ViewContact(List<String> _contactList)
        {
            InitializeComponent();
            nameTextBox.Text = _contactList[0];
            emailTextBox.Text = _contactList[1];
            numberTextBox.Text = _contactList[2];
            birthdayTextBox.Text = _contactList[3];
            streetTextBox.Text = _contactList[4];
            cityTextBox.Text = _contactList[5];
        }

        private void CancelClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
