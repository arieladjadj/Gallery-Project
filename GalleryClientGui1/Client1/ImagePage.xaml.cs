using Microsoft.Win32;
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
    /// Interaction logic for ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page
    {
        private String AlbumId, ImageName, CreationDate, ImagePath;
        ListBox listBox;

        public ImagePage(String AlbumId, String albummName)
        {
            InitializeComponent();
            ((Users)System.Windows.Application.Current.MainWindow).imagesChanged += new EventHandler(refreshImages);
            var value = new EventHandler(addImageHandler);
            ((Users)System.Windows.Application.Current.MainWindow).AddImagesHandlers.Add(value);
            ((Users)System.Windows.Application.Current.MainWindow).addImage += value;
            ((Users)System.Windows.Application.Current.MainWindow).BackToImages(albummName);
            this.AlbumId = AlbumId;
            LoadImages(this.AlbumId);
        }

        public void refreshImages(object sender, EventArgs e)
        {
            LoadImages(this.AlbumId);
        }

        public void addImageHandler(object sender, EventArgs e)
        {
            addImage();
        }

        private bool SetImagesDetails()
        {
            // Instantiate the dialog box
            AddImageDlg dlg = new AddImageDlg();

            // Configure the dialog box
            dlg.Owner = ((Users)System.Windows.Application.Current.MainWindow);

            // Open the dialog box modally 
            dlg.ShowDialog();
            this.ImageName = dlg.NewImageName;
            this.CreationDate = DateTime.Now.ToString("d/M/yyyy - HH:mm:ss");
            return dlg.IsChanged;
        }
        public async void addImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";  
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() ==true)
            {
                this.ImagePath = dlg.FileName;
                this.ImageName = System.IO.Path.GetFileName(this.ImagePath);
                this.CreationDate = DateTime.Now.ToString("d/M/yyyy - HH:mm:ss");

                try
                {
                    String msg = await ImagesProcessor.addImage(this.ImageName, this.CreationDate, this.ImagePath, this.AlbumId);
                    MessageBox.Show(msg);
                    LoadImages(this.AlbumId);
                }
                catch
                {
                    MessageBox.Show("Error while adding user");
                }
            }

        }

        public async void LoadImages(String albumId)
        {
            mainGrid.Children.Clear();
            var scroll = new ScrollViewer();
            mainGrid.Children.Add(scroll);
            this.listBox = new ListBox();
            scroll.Content = listBox;
            listBox.DisplayMemberPath = "Text";
            try
            {
                var Images = await ImagesProcessor.GetAllImagesOfAlbum(albumId);
                foreach (var Image in Images)
                {
                    listBox.Items.Add(Image);
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
                String imagePath = (item as ImageModel).Location;
                String imageName = (item as ImageModel).Name;
                ImageViewer dlg = new ImageViewer(imagePath, imageName);

                // Configure the dialog box
                dlg.Owner = ((Users)System.Windows.Application.Current.MainWindow);

                // Open the dialog box modally 
                dlg.ShowDialog();
            }
        }

        private async void MenuItemRename_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            var item = listBox.Items[listBox.SelectedIndex];

            if (SetImagesDetails())
            {
                try
                {
                    String msg = await ImagesProcessor.updateImage((item as ImageModel)._id, this.ImageName, this.CreationDate, (item as ImageModel).Location, this.AlbumId);
                    MessageBox.Show(msg);
                    LoadImages(this.AlbumId);
                }

                catch
                {
                    MessageBox.Show("Error while update image");
                }
            }

        }

        private async void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            var item = listBox.Items[listBox.SelectedIndex];
            try
            {
                String msg = await ImagesProcessor.deleteImage((item as ImageModel)._id);
                MessageBox.Show(msg);
                LoadImages(this.AlbumId);
            }
            catch
            {
                MessageBox.Show("Error while deleting image");
            }
        }
    }
}
