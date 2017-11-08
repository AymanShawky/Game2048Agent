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
using Game2048Core;

namespace Game2048Agent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameManager _game;
        TileStyle _tileStyle;
        Label[] GuiTiles = new Label[16];
        GamePlayer _player;
        System.Timers.Timer tmr;
        int movements = 0;
        public MainWindow()
        {
            InitializeComponent();
            _game = new GameManager();
            _tileStyle = new TileStyle();
            tmr = new System.Timers.Timer(100);
            tmr.Elapsed += tmr_Elapsed;

            GuiTiles[0] = Tile00;
            GuiTiles[1] = Tile01;
            GuiTiles[2] = Tile02;
            GuiTiles[3] = Tile03;
            GuiTiles[4] = Tile04;
            GuiTiles[5] = Tile05;
            GuiTiles[6] = Tile06;
            GuiTiles[7] = Tile07;
            GuiTiles[8] = Tile08;
            GuiTiles[9] = Tile09;
            GuiTiles[10] = Tile10;
            GuiTiles[11] = Tile11;
            GuiTiles[12] = Tile12;
            GuiTiles[13] = Tile13;
            GuiTiles[14] = Tile14;
            GuiTiles[15] = Tile15;

            UpdateGui();
        }
 
        void UpdateGui()
        {
            for (int i = 0; i < 16; i++)
            {
                GuiTiles[i].Content = _game.AllTiles[i].Value.ToString();
                _tileStyle.Number = _game.AllTiles[i].Value;
                GuiTiles[i].Background = _tileStyle.BackgraundColor;
                GuiTiles[i].Foreground = _tileStyle.TextColor;
            }
            lblScore.Content = _game.Score.ToString();
            lblMoves.Content = movements;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                _game.Move(Direction.Up);
            else if (e.Key == Key.Down)
                _game.Move(Direction.Down);
            else if (e.Key == Key.Right)
                _game.Move(Direction.Right);
            else if (e.Key == Key.Left)
                _game.Move(Direction.Left);
            
            movements += 1;
            UpdateGui();
            
            if (_game.IsGameover == true)
            {
                MessageBox.Show("Game Over!", "Fail", MessageBoxButton.OK,MessageBoxImage.None);
                _game = new GameManager();
            }

        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            _game = new GameManager();
            UpdateGui();
        }
        bool isAIpalyer = false;

        private async void btnAIpalyer_Click(object sender, RoutedEventArgs e)
        {
            isAIpalyer = !isAIpalyer;
            btnAiAgent.Content = isAIpalyer ? "Stop AI Player" : "Start AI Player";
            while (isAIpalyer)
            {
                _player = new GamePlayer(_game);
                Direction direction = _player.NextMovement((int)sldDepth.Value);
                await Task.Delay(1000 - (int)sldSpeed.Value);
                 _game.Move(direction);
                 movements += 1;
                  UpdateGui();

                if (_game.IsGameover)
                {
                    isAIpalyer = false;
                    MessageBox.Show("Game Over!");
                    _game = new GameManager();
                    UpdateGui();
                    btnAiAgent.Content = "Start AI Player";
                }   
            }

            return;

            if (tmr.Enabled)
            {
                tmr.Stop();
                btnAiAgent.Content = "Start AI Player";
            }
            else
            {
                tmr.Start();
                btnAiAgent.Content = "Stop AI Player";
            }
        }

        void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _player = new GamePlayer(_game);
            //Direction direction = _player.NextMovement(2);
            //_game.Move(direction);

            Dispatcher.Invoke(() => { UpdateGui(); });
            
            if (_game.IsGameover)
            {  
                tmr.Enabled = false;
                MessageBox.Show("Game Over!");
                _game = new GameManager();
                Dispatcher.Invoke(() => { UpdateGui(); });
                Dispatcher.Invoke(() => { btnAiAgent.Content = "Start AI Player"; });              
            }
        }

    }
}
