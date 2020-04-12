using System;
using System.Collections.Generic;
using System.IO;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for ImageViewer.xaml
    /// </summary>
    public partial class ImageViewer : Window
    {
        public ImageViewer(String path, String imageName)
        {
            InitializeComponent();
            displayImage(path, imageName);
        }

        private void displayImage(String path, String imageName)
        {
            try
            {
                byte[] binaryData = Convert.FromBase64String(path);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.EndInit();
                //var uriSource = new Uri(path, UriKind.Absolute);
                //ImageNameLabel.Content = imageName;
                //Image.Source = new BitmapImage(uriSource);
                Image.Source = bi;
                ImageNameLabel.Content = imageName;
            }
            catch
            {
                MessageBox.Show("Failes to load image");
            }
        }
    }
}
