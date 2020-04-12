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
using System.Net.Http;
using System.Windows.Navigation;

namespace Client
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>

    public partial class Users : Window
    {
        static readonly HttpClient client = new HttpClient();
        public List<EventHandler> AddUsersHandlers = new List<EventHandler>();  //users
        public List<EventHandler> AddAlbumsHandlers = new List<EventHandler>(); //albums
        public List<EventHandler> AddImagesHandlers = new List<EventHandler>(); //images

        public event EventHandler usersChanged, addUser;  //users
        public event EventHandler albumsChanged, addAlbum; //albums
        public event EventHandler imagesChanged, addImage; //images

        public Users()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
           
         }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (page.Content.GetType().ToString().Contains("AlbumsPage"))
            {
                addAlbum(this, e);
            }
            else if (page.Content.GetType().ToString().Contains("ImagePage"))
            {
                addImage(this, e);
            }
            else 
            {
                addUser(this, e);
            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            if (page.Content.GetType().ToString().Contains("AlbumsPage"))
            {
                albumsChanged(this, e);
            }
            else
            {
                usersChanged(this, e);
            }
            
        }

        public void userSelected(String userId, String userName)
        {
            foreach (EventHandler eh in AddUsersHandlers)
            {
                addUser -= eh;
            }
            foreach (EventHandler eh in AddAlbumsHandlers)
            {
                addAlbum -= eh;
            }
            AlbumsPage p = new AlbumsPage(userId, userName);
            page.Navigate(p);
            //page.Content = new AlbumsPage(userId);
        }

        public void albumSelected(String albumId, String albumName)
        {
            foreach (EventHandler eh in AddImagesHandlers)
            {
                addImage -= eh;
            }
            foreach (EventHandler eh in AddAlbumsHandlers)
            {
                addAlbum -= eh;
            }
            ImagePage p = new ImagePage(albumId, albumName);
            page.Navigate(p);
        }

        public void BackToUsers()
        {
            pageType.Content = "Users:";
        }

        public void BackToAlbums(String userName)
        {
            pageType.Content = $"{userName}'s Albums:";
        }

        public void BackToImages(String albumName)
        {
            pageType.Content = $"{albumName}'s Images:";
        }
    }
}
