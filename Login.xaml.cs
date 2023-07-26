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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly MainWindow _mainWindow;
        Crypto crypto = new Crypto();

        public Login(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string enteredPw = PwInput_Text.Text;
            List<String> user = _mainWindow.GetUserList();

            if (crypto.DecryptData(user[2], enteredPw) != "encrypted") 
            {
                MessageBox.Show("Wrong Password");
                //return;    
            }

            user[0] = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

            _mainWindow.setUser(user);
            _mainWindow.setlocalKey(enteredPw);
            _mainWindow.AppStart();
            _mainWindow.Show();
            this.Close();
        }


        bool pwClicked = false;
        private void PWGetFocus(object sender, RoutedEventArgs e)
        {
            if (!pwClicked)
            {
                PwInput_Text.Text = "";
            }
        }
    }
}
