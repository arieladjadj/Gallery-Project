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
    /// Interaction logic for addUserDlg.xaml
    /// </summary>
    public partial class addUserDlg : Window
    {
        public String NewUserName;
        public Boolean changed = false;
        public addUserDlg()
        {
            InitializeComponent();
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            if (textUsername.Text.Length == 0)
            {
                MessageBox.Show("Username must contain at least one character");
            }
            else
            {
                this.NewUserName = textUsername.Text;
                this.changed = true;
                this.Close();
            }
        }
    }
}
