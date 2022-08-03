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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PW_Manager.Windows;
using PW_Manager.Scripts;
using System.Text.RegularExpressions;

namespace PW_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentMode = 0;

        private string localKey = String.Empty;
        private string publicKey = @"5Z$kYWQcQDCchxLs9yt7e4@B";

        DataBase db = new DataBase();
        Crypto crypto = new Crypto();

        List<List<String>> Passwords = new List<List<String>>();
        List<List<String>> Notes = new List<List<String>>();
        List<List<String>> Contacts = new List<List<String>>();

        List<String> Folder = new List<String>();
        List<String> User = new List<String>();

        bool folderSelected = false;
        string selectedFolder = "";

        bool folderCollapsed = false;

        public MainWindow()
        {
            InitializeComponent();
            long unixSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
            //MessageBox.Show(unixSeconds.ToString());

            db.FolderExists();
            User = db.InitialLoad();
            if (User.Count == 0) {
                var registerWindow = new Register(this);
                registerWindow.Show();
                this.Hide();
            } else {
                var loginWindow = new Login(this);
                loginWindow.Show();
                this.Hide();
            }
        }



        /*  Functions  */

        public void AppStart()
        {
            LoadAll();
            RenderPws();
            SetSelectedPanel(0);
            LoadFolder();
        }


        public void LoadFolder()
        {
            Folder.Add("none");
            for (int i = 0; i < Passwords.Count; i++)
            {
                if (!Folder.Contains(Passwords[i][4])) Folder.Add(Passwords[i][4]);
            }

            for (int i = 0; i < Notes.Count; i++)
            {
                if (!Folder.Contains(Notes[i][2])) Folder.Add(Notes[i][2]);
            }

            RenderFolders();
        }



        public void LoadAll()
        {
            Passwords = db.Load2d(0, localKey);
            Notes = db.Load2d(1, localKey);
            Contacts = db.Load2d(2, localKey);
        }


        public void ViewFolder(string _name)
        {
            MainPanel.Children.Clear();

            if(currentMode == 0)
            {
                for(int i = 0; i < Passwords.Count; i++)
                {
                    if(Passwords[i][4] == _name)
                    {
                        RenderPw(i);
                    }
                }
            }

            if (currentMode == 1)
            {
                for (int i = 0; i < Notes.Count; i++)
                {
                    if (Notes[i][2] == _name)
                    {
                        RenderNote(i);
                    }
                }
            }
        }

        public void SelectFolder(int _index)
        {
            selectedFolder = Folder[_index];
            folderSelected = true;

            FolderPanel.Children.Clear();
            for(int i = 0; i < Folder.Count; i++)
            {
                if (i != _index)
                {
                    RenderFolder(i);
                    continue;
                }
                RenderActiveFolder(_index);
            }
            ResetSearchText();
        }

        public void ChangeSettings(int _lockOutInterval)
        {
            User[1] = _lockOutInterval.ToString();
            db.saveUser(User);
        }

        private void ResetSearchText()
        {
            defSearchClick = true;
            SearchTextBox.Text = "Search";
            defSearchClick = false;
        }

        public void OpenPassword()
        {
            Text_Display_Mode.Text = "Password";
            currentMode = 0;
            RenderPws();
            SetSelectedPanel(currentMode);

            RenderFolders();
            ResetSearchText();
            folderSelected = false;
        }


        /*  Getter Setter  */
        public List<String> GetUserList()
        {
            return User;
        }

        public string getlocalKey()
        {
            return localKey;
        }

        public void setlocalKey(string _setter)
        {
            localKey = _setter;
        }

        public string getpublicKey()
        {
            return publicKey;
        }

        public void setUser(List<String> _setter)
        {
            User = _setter;
            db.saveUser(User);
        }


        /* Add References */
        public void AddPw(List<String> _temp)
        {
            Passwords.Add(_temp);
            db.Save2d(0, Passwords, localKey);
            if (folderSelected && selectedFolder != _temp[4]) return;
            RenderPw(Passwords.Count - 1);
        }
        public void OpenPwAdd(string templatePw = "")
        {
            OpenPassword();
            AddPassword addPwWindow = new AddPassword(this, templatePw);
            addPwWindow.Show();
            this.Hide();
        }

        public void AddNote(List<String> _temp)
        {
            Notes.Add(_temp);
            db.Save2d(1, Notes, localKey);
            if (folderSelected && selectedFolder != _temp[2]) return;
            RenderNote(Notes.Count - 1);
        }

        public void AddContact(List<String> _temp)
        {
            Contacts.Add(_temp);
            db.Save2d(2, Contacts, localKey);
            RenderContact(Contacts.Count - 1);
        }

        public void AddFolder(string _temp)
        {
            if (Folder.Contains(_temp)) return;
            Folder.Add(_temp);
            RenderFolder(Folder.Count -1);
        }

        /* Remove References */
        public void RemovePw(int _index)
        {
            Passwords.RemoveAt(_index);
            db.Save2d(0, Passwords, localKey);
            RenderPws();
        }

        public void RemoveNote(int _index)
        {
            Notes.RemoveAt(_index);
            db.Save2d(1, Notes, localKey);
            RenderNotes();
        }

        public void RemoveContact(int _index)
        {
            Contacts.RemoveAt(_index);
            db.Save2d(2, Contacts, localKey);
            RenderContacts();
        }



        /* Edit References */

        //PW
        public void ApplyEditPw(List<String> _pw, int _index)
        {
            Passwords[_index] = _pw;
            RenderPws();
            db.Save2d(0, Passwords, localKey);
        }

        public void EditPw(int _index)
        {
            EditPassword editPassword = new EditPassword(this, Passwords[_index], _index);
            editPassword.Show();
            this.Hide();
        }

        //Note
        public void ApplyEditNote(List<String> _note, int _index)
        {
            Notes[_index] = _note;
            RenderNotes();
            db.Save2d(1, Notes, localKey);
        }

        public void EditNote(int _index)
        {
            EditNote editNote = new EditNote(this, Notes[_index], _index);
            editNote.Show();
            this.Hide();
        }

        //Contact
        public void ApplyEditContact(List<String> _contact, int _index)
        {
            Contacts[_index] = _contact;
            RenderContacts();
            db.Save2d(2, Contacts, localKey);
        }

        public void EditContact(int _index)
        {
            EditContact editContact = new EditContact(this, Contacts[_index], _index);
            editContact.Show();
            this.Hide();
        }


        /* Show Prefferences */

        public void ViewContactPanel(int _index) 
        {
            ViewContact viewContact = new ViewContact(Contacts[_index]);
            viewContact.Show();
        }

        public void ViewNotePanel(int _index)
        {
            ViewNote viewNote = new ViewNote(Notes[_index]);
            viewNote.Show();
        }

        /*  Buttons  */

        private void btn_add_click(object sender, MouseButtonEventArgs e)
        {
            switch (currentMode)
            {
                case 1:
                    AddNote addNoteWindow = new AddNote(this);
                    addNoteWindow.Show();
                    this.Hide();
                    break;

                case 2:
                    AddContact addContactWindow = new AddContact(this);
                    addContactWindow.Show();
                    this.Hide();
                    break;

                default:
                    OpenPwAdd();
                    break;
            }
        }


        private void Password_On_Click(object sender, MouseButtonEventArgs e)
        {
            OpenPassword();
        }

        private void Notes_On_Click(object sender, MouseButtonEventArgs e)
        {
            Text_Display_Mode.Text = "Notes";
            currentMode = 1;
            RenderNotes();
            SetSelectedPanel(currentMode);

            RenderFolders();
            folderSelected = false;
            ResetSearchText();
        }

        private void Contacts_On_Click(object sender, MouseButtonEventArgs e)
        {
            Text_Display_Mode.Text = "Contacts";
            currentMode = 2;
            RenderContacts();
            SetSelectedPanel(currentMode);

            RenderFolders();
            folderSelected = false;
            ResetSearchText();
        }

        private void FolderCollapseClicked(object sender, MouseButtonEventArgs e)
        {
           
            if (!folderCollapsed)
            {
                FolderPanel.Children.Clear();
                //ExpandImage.Source = new BitmapImage(new Uri(@"/Resources/Pngs/expand.png", UriKind.Relative));
            } else {
                RenderFolders();
                //ExpandImage.Source = new BitmapImage(new Uri(@"/Resources/Pngs/collapse.png", UriKind.Relative));
            }


            folderCollapsed = !folderCollapsed;
        }


        private void PwGen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordGenerator _pwGen = new PasswordGenerator(this);
            _pwGen.Show();
        }


        bool firstTriggerChange = false;
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!firstTriggerChange)
            {
                firstTriggerChange = true;
                return;
            }
            if (defSearchClick) return;

            folderSelected = false;

            MainPanel.Children.Clear();
            
            
            RenderFolders();

            for(int i = 0; i < Passwords.Count; i++)
            {
                if (Regex.IsMatch(Passwords[i][0], Regex.Escape(SearchTextBox.Text), RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(Passwords[i][1], Regex.Escape(SearchTextBox.Text), RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(Passwords[i][3], Regex.Escape(SearchTextBox.Text), RegexOptions.IgnoreCase)) RenderPw(i);
            }

            for (int i = 0; i < Notes.Count; i++)
            {
                if (Regex.IsMatch(Notes[i][0], Regex.Escape(SearchTextBox.Text), RegexOptions.IgnoreCase)) RenderPw(i);
            }
        }

        bool defSearchClick = false;
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Search") {
                defSearchClick = true;
                SearchTextBox.Text = "";
                defSearchClick = false;
            }  else defSearchClick = false;
        }

        private void SettingsClick(object sender, MouseButtonEventArgs e)
        {
            Settings setting = new Settings(this, User[1]);
            setting.Show();
        }

        /*  estatics  */
        private void Password_Hover_Enter(object sender, MouseEventArgs e)
        {
            if (currentMode != 0)
                PasswordSidePanel.Background = new BrushConverter().ConvertFrom("#3a3a3a") as Brush;
        }

        private void Password_Hover_Leave(object sender, MouseEventArgs e)
        {
            if (currentMode != 0)
                PasswordSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
        }

        private void Note_Hover_Enter(object sender, MouseEventArgs e)
        {
            if (currentMode != 1)
                NoteSidePanel.Background = new BrushConverter().ConvertFrom("#3a3a3a") as Brush;
        }

        private void Note_Hover_Leave(object sender, MouseEventArgs e)
        {
            if (currentMode != 1)
                NoteSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
        }

        private void Contact_Hover_Leave(object sender, MouseEventArgs e)
        {
            if (currentMode != 2)
                ContactSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
        }

        private void Contact_Hover_Enter(object sender, MouseEventArgs e)
        {
            if (currentMode != 2)
                ContactSidePanel.Background = new BrushConverter().ConvertFrom("#3a3a3a") as Brush;
        }

        private void SetSelectedPanel(int _index)
        {
            switch (_index)
            {
                case 0:
                    PasswordSidePanel.Background = new BrushConverter().ConvertFrom("#303030") as Brush;
                    NoteSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
                    ContactSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
                    break;

                case 1:
                    NoteSidePanel.Background = new BrushConverter().ConvertFrom("#303030") as Brush;
                    PasswordSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
                    ContactSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
                    break;
                case 2:
                    ContactSidePanel.Background = new BrushConverter().ConvertFrom("#303030") as Brush;
                    PasswordSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
                    NoteSidePanel.Background = new BrushConverter().ConvertFrom("#3f3f3f") as Brush;
                    break;
            }
        }



        // Drawing
        public void RenderPws()
        {
            MainPanel.Children.Clear();
            for (int i = 0; i < Passwords.Count; i++)
            {
                RenderPw(i);
            }
        }

        public void RenderNotes()
        {
            MainPanel.Children.Clear();
            for (int i = 0; i < Notes.Count; i++)
            {
                RenderNote(i);
            }
        }

        public void RenderContacts()
        {
            MainPanel.Children.Clear();
            for (int i = 0; i < Contacts.Count; i++)
            {
                RenderContact(i);
            }
        }

        public void RenderFolders()
        {
            FolderPanel.Children.Clear();
            for (int i = 0; i < Folder.Count; i++)
            {
                RenderFolder(i);
            }
        }


        private void RenderPw(int _index)
        {
            Render render = new Render(this);
            MainPanel.Children.Add(render.PwGrid(Passwords[_index], _index));
        }

        private void RenderNote(int _index)
        {
            Render render = new Render(this);
            MainPanel.Children.Add(render.NoteGrid(Notes[_index], _index));
        }

        private void RenderContact(int _index)
        {
            Render render = new Render(this);
            MainPanel.Children.Add(render.ContactGrid(Contacts[_index], _index));
        }

        private void RenderFolder(int _index)
        {
            Render render = new Render(this);
            FolderPanel.Children.Add(render.FolderGrid(Folder[_index], _index));
        }
        private void RenderActiveFolder(int _index)
        {
            Render render = new Render(this);
            FolderPanel.Children.Add(render.SelectedFolderGrid(Folder[_index], _index));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //systemTray
        }
    }
}