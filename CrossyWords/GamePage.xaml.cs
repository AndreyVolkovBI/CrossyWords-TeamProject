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
using System.Windows.Threading;

namespace CrossyWords
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        DispatcherTimer _disptchertimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

        int _timeForGameLeft = 131;
        int _points = 0;
        bool _trackOn = false;
        List<Cell> cells = new List<Cell>(); // all the cells (id, button)
        Cell previousCell;

        int idOfCell = 1;


        List<string> foundWords = new List<string>();
        List<char> _currentWord = new List<char>();
        List<Button> _chosenButtons = new List<Button>();
        //currentword_textblock

        Repository _repo = Factory.Default.GetRepository();
        UsersData _usersdata = Factory.Default.GetUsersData();
        DatabaseRepository _dbRepository = Factory.Default.GetDatabaseRepository();

        Battle _battle;

        public GamePage(Battle battle = null)
        {
            InitializeComponent();
            if (battle != null)
                _battle = battle;
            Init();
            AddButtons();
            FillButtons();
            MakeHandlerForTimer();
            TextBlock_Points.Text = "Points: 0";
        }

        private void Init()
        {
            //if (_dbRepository.GetWords().Count == 0 || _dbRepository.GetAlphabet().Count == 0)
            //    _dbRepository.FillWordsAndAlphabet();
        }

        private void MakeHandlerForTimer()
        {
            EventHandler handler = (sender, args) =>
            {
                Timer();
            };

            _disptchertimer.Tick += handler;
            _repo.GameOn = true;
            _disptchertimer.Start();
            handler(null, null);
            ShowTimerToUser(); //and show info
        }


        private void Timer()
        {
            if (_timeForGameLeft == 0)
            {
                _disptchertimer.Stop();
                SaveInfoAboutBattle();
                _repo.GameOn = false;
                NavigationService.Navigate(new AccountPage()); 
            }
            else
            {
                _timeForGameLeft--;
                ShowTimerToUser();
            }

        }

        private void SaveInfoAboutBattle()
        {
            if (_battle != null && _battle.AllWords == null)
            {
                string allwords = null;
                for (int i = 0; i < cells.Count(); i++)
                {
                    allwords = allwords + cells[i].Button.Content;
                }
                _usersdata.SaveAllInformationAboutBattle(_battle, _points, allwords);
            }
            else if (_battle != null)
            {
                _usersdata.SaveAllInformationAboutBattle(_battle, _points);
            }
        }

        private void ShowTimerToUser()
        {
            int minutes = _timeForGameLeft / 60;
            int seconds = _timeForGameLeft - minutes * 60;
            string secondshow;
            if (seconds < 10)
                secondshow = "0" + seconds.ToString();
            else
                secondshow = seconds.ToString();
            TextBlock_Timer.Text = "Time: " + minutes.ToString() + ":" + secondshow;
        }
   
        private bool CheckPreviousButton(int id)
        {
            foreach (var item in _repo.GetIds(id))
                if (previousCell.Id == item)
                    return true;
            return false;
        }

        private void FillButtons() 
        {
            if (_battle == null || _battle.AllWords == null)
            {
                List<Cell> cellsTemp = _repo.FillAllCells(_dbRepository.GetListChosenWords());
                for (int i = 0; i < cells.Count; i++)
                    cells[i].Button.Content = cellsTemp[i].Value;
            } 
            else
            {
                for (int i = 0; i < cells.Count; i++)
                {
                    cells[i].Button.Content = _battle.AllWords[i];
                }
            }

      
        }

        private void AddButtons()
        {
            for (int i = 0; i < _repo.Dimension; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < _repo.Dimension; i++)
                for (int j = 0; j < _repo.Dimension; j++)
                {
                    var button = new Button();

                    button.FontSize = 30;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Black;
                    button.Height = 60;
                    button.Margin = new Thickness(7);

                    button.Click += Buttotn_Click;
                    button.MouseEnter += Button_MouseEnter;
                    button.MouseLeave += Button_MouseLeave;

                    cells.Add(new Cell { Id = idOfCell, Button = button });
                    idOfCell++;
                    mainGrid.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Margin = new Thickness(7);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Margin = new Thickness(5);
            if (_trackOn)
            {
                foreach (var item in cells)
                    if (item.Button == (sender as Button) && CheckPreviousButton(cells.First(x => x.Button == (sender as Button)).Id) && _chosenButtons.FirstOrDefault(b => b == item.Button) == null)
                    {
                        previousCell = item;
                        item.Button.Background = Brushes.Orange;
                        _chosenButtons.Add(item.Button);
                        _currentWord.Add(char.Parse(item.Button.Content.ToString()));
                        FillTextBox();
                        //need?
                        break;
                    }
                    else if (item.Button == (sender as Button) && _chosenButtons.Count > 1 && _chosenButtons[_chosenButtons.Count - 2] == item.Button)
                    {
                        _chosenButtons.Last().Background = Brushes.White;
                        _chosenButtons.Remove(_chosenButtons.Last());
                        _currentWord.RemoveAt(_currentWord.Count - 1); //smt wrong with Last()
                        previousCell = item;
                        FillTextBox();
                        break; //need?
                    }
            }
        }

        private void Buttotn_Click(object sender, RoutedEventArgs e)
        {
            _trackOn = !_trackOn;
            if (_trackOn)
            {
                foreach (var item in cells)
                    if (item.Button == (sender as Button))
                    {
                        previousCell = item;
                        item.Button.Background = Brushes.Orange;
                        //my
                        _chosenButtons.Add(item.Button);
                        _currentWord.Add(char.Parse(item.Button.Content.ToString()));
                        FillTextBox();
                    }
            }
            else
            {
                foreach (var button in _chosenButtons)
                {
                    button.Background = Brushes.White;
                }
                _chosenButtons = new List<Button>();
                //find currentwordtextbox in database
                _currentWord = new List<char>();
                previousCell = new Cell();
                CheckFinalWord();             
            }
        }


        private void CheckFinalWord()
        {
            bool isWordInList = _dbRepository.IsWordInList(CurrentWord_textblock.Text); //here we check the final word and give balls
            if (isWordInList && !foundWords.Contains(CurrentWord_textblock.Text))
            {
                _points = _points + CurrentWord_textblock.Text.Length * 2;
                TextBlock_Points.Text = "Points: " + _points.ToString();
                foundWords.Add(CurrentWord_textblock.Text);
            }
        }

        private void FillTextBox()
        {
            string currentword = null;

            for (int i = 0; i < _currentWord.Count; i++)
            {
                currentword = currentword + _currentWord[i];
            }
            CurrentWord_textblock.Text = currentword;
        }
    }
}
