using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private Button[] buttons;
        private bool isPlayerTurn;
        private bool isGameOver;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            restart_();
        }

        private void InitializeGame()
        {
            buttons = new Button[9] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
            isPlayerTurn = true;
            isGameOver = false;


            foreach (Button button in buttons)
            {
                button.Content = String.Empty;
                button.IsEnabled = true;
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (isPlayerTurn)
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O";
            }

            button.IsEnabled = false;
            isPlayerTurn = !isPlayerTurn;
            CheckForWin();
            if (!isGameOver && !isPlayerTurn)
            {
                CheckForWin();
            }
        }

        private void MakeComputerMove()
        {
            int buttonIndex = GetRandomEmptyButtonIndex();

            if (buttonIndex != -1)
            {
                buttons[buttonIndex].Content = "O";
                buttons[buttonIndex].IsEnabled = false;
                isPlayerTurn = true;
            }
        }

        private int GetRandomEmptyButtonIndex()
        {
            int[] emptyButtonIndices = new int[9];
            int index = 0;

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Content.ToString() == String.Empty)
                {
                    emptyButtonIndices[index] = i;
                    index++;
                }
            }

            if (index > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(index);
                return emptyButtonIndices[randomIndex];
            }

            return -1;
        }

        private void CheckForWin()
        {
            if (buttons[0].Content == buttons[1].Content && buttons[1].Content == buttons[2].Content && buttons[0].Content.ToString() != String.Empty)
            {
                win(buttons[0].Content.ToString());
            }
            else if(buttons[3].Content == buttons[4].Content && buttons[4].Content == buttons[5].Content && buttons[3].Content.ToString() != String.Empty)
            {
                win(buttons[3].Content.ToString());
            }
            else if(buttons[6].Content == buttons[7].Content && buttons[7].Content == buttons[8].Content && buttons[6].Content.ToString() != String.Empty){
                win(buttons[6].Content.ToString());
            }
            else if(buttons[0].Content == buttons[3].Content && buttons[3].Content == buttons[6].Content && buttons[0].Content.ToString() != String.Empty){
                win(buttons[0].Content.ToString());
            }
            else if(buttons[1].Content == buttons[4].Content && buttons[4].Content == buttons[7].Content && buttons[1].Content.ToString() != String.Empty){
                win(buttons[1].Content.ToString());
            }
            else if(buttons[2].Content == buttons[5].Content && buttons[5].Content == buttons[8].Content && buttons[2].Content.ToString() != String.Empty){
                win(buttons[2].Content.ToString());
            }
            else if(buttons[0].Content == buttons[4].Content && buttons[4].Content == buttons[8].Content && buttons[0].Content.ToString() != String.Empty)
            {
                win(buttons[0].Content.ToString());
            }
            else
            {
                int check = 0;
                foreach(var button in buttons)
                {
                    if(button.Content != "")
                    {
                        check++;
                    }
                }
                if (check == 9)
                {
                    Nichya();
                }
                else
                {
                    MakeComputerMove();
                }
            }
        }
        private void win(string butt)
        {
            if (butt == "X")
            {
                MessageBox.Show("Крестики победили");
                restart_();
            }
            else if (butt == "O")
            {
                MessageBox.Show("Нолики победили");
                restart_();
            }
        }
        private void Nichya()
        {
            MessageBox.Show("Ничья");
        }

        private void start_restart(object sender, RoutedEventArgs e)
        {
            switch (start.Content)
            {
                case "Начать игру":
                    start_();
                    break;
                case "Начать заново":
                    restart_();
                    break;
            }
        }
        private void start_()
        {
            foreach(var button in buttons)
            {
                button.IsEnabled = true;
            }
            start.Content = "Начать заново";
        }
        private void restart_()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = false;
                button.Content = "";
            }
            start.Content = "Начать игру";
        }
    }
}