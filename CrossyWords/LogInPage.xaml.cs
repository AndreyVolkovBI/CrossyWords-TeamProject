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
            textbox_NickName.Focus();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox_NickName.Text) || string.IsNullOrWhiteSpace(textbox_Password.Password))
                MessageBox.Show("You should fill all fields");
            else
            {
                //string text = textbox_Password.Password;
                try
                {                    
                    if (_usersdata.FindUser(textbox_NickName.Text, textbox_Password.Password))
                    {
                        NavigationService.Navigate(new NavigationDrawer()); //User Games           // var user must be got by other window ... I will do it         
                    }
                    else
                        MessageBox.Show("This account was not found");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error operation to data. We will try to solve this problem in the nearest time.", "Sorry for inconvenience", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage(_usersdata));
        }

        


    }
}
