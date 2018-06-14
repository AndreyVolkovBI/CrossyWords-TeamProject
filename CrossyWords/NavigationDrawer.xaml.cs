using CrossyWords.Core;
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

namespace CrossyWords
{
    /// <summary>
    /// Логика взаимодействия для NavigationDrawer.xaml
    /// </summary>
    public partial class NavigationDrawer : Page
    {
        IRepository _repo = Factory.Default.GetRepository<Repository>();

        public NavigationDrawer()
        {
            InitializeComponent();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Page usc = null;

            if (!_repo.GameOn)
            {
                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {
                    case "AccountItem":
                        usc = new AccountPage();
                        GridMain_Frame.Navigate(usc);
                        break;
                    case "GameItem":
                        usc = new GamePage();
                        GridMain_Frame.Navigate(usc);
                        _repo.GameOn = true;
                        break;
                    case "ChallangeItem":
                        usc = new ChallangePage();
                        GridMain_Frame.Navigate(usc);
                        break;
                    case "RatingItem":
                        usc = new RatingPage();
                        GridMain_Frame.Navigate(usc);
                        break;
                    case "SettingsItem":
                        usc = new SettingsPage();
                        GridMain_Frame.Navigate(usc);
                        break;
                    case "Logout":
                        usc = new LogInPage();
                        NavigationService.Navigate(usc);
                        break;
                    default:
                        break;
                }

                foreach (var item in listView.Items)
                    (item as ListViewItem).Background = Brushes.DarkBlue;
                (listView.SelectedItem as ListViewItem).Background = Brushes.White;
            }
        }
    }
}
