﻿using CrossyWords.Core;
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
    /// Логика взаимодействия для UserRating.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        UsersData _usersdata = Factory.Default.GetUsersData();

        public RatingPage()
        {
            InitializeComponent();
            FillRating();
        }

        private void FillRating()
        {
            DataGridRating.ItemsSource = _usersdata.GetRatingOfUsers();
        }

        
    }
}
