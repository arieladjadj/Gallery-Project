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
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private String NewUserName;
        private ListBox listBox;
        public  UsersPage()
        {
            InitializeComponent();
            ((Users)System.Windows.Application.Current.MainWindow).usersChanged += new EventHandler(refreshUsers);
            var value = new EventHandler(addUserHandler);
            ((Users)System.Windows.Application.Current.MainWindow).AddUsersHandlers.Add(value);
            ((Users)System.Windows.Application.Current.MainWindow).addUser +=value;
            ((Users)System.Windows.Application.Current.MainWindow).BackToUsers();
            LoadUsers();
        }

        public void refreshUsers(object sender, EventArgs e)
        {
            LoadUsers();
        }

        public void addUserHandler(object sender, EventArgs e)
        {
            addUser();
        }

        private Boolean SetNewUserName()
        {
            // Instantiate the dialog box
            addUserDlg dlg = new addUserDlg();

            // Configure the dialog box
            dlg.Owner = ((Users)System.Windows.Application.Current.MainWindow);

            // Open the dialog box modally 
            dlg.ShowDialog();
            this.NewUserName = dlg.NewUserName;
           return dlg.changed; 
            
        }
        public async void addUser()
        {
            if (SetNewUserName())
            {
                try
                {
                    String msg = await UsersProcessor.addUser(this.NewUserName);
                    MessageBox.Show(msg);
                    LoadUsers();
                }
                catch
                {
                    MessageBox.Show("Error while adding user");
                }
            }
            
        }
            public async void LoadUsers()
        {
            mainGrid.Children.Clear();
            var scroll = new ScrollViewer();
            mainGrid.Children.Add(scroll);
            this.listBox = new ListBox();
            scroll.Content = listBox;
            listBox.DisplayMemberPath = "Name";
            try
            {
                var users = await UsersProcessor.GetAllUsers();            
                foreach (var user in users)
                {
                    listBox.Items.Add(user);
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
           // listBox.AddHandler(UIElement.MouseRightButtonDownEvent, new MouseButtonEventHandler(OnMouseRightButtonDown), true);
        }


        private void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem;
            if (item != null)
            {
                ((Users)System.Windows.Application.Current.MainWindow).userSelected((item as UserModel)._id, (item as UserModel).Name);
                //MessageBox.Show((item as UserModel)._id);
            }
        }

        private void OnMouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem;
            if (item != null)
            {
                //((Users)System.Windows.Application.Current.MainWindow).userSelected((item as UserModel)._id);
                MessageBox.Show((item as UserModel).Name);
                MessageBox.Show("Right click");
            }
        }

        private async void MenuItemRename_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedIndex == -1) return;

            var item = listBox.Items[listBox.SelectedIndex];

            if (SetNewUserName())
            {
                try
                {
                    String msg = await UsersProcessor.updateUser((item as UserModel)._id, this.NewUserName);
                    MessageBox.Show(msg);
                    LoadUsers();
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
                String msg = await UsersProcessor.deleteUser((item as UserModel)._id);
                MessageBox.Show(msg);
                LoadUsers();
            }
            catch
            {
                MessageBox.Show("Error while deleting user");
            }
        }

        private void listBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            listBox.UnselectAll();
        }

    }
}
