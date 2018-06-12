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
    /// Логика взаимодействия для UserGames.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        UsersData _usersData = Factory.Default.GetUsersData(); 
        public AccountPage()
        {
            InitializeComponent();
            ShowAllInformation();
        }

        private void ShowAllInformation()
        {
            TextBlock_Win.Text = "Win: " + _usersData.User.Win.ToString();
            TextBlock_Draw.Text = "Draw: " + _usersData.User.Draw.ToString();
            TextBlock_Loss.Text = "Loss: " + _usersData.User.Lose.ToString();

            DataGridInfoBattles.ItemsSource = _usersData.GetAllCurrentBattles();

        }

        private void Button_ChallengePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChallangePage());

        }

        private void Button_Rating_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RatingPage());

        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SettingsPage());

        }
    }
}
