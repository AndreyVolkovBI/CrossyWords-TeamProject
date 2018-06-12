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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrossyWords
{
    /// <summary>
    /// Логика взаимодействия для WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        public void DownloadLine()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(10));
            DoubleAnimation dbani = new DoubleAnimation(150.0, dur);
            dbani.Completed += new EventHandler(anim_Completed);
            BeginAnimation(ProgressBar.ValueProperty, dbani);
        }

        private void anim_Completed(object sender, EventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }
    }
}
