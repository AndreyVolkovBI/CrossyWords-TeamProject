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
    public partial class UserGames : Page
    {
        public UserGames()
        {
            InitializeComponent();
        }



        private void Button_FastGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GamePage());
        }

        private void Button_ChallengePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserChallenge());

        }

        private void Button_Rating_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserRating());

        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserSettings());

        }
    }
}
