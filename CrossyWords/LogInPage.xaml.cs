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
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        UsersData _usersdata = Factory.Default.GetUsersData();
        public LogInPage()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNullFields())
            {
                //var user = _usersdata.FindUser(textbox_NickName.Text, textbox_Password.Password);
                //if (user == null)
                //    MessageBox.Show("Your account was not found", "Wrong data", MessageBoxButton.OK, MessageBoxImage.Stop);
                //else                
                //    NavigationService.Navigate(new GamePage());
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage());
        }

        private bool CheckNullFields()
        {
            if (string.IsNullOrWhiteSpace(textbox_NickName.Text) || string.IsNullOrWhiteSpace(textbox_Password.Password))
            {
                MessageBox.Show("You should fill all fields for registration");
                return false;
            }
            else
                return true;
        }


    }
}
