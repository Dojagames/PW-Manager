using PW_Manager.Scripts;
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
    /// Interaction logic for PasswordGenerator.xaml
    /// </summary>
    public partial class PasswordGenerator : Window
    {
        PwGen pwGen = new PwGen();
        private readonly MainWindow _mainWindow;

        public PasswordGenerator(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void TitleGetFocus(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(PWTextBox.Text);
        }

        private void GenerateClick(object sender, MouseButtonEventArgs e)
        {
            int _lenght = 0;
            try
            {
                _lenght = Convert.ToInt32(pwLength.Text);
                PWTextBox.Text = pwGen.GeneratePassword(upperCaseRadio.IsChecked.GetValueOrDefault(), lowerCaseRadio.IsChecked.GetValueOrDefault(), numberRadio.IsChecked.GetValueOrDefault(), symbolRadio.IsChecked.GetValueOrDefault(), _lenght);
            } catch {
                PWTextBox.Text = pwGen.GeneratePassword(upperCaseRadio.IsChecked.GetValueOrDefault(), lowerCaseRadio.IsChecked.GetValueOrDefault(), numberRadio.IsChecked.GetValueOrDefault(), symbolRadio.IsChecked.GetValueOrDefault());
            }          
        }

        private void AddPwClick(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.OpenPwAdd(PWTextBox.Text);
            this.Close();
        }
    }
}
