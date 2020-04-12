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

namespace Client
{
    /// <summary>
    /// Interaction logic for AlbumsPage.xaml
    /// </summary>
    public partial class AlbumsPage : Page
    {
        String UserId, AlbumName, CreationDate;
        ListBox listBox;
        public AlbumsPage(String userId, String userName)
        {
            this.UserId= userId;
            InitializeComponent();
            ((Users)System.Windows.Application.Current.MainWindow).albumsChanged += new EventHandler(refreshAlbums);
            var value = new EventHandler(addAlbumHandler);
            ((Users)System.Windows.Application.Current.MainWindow).addAlbum += value;
            ((Users)System.Windows.Application.Current.MainWindow).AddAlbumsHandlers.Add(value);
            ((Users)System.Windows.Application.Current.MainWindow).BackToAlbums(userName);
            LoadAlbums(userId);
            // Keep this page in navigation history
            this.KeepAlive = false;
        }

        public void refreshAlbums(object sender, EventArgs e)
        {
            LoadAlbums(this.UserId);
        }

        public void addAlbumHandler(object sender, EventArgs e)
        {
            addAlbum();
        }
        private bool SetAlbumsDetails()
        {
            // Instantiate the dialog box
            AddAlbumDlg dlg = new AddAlbumDlg();

            // Configure the dialog box
            dlg.Owner = ((Users)System.Windows.Application.Current.MainWindow);
            //  this.AlbumName = dlg.AlbumName; // = this.documentTextBox.Margin;

            // Open the dialog box modally 
            dlg.ShowDialog();
            this.AlbumName = dlg.AlbumName;
            this.CreationDate =  DateTime.Now.ToString("d/M/yyyy - HH:mm:ss");

            return dlg.IsChanged;
        }
        public async void addAlbum()
        {
            if (SetAlbumsDetails())
            {
                try
                {
                    String msg = await AlbumsProcessor.addAlbum(this.AlbumName, this.CreationDate, this.UserId);
                    MessageBox.Show(msg);
                    LoadAlbums(this.UserId);
                }
                catch
                {
                    MessageBox.Show("Error while adding user");
                }
            }

        }

        public async void LoadAlbums(String userId)
        {
            mainGrid.Children.Clear();
            var scroll = new ScrollViewer();
            mainGrid.Children.Add(scroll);
            this.listBox= new ListBox();
            scroll.Content = listBox;
            listBox.DisplayMemberPath = "Text";
            try
            {
                var albums = await AlbumsProcessor.GetAllAlbumsOfUser(userId);
                foreach (var album in albums)
                {
                    listBox.Items.Add(album);
                }
            }
            catch
            {
                MessageBox.Show("Error while loading users");
            }
            ContextMenu contextMenu = new ContextMenu();

            MenuItem item = new MenuItem();
            item.Header = "Rename";
            item.Click += MenuItemRename_Click;
            contextMenu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Delete";
            item.Click += MenuItemDelete_Click;
            contextMenu.Items.Add(item);

            scroll.ContextMenu = contextMenu;
            listBox.AddHandler(UIElement.MouseLeftButtonDownEvent,
            new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        }

        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem;
            if (item != null)
            {
                ((Users)System.Windows.Application.Current.MainWindow).albumSelected((item as AlbumModel)._id, (item as AlbumModel).Name);
               // MessageBox.Show((item as AlbumModel)._id);
            }
        }

        private async void MenuItemRename_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            var item = listBox.Items[listBox.SelectedIndex];

            if (SetAlbumsDetails())
            {
                try
                {
                    String msg = await AlbumsProcessor.updateAlbum((item as AlbumModel)._id,this.AlbumName, this.CreationDate, this.UserId);
                    MessageBox.Show(msg);
                    LoadAlbums(this.UserId);
                }

                catch
                {
                    MessageBox.Show("Error while update user");
                }
            }

        }

        private async void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            var item = listBox.Items[listBox.SelectedIndex];
            try
            {
                String msg = await AlbumsProcessor.deleteAlbum((item as AlbumModel)._id);
                MessageBox.Show(msg);
                LoadAlbums(this.UserId);
            }
            catch
            {
                MessageBox.Show("Error while deleting user");
            }
        }
    }
}
