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

namespace svpp_lab5
{
    /// <summary>
    /// Логика взаимодействия для HorseMovement.xaml
    /// </summary>
    public partial class HorseMovement : UserControl
    {
        private double _angle;

        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(HorseMovement), new PropertyMetadata(0.0));

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(HorseMovement), new PropertyMetadata(new Point(0, 0)));

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public Point InitialPosition { get; set; }

        public void Move(double centerX, double centerY, double radius)
        {
            _angle += Speed;
            if (_angle >= 360) _angle = 0;

            double radians = _angle * (Math.PI / 180);
            double newX = centerX + radius * Math.Cos(radians);
            double newY = centerY + radius * Math.Sin(radians);

            Position = new Point(newX, newY);
          
        }
        private static readonly Random RandomGenerator = new Random();
        public void Reset()
        {
            _angle = -90;
            Position = InitialPosition;
            Speed = RandomGenerator.Next(1, 20); 
        }

        public static readonly RoutedEvent SpeedClickedEvent =
            EventManager.RegisterRoutedEvent("SpeedClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HorseMovement));

        public event RoutedEventHandler SpeedClicked
        {
            add { AddHandler(SpeedClickedEvent, value); }
            remove { RemoveHandler(SpeedClickedEvent, value); }
        }

        public static readonly RoutedEvent PositionClickedEvent =
            EventManager.RegisterRoutedEvent("PositionClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HorseMovement));

        public event RoutedEventHandler PositionClicked
        {
            add { AddHandler(PositionClickedEvent, value); }
            remove { RemoveHandler(PositionClickedEvent, value); }
        }

        public HorseMovement()
        {
            InitializeComponent();
            _angle = -90; // Начальная позиция сверху
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            RaiseEvent(new RoutedEventArgs(SpeedClickedEvent));
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            RaiseEvent(new RoutedEventArgs(PositionClickedEvent));
        }
    }
}