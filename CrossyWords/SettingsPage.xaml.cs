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
            textbox_NickName.Text = _usersdata.User.Name;

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
            AcceptChangesWithName();
            AcceptChangesWithPassword();
        }

        private void AcceptChangesWithPassword()
        {
            if (!string.IsNullOrWhiteSpace(textbox_CurrentPassword.Password) || !string.IsNullOrWhiteSpace(TextBox_NewPassword.Password) || !string.IsNullOrWhiteSpace(TextBox_RepeatPassword.Password))
            {
                if (string.IsNullOrWhiteSpace(textbox_CurrentPassword.Password) || string.IsNullOrWhiteSpace(TextBox_NewPassword.Password) || string.IsNullOrWhiteSpace(TextBox_RepeatPassword.Password))
                    MessageBox.Show("You must fill all fields connecting to password for changing it", "Null exception", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (_usersdata.FindUser(textbox_NickName.Text, textbox_CurrentPassword.Password))
                    {
                        if (TextBox_NewPassword.Password == TextBox_RepeatPassword.Password)
                        {
                            _usersdata.ChangeUserInformation(null, TextBox_NewPassword.Password);
                            textbox_CurrentPassword.Password = null;
                            TextBox_NewPassword.Password = null;
                            TextBox_RepeatPassword.Password = null;
                            MessageBox.Show("Password was succesfully changed", "Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Passwords are not equal", "Repeat password != new Password", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Input current password does not coincide to real password", "Password is not correct", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private void AcceptChangesWithName()
        {
            if (string.IsNullOrWhiteSpace(textbox_NickName.Text))
                MessageBox.Show("You cannot have an empty name", "Name was null", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (textbox_NickName.Text != _usersdata.User.Name)
                {
                    if (textbox_NickName.Text.Length < 3 || textbox_NickName.Text.Length > 50)
                    {
                        if (_usersdata.UniqueName(textbox_NickName.Text))
                            _usersdata.ChangeUserInformation(textbox_NickName.Text);
                        else
                            MessageBox.Show("This name is already taken", "Choose another name", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                        MessageBox.Show("Your name must have more than 3 letters and less than 50 letters", "Limitations", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
            }
        }


        private void Button_SendReview_Click(object sender, RoutedEventArgs e)
        {
            _usersdata.SendReview(TextBox_Review.Text);
        }
    }
}
