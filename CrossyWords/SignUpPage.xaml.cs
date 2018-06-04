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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        UsersData _users = Factory.Default.GetUsersData();

        public SignUpPage()
        {
            InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNullFields())
            {
                if (_users.UniqueNickName(textbox_NickName.Text))
                {
                    _users.AddNewUser(textbox_NickName.Text, textbox_Password.Text);
                    MessageBox.Show("You have successfully registered!", "Thank You", MessageBoxButton.OK);
                    NavigationService.Navigate(new LogInPage());
                }
                else
                    MessageBox.Show("Unfortunately, your nickname already exists. Please, choose another nickname.", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                
            }

        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }

        private bool CheckNullFields()
        {
            if (string.IsNullOrWhiteSpace(textbox_NickName.Text) || string.IsNullOrWhiteSpace(textbox_Password.Text))
            {
                MessageBox.Show("You should fill all fields for registration");
                return false;
            }
            else
                return true;
        }
    }
}
