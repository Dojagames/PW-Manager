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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private readonly MainWindow _mainWindow;
        public Settings(MainWindow mainWindow, string currentVal)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            timeText.Text = (Int32.Parse(currentVal) * 1 / 3600).ToString();
        }

        private void SaveClick(object sender, MouseButtonEventArgs e)
        {
            int _time;
            try {
                _time = Int32.Parse(timeText.Text) * 3600;
                _mainWindow.ChangeSettings(_time);
            } catch { }

            this.Close();
        }

        private void CancelClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
