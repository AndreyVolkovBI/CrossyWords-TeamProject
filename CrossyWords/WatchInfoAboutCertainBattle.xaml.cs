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
    /// Логика взаимодействия для WatchInfoAboutCertainBattle.xaml
    /// </summary>
    public partial class WatchInfoAboutCertainBattle : Page
    {
        Battle _battle;

        public WatchInfoAboutCertainBattle(Battle battle)
        {
            InitializeComponent();
            _battle = battle;
        }
    }
}
