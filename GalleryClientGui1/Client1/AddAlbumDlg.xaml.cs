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

namespace Client
{
    /// <summary>
    /// Interaction logic for AddAlbumDlg.xaml
    /// </summary>
    public partial class AddAlbumDlg : Window
    {
        public String AlbumName;
        public Boolean IsChanged = false;
        public AddAlbumDlg()
        {
            InitializeComponent();
        }

        private void addAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (textAlbumName.Text.Length == 0)
            {
                MessageBox.Show("Album Name must contain at least one character");
            }
            else
            {
                this.AlbumName = textAlbumName.Text;
                this.IsChanged = true;
                this.Close();
            }
        }
    }
}
