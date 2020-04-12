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
    /// Interaction logic for AddImageDlg.xaml
    /// </summary>
    public partial class AddImageDlg : Window
    {
        public String NewImageName, NewImagePath;
        public Boolean IsChanged = false;
        public AddImageDlg()
        {
            InitializeComponent();
        }

        private void addImage_Click(object sender, RoutedEventArgs e)
        {
            if (textImage.Text.Length == 0 ) 
            {
                MessageBox.Show("Image name must contain at least one character");
            }
            else
            {
                this.NewImageName = textImage.Text;
                this.IsChanged = true;
                this.Close();
            }
        }
    }
}
