using CrossyWords.Core;
using CrossyWords.Core.Users;
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
    /// Логика взаимодействия для UserChallenge.xaml
    /// </summary>
    public partial class ChallangePage : Page
    {
        UsersData _usersdata = Factory.Default.GetUsersData();
        public ChallangePage()
        {
            InitializeComponent();
            TextBox_FindUser.Focus();
        }

        private void Button_Game_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void Button_Rating_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RatingPage());
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SettingsPage());
        }
      
        private void UpdateDataGrid()
        {
            DataGridOpponents.ItemsSource = null;
            DataGridOpponents.ItemsSource = _usersdata.FindOpponents(TextBox_FindUser.Text);
        }

        private void Button_MakeBattle_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = DataGridOpponents.SelectedItem as User;
            if (selectedUser == null)
                MessageBox.Show("Please, choose opponent for making battle", "Opponent", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
            {
                if (_usersdata.CheckLimitationsInGames())
                    SendUserToGame(_usersdata.MakeBattle(selectedUser));
                else
                    MessageBox.Show("You already have more than 4 games. Please, finish them.", "Limitations", MessageBoxButton.OK, MessageBoxImage.Hand);
                
            }
        }

        private void SendUserToGame(Battle _createdbattle)
        {
            NavigationService.Navigate(new GamePage(_createdbattle));
        }

        private void Button_MakeRandomBattle_Click(object sender, RoutedEventArgs e)
        {
            if (_usersdata.IsMakingBattleAllowable())
            {
                if (_usersdata.CheckLimitationsInGames())
                    SendUserToGame(_usersdata.MakeRandomBattle());
                else
                    MessageBox.Show("You already have more than 4 games. Please, finish them.", "Limitations", MessageBoxButton.OK, MessageBoxImage.Hand);               
            }
            else
                MessageBox.Show("There's no any suitable user at the moment for you", "Try later", MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void TextBox_FindUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox_FindUser.Text))
            {
                DataGridOpponents.ItemsSource = null;
            }
            else
                UpdateDataGrid();
        }


    }
}
