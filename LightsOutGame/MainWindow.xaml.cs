using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LightsOutGame.assets;
using LightsOutGame.helpers;
using LightsOutGame.Models;
using Newtonsoft.Json;

namespace LightsOutGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[,] LightsMatrix { get; set; }
        public List<Level> LevelsInput { get; set; }
        public int MatrixRows { get; set; }
        public int MatrixColumns { get; set; }
        public Level CurrentLevel { get; set; }
        public int CurrentLevelIndex { get; set; }
        public int ClickCounter { get; set; }
        public int WinsCounter { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            //Read the levels
            var jsonString = new System.Net.WebClient().DownloadString(ConfigurationManager.AppSettings["UrlJsonLevels"]);
            
            //Deserialize the levels
            LevelsInput = JsonConvert.DeserializeObject<List<Level>>(jsonString);

            //Start always from first level
            SetLevel(0);
        }

        #region private_methods

        /// <summary>
        /// Set the current level, set the properties and the On matrix values
        /// </summary>
        /// <param name="levelIndex"></param>
        private void SetLevel(int levelIndex)
        {
            CurrentLevel = LevelsInput[levelIndex];
            CurrentLevelIndex = levelIndex;

            MatrixRows = CurrentLevel.Rows;
            MatrixColumns = CurrentLevel.Columns;
            LightsMatrix = new int[MatrixRows, MatrixColumns];
            OperationsHelper.SetOnValues(CurrentLevel, LightsMatrix);
            lblLevel.Content = CurrentLevel.Name;

            PrintMatrix();
        }

        /// <summary>
        /// Reset Controls to defaults
        /// </summary>
        private void ResetControls()
        {
            lblYouWin.Visibility = Visibility.Hidden;
            lblLevel.Visibility = Visibility.Visible;
            btnLevelChange.Visibility = Visibility.Hidden;
            gridPanel.IsEnabled = true;
            ClickCounter = 0;
        }

        #endregion

        #region Print
        /// <summary>
        /// Print the switch buttons
        /// </summary>
        private void PrintMatrix()
        {
            gridPanel.Children.Clear(); //clear children objects
            gridPanel.ColumnDefinitions.Clear();
            gridPanel.RowDefinitions.Clear();

            //add columns dynamically
            for (int i = 0; i < MatrixColumns; i++)
            {
                var newColumn = new ColumnDefinition();
                newColumn.Name = "column" + i.ToString();
                newColumn.Width = GridLength.Auto;
                this.gridPanel.ColumnDefinitions.Add(newColumn);
            }

            //add rows dynamically
            for (int row = 0; row < MatrixRows; row++)
            {
                var newRows = new RowDefinition();
                newRows.Name = "row" + row.ToString();
                newRows.Height = GridLength.Auto;
                
            
                this.gridPanel.RowDefinitions.Add(newRows);
                for (int col = 0; col < MatrixColumns; col++)
                {
                    UserControl switchBox = new IMAGE_off();

                    if (LightsMatrix[row, col] == 1)
                    {
                        switchBox = new IMAGE_on();
                    }

                    Button btn = new Button();
                    btn.Content = switchBox;
                    btn.RenderSize = new Size(20,20);
                    btn.CommandParameter = Tuple.Create(row, col);
                    btn.Click += SwitchHandler;
                    btn.Background = Brushes.DarkRed;

                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    btn.VerticalAlignment = VerticalAlignment.Bottom;

                    btn.SetValue(Grid.RowProperty, row);
                    btn.SetValue(Grid.ColumnProperty, col);

                    gridPanel.Children.Add(btn);
                }
            }

            lblCounter.Content = ClickCounter;
            lblTrophy.Content = WinsCounter;

            //Check if all the switches are in OFF
            if (OperationsHelper.AllLightsOff(LightsMatrix))
            {
                lblYouWin.Visibility = Visibility.Visible;
                WinsCounter++;
                lblTrophy.Content = WinsCounter;
                gridPanel.IsEnabled = false;
                btnLevelChange.Visibility = Visibility.Visible;
                lblLevel.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Events_Handlers
        /// <summary>
        /// Event handler of the switch buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchHandler(object sender, RoutedEventArgs e)
        {
            var coordinates = (Tuple<int, int>)((Button) sender).CommandParameter;
            //add one click
            ClickCounter++;

            //switch the neighbors states
            OperationsHelper.SetVonNeumannNeighborhood(CurrentLevel, coordinates, LightsMatrix);

            PrintMatrix();
        }


        /// <summary>
        /// reset the current Level to defaults
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            OperationsHelper.SetOnValues(CurrentLevel, LightsMatrix);
            ResetControls();
            PrintMatrix();
        }

        /// <summary>
        /// Change level button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLevelChange_Click(object sender, RoutedEventArgs e)
        {
           if (CurrentLevelIndex < LevelsInput.Count-1) {
               SetLevel(CurrentLevelIndex+1);
               ResetControls();
           }
        }

        #endregion

    }


}
