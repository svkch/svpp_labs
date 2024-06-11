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

namespace svpp_lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private double centerX;
        private double centerY;
        private double radius;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;

            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateRaceTrackParameters();
            SetInitialPositions();
            ResetHorses();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateRaceTrackParameters();
            SetInitialPositions();
            ResetHorses(); 
        }

        private void UpdateRaceTrackParameters()
        {
            centerX = RaceTrack.ActualWidth / 2;
            centerY = RaceTrack.ActualHeight / 2 - 40;
            radius = Math.Min(centerX, centerY) - Horse1.ActualWidth / 2;
        }

        private void SetInitialPositions()
        {
            Horse1.InitialPosition = new Point(centerX, centerY - radius);
            Horse2.InitialPosition = new Point(centerX, centerY - radius - 50);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Horse1.Move(centerX, centerY, radius);
            Horse2.Move(centerX, centerY, radius);
            UpdateHorsePositions();
        }

        private void UpdateHorsePositions()
        {
            Canvas.SetLeft(Horse1, Horse1.Position.X - Horse1.ActualWidth / 2);
            Canvas.SetTop(Horse1, Horse1.Position.Y - Horse1.ActualHeight / 2);
            Canvas.SetLeft(Horse2, Horse2.Position.X - Horse2.ActualWidth / 2);
            Canvas.SetTop(Horse2, Horse2.Position.Y - Horse2.ActualHeight / 2);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            ResetHorses();
        }

        private void ResetHorses()
        {
            Horse1.Reset();
            Horse2.Reset();
            UpdateHorsePositions();
        }

        private void Horse_SpeedClicked(object sender, RoutedEventArgs e)
        {
            var horse = sender as HorseMovement;
            InfoTextBlock.Text = $"Скорость: {horse.Speed}";
        }

        private void Horse_PositionClicked(object sender, RoutedEventArgs e)
        {
            var horse = sender as HorseMovement;
            InfoTextBlock.Text = $"Позиция: {horse.Position.X:F2}, {horse.Position.Y:F2}";
        }
    }
}