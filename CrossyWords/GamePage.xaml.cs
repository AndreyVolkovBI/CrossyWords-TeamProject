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
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        bool _trackOn = false;
        List<Cell> trackedCell; // selected cells
        List<Cell> cells = new List<Cell>(); // all the cells (id, button)
        Cell previousCell;

        int idOfCell = 1;

        List<char> _currentword = new List<char>();
        List<Button> _chosenbuttons = new List<Button>();
        //currentword_textblock

        IRepository _repo = Factory.Default.GetRepository<Repository>();

        //List<Button> _butons;

        public GamePage()
        {
            InitializeComponent();
            Init();
            FillButtons();
        }

        private bool CheckPreviousButton(int id)
        {
            foreach (var item in _repo.GetIds(id))
                if (previousCell.Id == item)
                    return true;
            return false;
        }

        private void FillButtons() //
        {
            List<Cell> cellsTemp = _repo.FillAllCells();

            for (int i = 0; i < cells.Count; i++)
                cells[i].Button.Content = cellsTemp[i].Value;
        }

        private void Init()
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
                    button.Margin = new Thickness(2);

                    button.Click += btn_Click;
                    button.MouseEnter += Button_MouseEnter;
                    button.MouseLeave += Button_MouseLeave;

                    cells.Add(new Cell { Id = idOfCell, Button = button });
                    idOfCell++;
                    mainGrid.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                }

            /*
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].Button.Content = chars[i].ToString();
            }*/
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Margin = new Thickness(2);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Margin = new Thickness(5);
            if (_trackOn)
            {
                foreach (var item in cells)
                    if (item.Button == (sender as Button) && CheckPreviousButton(cells.First(x => x.Button == (sender as Button)).Id) && _chosenbuttons.FirstOrDefault(b => b == item.Button) == null)
                    {
                        previousCell = item;
                        item.Button.Background = Brushes.Yellow;
                        _chosenbuttons.Add(item.Button);
                        _currentword.Add(char.Parse(item.Button.Content.ToString()));
                        FillTextBox();
                        //need?
                        break;
                    }
                    else if (item.Button == (sender as Button) && _chosenbuttons.Count > 1 && _chosenbuttons[_chosenbuttons.Count - 2] == item.Button)
                    {
                        _chosenbuttons.Last().Background = Brushes.LightGray;
                        _chosenbuttons.Remove(_chosenbuttons.Last());
                        _currentword.RemoveAt(_currentword.Count - 1); //smt wrong with Last()
                        previousCell = item;
                        FillTextBox();
                        break; //need?
                    }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            _trackOn = !_trackOn;
            if (_trackOn)
            {
                foreach (var item in cells)
                    if (item.Button == (sender as Button))
                    {
                        previousCell = item;
                        item.Button.Background = Brushes.Yellow;
                        //my
                        _chosenbuttons.Add(item.Button);
                        _currentword.Add(char.Parse(item.Button.Content.ToString()));
                        FillTextBox();
                    }
            }
            else
            {
                foreach (var button in _chosenbuttons)
                {
                    button.Background = Brushes.LightGray;
                }
                _chosenbuttons = new List<Button>();
                //find currentwordtextbox in database
                _currentword = new List<char>();
                previousCell = new Cell();
                bool isWordInList = _repo.IsWordInList(CurrentWord_textblock.Text); //here we check the final word and give balls


            }
        }

        private void FillTextBox()
        {
            string currentword = null;

            for (int i = 0; i < _currentword.Count; i++)
            {
                currentword = currentword + _currentword[i];
            }
            CurrentWord_textblock.Text = currentword;
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
            // CurrentWord_textblock.Text = btn1.Content.ToString();
        }

        private void ToSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SettingsPage());
        }
    }
}
