using CrossyWords.Core;
using CrossyWords.Core.API.OxfordDictionary;
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
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class GameSettingsPage : Page
    {
        public GameSettingsPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            using (var context = new Context())
            {
                for (int i = 3; i < 7; i++)
                    dimensionComboBox.Items.Add(i);

                List<Word> categories = context.Categories.ToList();

                foreach (var item in categories)
                    categoryComboBox.Items.Add(item);

            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new GamePage());
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new GamePage());
        }
    }
}
