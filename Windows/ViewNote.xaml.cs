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
    /// Interaction logic for ViewNote.xaml
    /// </summary>
    public partial class ViewNote : Window
    {
        public ViewNote(List<String> NoteList)
        {
            InitializeComponent();
            titleTextBox.Text = NoteList[0];
            textTextBox.Text = NoteList[1];
            folderTextBox.Text = NoteList[2];
        }

        private void CancelClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
