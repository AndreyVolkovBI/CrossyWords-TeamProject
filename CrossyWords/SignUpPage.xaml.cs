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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        UsersData _usersdata;

        public SignUpPage(UsersData usersdata)
        {
            InitializeComponent();
            textbox_NickName.Focus();
            _usersdata = usersdata;
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNullFields())
                if (CheckLengthOfFields())
                {

                    if (DetermineUniquiness())
                    {
                        try
                        {
                            _usersdata.AddNewUser(textbox_NickName.Text, textbox_Password.Text);
                            NavigationService.Navigate(new LogInPage());
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error with adding your account. We will try to solve this problem in the nearest time. Sorry for inconvenience.", "Error with data base", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show("User with such email addres has already been registrated. Please, choose another email or login if you have account or if it application error - wait for some time while we will fix this problem.", "Email is not unique or data error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage()); //nul or get optimization
        }


        private bool CheckNullFields()
        {
            if (string.IsNullOrWhiteSpace(textbox_NickName.Text) || string.IsNullOrWhiteSpace(textbox_Password.Text))
            {
                MessageBox.Show("You should fill all fields for authorization");
                return false;
            }
            else
                return true;
        }

        private bool CheckLengthOfFields()
        {
            if (textbox_NickName.Text.Length < 4 || textbox_Password.Text.Length < 4)
            {
                MessageBox.Show("All fields must contain more than 3 letters");
                return false;
            }
            else
                return true;
        }

        private bool DetermineUniquiness()
        {
            try
            {
                return _usersdata.UniqueName(textbox_NickName.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error with determination of uniquiness your email. We will try to solve this problem in the nearest time. Sorry for inconvenience.", "Error operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
