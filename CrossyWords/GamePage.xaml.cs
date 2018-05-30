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
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        IRepository _repo = Factory.Default.GetRepository<Repository>();

        List<Button> _butons;

        public GamePage()
        {
            InitializeComponent();
            _butons = new List<Button> { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20, btn21, btn22, btn23, btn24, btn25};
            FillButtons();
        }

        private void FillButtons()
        {
            List<char> chars = new List<char> { 'Н','Р', 'Т', 'Р', 'И', 'Ы', 'Ы', 'Я', 'А', 'Н', 'И', 'Й', 'Д', 'В', 'Ш', 'Р', 'Р', 'О', 'А', 'Н', 'У', 'Т', 'Й', 'Ы', 'Н' };

            for (int i = 0; i < _butons.Count; i++)
            {
                _butons[i].Content = chars[i];
            }
         //this list must be filled from repository
         
            
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }

        public void GetNeighbours()
        {
            List<int> count = _repo.GetIds(10); // метод, который возвращает id-s ячеек соседей заданной ячейки
        }

        private void Click_Button_btn1(object sender, RoutedEventArgs e)
        {
            CurrentWord_textblock.Text = btn1.Content.ToString();
           
          
        }
    }
}
