using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PW_Manager.Scripts;
using PW_Manager.Windows;

namespace PW_Manager.Scripts
{
    public partial class Render
    {
        private readonly MainWindow _mainWindow;

        public Render(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public String MakeWebsiteValid(string _input)
        {
            Uri? _uri;
            try
            {
                _uri = new Uri(_input);
            } catch {
                try {
                    _uri = new Uri("http://" + _input);
                } catch {
                    _uri = new Uri("https://www.google.com/");
                }
                
            }
            return _uri.ToString();
        }

        public void LaunchSite(string _url)
        {
            System.Diagnostics.Process.Start("cmd", "/C start" + " " + _url);
        }

        public BitmapImage GetBitmap(string _url)
        {
            var _uri = new Uri(MakeWebsiteValid(_url));
            string host = _uri.Host;
            string completeUri = @"http://" + host + @"/favicon.ico";
            return new BitmapImage(new Uri(completeUri));
        }

  


        public Grid PwGrid(List<String> pwList, int _index)
        {
            //Grid def
            Grid tempGrid = new Grid();
            tempGrid.Height = 38;
            tempGrid.Background = new SolidColorBrush(Colors.Transparent);

            //Row def
            RowDefinition row0 = new RowDefinition();
            RowDefinition rowMain = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row0.Height = new GridLength(1);
            rowMain.Height = new GridLength(35);
            row2.Height = new GridLength(2);

            tempGrid.RowDefinitions.Add(row0);
            tempGrid.RowDefinitions.Add(rowMain);
            tempGrid.RowDefinitions.Add(row2);

            //Column def
            ColumnDefinition column0 = new ColumnDefinition();
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            ColumnDefinition column4 = new ColumnDefinition();
            ColumnDefinition column5 = new ColumnDefinition();
            ColumnDefinition column6 = new ColumnDefinition();
            ColumnDefinition column7 = new ColumnDefinition();
            ColumnDefinition column8 = new ColumnDefinition();
            column0.Width = new GridLength(35);
            column1.Width = new GridLength(50);
            //column2.Width = new GridLength();
            column3.Width = new GridLength(40);
            column4.Width = new GridLength(35);
            column5.Width = new GridLength(35);
            column6.Width = new GridLength(35);
            column7.Width = new GridLength(35);
            column8.Width = new GridLength(20);

            tempGrid.ColumnDefinitions.Add(column0);
            tempGrid.ColumnDefinitions.Add(column1);
            tempGrid.ColumnDefinitions.Add(column2);
            tempGrid.ColumnDefinitions.Add(column3);
            tempGrid.ColumnDefinitions.Add(column4);
            tempGrid.ColumnDefinitions.Add(column5);
            tempGrid.ColumnDefinitions.Add(column6);
            tempGrid.ColumnDefinitions.Add(column7);
            tempGrid.ColumnDefinitions.Add(column8);


            //Spacer
            Label spacerLabel = new Label();
            spacerLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5f5f5f"));
            spacerLabel.SetValue(Grid.ColumnSpanProperty, 9);

            Grid.SetRow(spacerLabel, 0);
            Grid.SetColumn(spacerLabel, 0);
            Grid.SetRow(spacerLabel, 2);
            Grid.SetColumn(spacerLabel, 0);
            tempGrid.Children.Add(spacerLabel);

            //Image
            Image image = new Image();
            image.Source = GetBitmap(pwList[3]);
            Grid.SetRow(image, 1);
            Grid.SetColumn(image, 1);
            tempGrid.Children.Add(image);

            //Text
            TextBlock websiteBlock = new TextBlock();
            websiteBlock.Text = pwList[0];
            websiteBlock.VerticalAlignment = VerticalAlignment.Top;
            websiteBlock.FontWeight = FontWeights.Bold;
            websiteBlock.Margin = new Thickness(0, 3, 0, 0);
            Grid.SetRow(websiteBlock, 1);
            Grid.SetColumn(websiteBlock, 2);
            tempGrid.Children.Add(websiteBlock);

            TextBlock usernameBlock = new TextBlock();
            usernameBlock.Text = pwList[1];
            usernameBlock.VerticalAlignment = VerticalAlignment.Bottom;
            usernameBlock.FontWeight = FontWeights.Light;
            usernameBlock.FontSize = 11;
            usernameBlock.Margin = new Thickness(0, 0, 0, 4);
            Grid.SetRow(usernameBlock, 1);
            Grid.SetColumn(usernameBlock, 2);
            tempGrid.Children.Add(usernameBlock);

            //Nav
            Image launchImage = new Image();
            launchImage.Source = new BitmapImage(new Uri("/Resources/Pngs/Launch.png", UriKind.Relative));
            launchImage.Margin = new Thickness(15, 0, 0, 0);
            launchImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => {LaunchSite(MakeWebsiteValid(pwList[3]));});
            launchImage.Name = "launchImage";
            launchImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(launchImage, 3);
            Grid.SetRow(launchImage, 1);
            tempGrid.Children.Add(launchImage);

            Image userImage = new Image();
            userImage.Source = new BitmapImage(new Uri("/Resources/Pngs/copyUser.png", UriKind.Relative));
            userImage.Margin = new Thickness(15, 0, 0, 0);
            userImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { Clipboard.SetText(pwList[1]); });
            userImage.Name = "userImage";
            userImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(userImage, 4);
            Grid.SetRow(userImage, 1);
            tempGrid.Children.Add(userImage);

            Image pwImage = new Image();
            pwImage.Source = new BitmapImage(new Uri("/Resources/Pngs/copyPw.png", UriKind.Relative));
            pwImage.Margin = new Thickness(15, 0, 0, 0);
            pwImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { Clipboard.SetText(pwList[2]); });
            pwImage.Name = "PwImage";
            pwImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(pwImage, 5);
            Grid.SetRow(pwImage, 1);
            tempGrid.Children.Add(pwImage);

            Image editImage = new Image();
            editImage.Source = new BitmapImage(new Uri("/Resources/Pngs/edit.png", UriKind.Relative));
            editImage.Margin = new Thickness(15, 0, 0, 0);
            editImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.EditPw(_index); });
            editImage.Name = "editImage";
            editImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(editImage, 6);
            Grid.SetRow(editImage, 1);
            tempGrid.Children.Add(editImage);

            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new Uri("/Resources/Pngs/delete.png", UriKind.Relative));
            deleteImage.Margin = new Thickness(15, 0, 0, 0);
            deleteImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.RemovePw(_index); });
            deleteImage.Name = "deleteImage";
            deleteImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(deleteImage, 7);
            Grid.SetRow(deleteImage, 1);
            tempGrid.Children.Add(deleteImage);


            tempGrid.MouseEnter += new MouseEventHandler((o, e) => { Enter(); });
            tempGrid.MouseLeave += new MouseEventHandler((o, e) => { Leave(); });


            void Leave()
            {
                launchImage.Visibility = Visibility.Hidden;
                userImage.Visibility = Visibility.Hidden;
                pwImage.Visibility = Visibility.Hidden;
                editImage.Visibility = Visibility.Hidden;
                deleteImage.Visibility = Visibility.Hidden;
            }

            void Enter()
            {
                launchImage.Visibility = Visibility.Visible;
                userImage.Visibility = Visibility.Visible;
                pwImage.Visibility = Visibility.Visible;
                editImage.Visibility = Visibility.Visible;
                deleteImage.Visibility = Visibility.Visible;
            }

            return tempGrid;
        }

        public Grid NoteGrid(List<String> NoteList, int _index)
        {
            Grid tempGrid = new Grid();
            tempGrid.Height = 38;
            tempGrid.Background = new SolidColorBrush(Colors.Transparent);

            //Row def
            RowDefinition row0 = new RowDefinition();
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row0.Height = new GridLength(1);
            row1.Height = new GridLength(35);
            row2.Height = new GridLength(2);

            tempGrid.RowDefinitions.Add(row0);
            tempGrid.RowDefinitions.Add(row1);
            tempGrid.RowDefinitions.Add(row2);

            //Column def
            ColumnDefinition column0 = new ColumnDefinition();
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            ColumnDefinition column4 = new ColumnDefinition();
            ColumnDefinition column5 = new ColumnDefinition();          
            column0.Width = new GridLength(85);
            //column1.Width = new GridLength();
            column2.Width = new GridLength(40);
            column3.Width = new GridLength(35);
            column4.Width = new GridLength(35);
            column5.Width = new GridLength(20);

            tempGrid.ColumnDefinitions.Add(column0);
            tempGrid.ColumnDefinitions.Add(column1);
            tempGrid.ColumnDefinitions.Add(column2);
            tempGrid.ColumnDefinitions.Add(column3);
            tempGrid.ColumnDefinitions.Add(column4);
            tempGrid.ColumnDefinitions.Add(column5);


            //Spacer
            Label spacerLabel = new Label();
            spacerLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5f5f5f"));
            spacerLabel.SetValue(Grid.ColumnSpanProperty, 6);

            Grid.SetRow(spacerLabel, 0);
            Grid.SetColumn(spacerLabel, 0);
            Grid.SetRow(spacerLabel, 2);
            Grid.SetColumn(spacerLabel, 0);
            tempGrid.Children.Add(spacerLabel);


            //Text
            TextBlock titleBlock = new TextBlock();
            titleBlock.Text = NoteList[0];
            titleBlock.VerticalAlignment = VerticalAlignment.Top;
            titleBlock.FontWeight = FontWeights.Bold;
            titleBlock.FontSize = 16;
            Grid.SetRow(titleBlock, 1);
            Grid.SetColumn(titleBlock, 1);
            tempGrid.Children.Add(titleBlock);

            //Launch
            Image launchImage = new Image();
            launchImage.Source = new BitmapImage(new Uri("/Resources/Pngs/Launch.png", UriKind.Relative));
            launchImage.Margin = new Thickness(15, 0, 0, 0);
            launchImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.ViewNotePanel(_index); });
            launchImage.Name = "launchImage";
            launchImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(launchImage, 2);
            Grid.SetRow(launchImage, 1);
            tempGrid.Children.Add(launchImage);

            //edit
            Image editImage = new Image();
            editImage.Source = new BitmapImage(new Uri("/Resources/Pngs/edit.png", UriKind.Relative));
            editImage.Margin = new Thickness(15, 0, 0, 0);
            editImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.EditNote(_index); });
            editImage.Name = "editImage";
            editImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(editImage, 3);
            Grid.SetRow(editImage, 1);
            tempGrid.Children.Add(editImage);

            //delete
            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new Uri("/Resources/Pngs/delete.png", UriKind.Relative));
            deleteImage.Margin = new Thickness(15, 0, 0, 0);
            deleteImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.RemoveNote(_index); });
            deleteImage.Name = "deleteImage";
            deleteImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(deleteImage, 4);
            Grid.SetRow(deleteImage, 1);
            tempGrid.Children.Add(deleteImage);

            tempGrid.MouseEnter += new MouseEventHandler((o, e) => { Enter(); });
            tempGrid.MouseLeave += new MouseEventHandler((o, e) => { Leave(); });


            void Leave()
            {
                launchImage.Visibility = Visibility.Hidden;
                editImage.Visibility = Visibility.Hidden;
                deleteImage.Visibility = Visibility.Hidden;
            }

            void Enter()
            {
                launchImage.Visibility = Visibility.Visible;
                editImage.Visibility = Visibility.Visible;
                deleteImage.Visibility = Visibility.Visible;
            }

            return tempGrid;
        }

        public Grid ContactGrid(List<String> contactList, int _index)
        {
            Grid tempGrid = new Grid();
            tempGrid.Height = 38;
            tempGrid.Background = new SolidColorBrush(Colors.Transparent);

            //Row def
            RowDefinition row0 = new RowDefinition();
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row0.Height = new GridLength(1);
            row1.Height = new GridLength(35);
            row2.Height = new GridLength(2);

            tempGrid.RowDefinitions.Add(row0);
            tempGrid.RowDefinitions.Add(row1);
            tempGrid.RowDefinitions.Add(row2);

            //Column def
            ColumnDefinition column0 = new ColumnDefinition();
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            ColumnDefinition column4 = new ColumnDefinition();
            ColumnDefinition column5 = new ColumnDefinition();
            column0.Width = new GridLength(85);
            //column1.Width = new GridLength();
            column2.Width = new GridLength(40);
            column3.Width = new GridLength(35);
            column4.Width = new GridLength(35);
            column5.Width = new GridLength(20);

            tempGrid.ColumnDefinitions.Add(column0);
            tempGrid.ColumnDefinitions.Add(column1);
            tempGrid.ColumnDefinitions.Add(column2);
            tempGrid.ColumnDefinitions.Add(column3);
            tempGrid.ColumnDefinitions.Add(column4);
            tempGrid.ColumnDefinitions.Add(column5);


            //Spacer
            Label spacerLabel = new Label();
            spacerLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5f5f5f"));
            spacerLabel.SetValue(Grid.ColumnSpanProperty, 6);

            Grid.SetRow(spacerLabel, 0);
            Grid.SetColumn(spacerLabel, 0);
            Grid.SetRow(spacerLabel, 2);
            Grid.SetColumn(spacerLabel, 0);
            tempGrid.Children.Add(spacerLabel);


            //Text
            TextBlock titleBlock = new TextBlock();
            titleBlock.Text = contactList[0];
            titleBlock.VerticalAlignment = VerticalAlignment.Top;
            titleBlock.FontWeight = FontWeights.Bold;
            titleBlock.FontSize = 16;
            Grid.SetRow(titleBlock, 1);
            Grid.SetColumn(titleBlock, 1);
            tempGrid.Children.Add(titleBlock);

            //Launch
            Image launchImage = new Image();
            launchImage.Source = new BitmapImage(new Uri("/Resources/Pngs/Launch.png", UriKind.Relative));
            launchImage.Margin = new Thickness(15, 0, 0, 0);
            launchImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.ViewContactPanel(_index); });
            launchImage.Name = "launchImage";
            launchImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(launchImage, 2);
            Grid.SetRow(launchImage, 1);
            tempGrid.Children.Add(launchImage);

            //edit
            Image editImage = new Image();
            editImage.Source = new BitmapImage(new Uri("/Resources/Pngs/edit.png", UriKind.Relative));
            editImage.Margin = new Thickness(15, 0, 0, 0);
            editImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.EditContact(_index); });
            editImage.Name = "editImage";
            editImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(editImage, 3);
            Grid.SetRow(editImage, 1);
            tempGrid.Children.Add(editImage);

            //delete
            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new Uri("/Resources/Pngs/delete.png", UriKind.Relative));
            deleteImage.Margin = new Thickness(15, 0, 0, 0);
            deleteImage.MouseLeftButtonDown += new MouseButtonEventHandler((o, e) => { _mainWindow.RemoveContact(_index); });
            deleteImage.Name = "deleteImage";
            deleteImage.Visibility = Visibility.Hidden;
            Grid.SetColumn(deleteImage, 4);
            Grid.SetRow(deleteImage, 1);
            tempGrid.Children.Add(deleteImage);

            tempGrid.MouseEnter += new MouseEventHandler((o, e) => { Enter(); });
            tempGrid.MouseLeave += new MouseEventHandler((o, e) => { Leave(); });


            void Leave()
            {
                launchImage.Visibility = Visibility.Hidden;
                editImage.Visibility = Visibility.Hidden;
                deleteImage.Visibility = Visibility.Hidden;
            }

            void Enter()
            {
                launchImage.Visibility = Visibility.Visible;
                editImage.Visibility = Visibility.Visible;
                deleteImage.Visibility = Visibility.Visible;
            }

            return tempGrid;
        }

        public Grid FolderGrid(string _name, int _index)
        {
            Grid tempGrid = new Grid();
            tempGrid.Background = new SolidColorBrush(Colors.Transparent);

            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = _name;
            nameBlock.VerticalAlignment = VerticalAlignment.Center;
            nameBlock.HorizontalAlignment = HorizontalAlignment.Left;
            nameBlock.Margin = new Thickness(20,5,0,5);
            tempGrid.Children.Add(nameBlock);

            tempGrid.MouseLeftButtonDown += new MouseButtonEventHandler( (o,e) => { _mainWindow.ViewFolder(_name); _mainWindow.SelectFolder(_index); } );

            return tempGrid;
        }

        public Grid SelectedFolderGrid(string _name, int _index)
        {
            Grid tempGrid = new Grid();
            tempGrid.Background = new BrushConverter().ConvertFrom("#303030") as Brush;

            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = _name;
            nameBlock.VerticalAlignment = VerticalAlignment.Center;
            nameBlock.HorizontalAlignment = HorizontalAlignment.Left;
            nameBlock.Margin = new Thickness(20, 5, 0, 5);
            tempGrid.Children.Add(nameBlock);
 
            return tempGrid;
        }
    }
}