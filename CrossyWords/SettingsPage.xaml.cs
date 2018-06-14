using CrossyWords.Core;
using CrossyWords.Core.API.OxfordDictionary;
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
        DatabaseRepository _repo = Factory.Default.GetDatabaseRepository();

        UsersData _usersdata = Factory.Default.GetUsersData();

        public SettingsPage()
        {
            InitializeComponent();
            textbox_NickName.Text = _usersdata.User.Name;
            Init();
        }

        private void Init()
        {
            if (_repo.Categories.Count == 0)
                _repo.FillCategories();

            foreach (var item in _repo.Categories)
                categoryComboBox.Items.Add(item);

            if (_repo.Levels.Count == 0)
                _repo.FillLevels();

            foreach (var item in _repo.Levels)
                levelComboBox.Items.Add(item);
        }

        
        private void Button_SaveChanges(object sender, RoutedEventArgs e)
        {
            AcceptChangesWithName();
            AcceptChangesWithPassword();
            AcceptChangesWayOfFillingIn();
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
                            if (CheckLengthOfString(TextBox_NewPassword.Password))
                            {
                                _usersdata.ChangeUserInformation(null, TextBox_NewPassword.Password);
                                textbox_CurrentPassword.Password = null;
                                TextBox_NewPassword.Password = null;
                                TextBox_RepeatPassword.Password = null;
                                MessageBox.Show("Password was succesfully changed", "Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                                MessageBox.Show("Your password must have more than 3 letters and less than 50 letters", "Limitations", MessageBoxButton.OK, MessageBoxImage.Error);


                        }
                        else
                            MessageBox.Show("Passwords are not equal", "Repeat password != new Password", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Input current password does not coincide to real password", "Password is not correct", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private bool CheckLengthOfString(string text)
        {
            if (text.Length > 3 && text.Length <= 50)
                return true;
            else
                return false;

        }

        private void AcceptChangesWithName()
        {
            if (string.IsNullOrWhiteSpace(textbox_NickName.Text))
                MessageBox.Show("You cannot have an empty name", "Name was null", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (textbox_NickName.Text != _usersdata.User.Name)
                {
                    if (CheckLengthOfString(textbox_NickName.Text))
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
            if (string.IsNullOrWhiteSpace(TextBox_Review.Text))
            {
                MessageBox.Show("You must write smth for sending review", "Emptry string", MessageBoxButton.OK, MessageBoxImage.None);
                TextBox_Review.Text = null;
            }
            else
            {
                _usersdata.SendReview(TextBox_Review.Text);
                TextBox_Review.Text = null;
                MessageBox.Show("Thank you for review! Your opinion is very important for us.", "Succesful sending", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void levelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (levelComboBox.SelectedIndex != -1)
                categoryComboBox.SelectedIndex = -1;
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedIndex != -1)
                levelComboBox.SelectedIndex = -1;
        }

        private void AcceptChangesWayOfFillingIn()
        {
            if (levelComboBox.SelectedIndex != -1)
            {
                _repo.SelectedLevel = levelComboBox.SelectedItem as Level;
                _repo.SelectedCategory = null;
            }

            if (categoryComboBox.SelectedIndex != -1)
            {
                _repo.SelectedCategory = levelComboBox.SelectedItem as Category;
                _repo.SelectedLevel = null;
            }
        }
    }
}
