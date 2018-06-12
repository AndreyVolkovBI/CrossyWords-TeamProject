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
    /// Логика взаимодействия для UserSettings.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        

        UsersData _usersdata = Factory.Default.GetUsersData();

        public SettingsPage()
        {
            InitializeComponent();
            FillInformation();
        }

        private void FillInformation()
        {

        }

        private void Button_Game_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());

        }

        private void Button_ChallengePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChallangePage());

        }

        private void Button_Rating_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RatingPage());

        }

        private void Button_SaveChanges(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textbox_NickName.Text)) //прописать причем пароль может быть пустым
            {
                if (true) //AllowChanges
                {
                    if (true) //change
                    {

                    }
                    else
                    {
                        MessageBox.Show("Your new name is not unique", "This name is already taken");
                    }
                }
                else
                {
                    MessageBox.Show("You write wrong current password", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            else
            {
                MessageBox.Show("You should write your Name", "Name was null", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Button_SendReview_Click(object sender, RoutedEventArgs e)
        {
            _usersdata.SendReview(TextBox_Review.Text);
        }
    }
}
