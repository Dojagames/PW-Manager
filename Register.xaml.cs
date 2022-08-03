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
using PW_Manager.Scripts;

namespace PW_Manager
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly MainWindow _mainWindow;
        Crypto crypto = new Crypto();

        public Register(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Submit_btn(object sender, RoutedEventArgs e)
        {
            if (!pw1Text.Text.Equals(pw2Text.Text))
            {
                MessageBox.Show("passwords dont match");
                return;
            }

            string enteredPw = pw1Text.Text;
            List<String> user = new List<string>();

            long unixSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            user.Add(unixSeconds.ToString());
            user.Add("84600");
            user.Add(crypto.EncryptData("encrypted", enteredPw));

            _mainWindow.setlocalKey(enteredPw);

            _mainWindow.setUser(user);
            _mainWindow.Show();
            this.Close();
        }

        bool pw1Clicked = false;
        private void pw1Text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!pw1Clicked)
            {
                pw1Text.Text = "";
                pw1Clicked = true;
            }
        }

        bool pw2Clicked = false;
        private void pw2Text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!pw2Clicked)
            {
                pw2Text.Text = "";
                pw2Clicked = true;
            }
        }
    }
}