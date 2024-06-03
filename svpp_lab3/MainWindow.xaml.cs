using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using MessageBox = System.Windows.Forms.MessageBox;
using Path = System.IO.Path;
using Point = System.Windows.Point;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using static System.Net.WebRequestMethods;
using System.Windows.Ink;
using svpp_lab3;



namespace svpp_lab3
{

    public partial class MainWindow : Window
    {
        private Color bck_color = Colors.Turquoise;
        private Color ln_color = Colors.Black;
        public double ln_thickness = 0.5;

        private List<Shape> figures = new List<Shape>();
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainViewModel();
            this.DataContext = viewModel;

            CommandBinding save_binding = new CommandBinding(ApplicationCommands.Save);
            save_binding.Executed += Save_Executed;
            save_binding.CanExecute += Save_CanExecute;
            this.CommandBindings.Add(save_binding);

            CommandBinding open_binding = new CommandBinding(ApplicationCommands.Open);
            open_binding.Executed += Open_Executed;
            //open_binding.CanExecute += Open_CanExecute;
            this.CommandBindings.Add(open_binding);

        }

        private void DrawHexagon(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(DrawingCanvas);

            status.Text = $" Координаты фигуры:     x: {position.X},   y: {position.Y}";

            if (position.Y > 38) //чтобы не залазило на ToolBar
            {
                double side = 50;
                double height = side * Math.Sqrt(3) / 2;

                Polygon polygon = new Polygon
                {
                    Points = new PointCollection
                    {
                        new Point(position.X - side, position.Y - height / 2),
                        new Point(position.X - side / 2, position.Y - height),
                        new Point(position.X + side / 2, position.Y - height),
                        new Point(position.X + side, position.Y - height / 2),
                        new Point(position.X + side / 2, position.Y + height / 16),
                        new Point(position.X - side / 2, position.Y + height / 16)
                    },
                    Stroke = new SolidColorBrush(ln_color),
                    StrokeThickness = viewModel.LineThickness, // Используем толщину линии из ViewModel
                    Fill = new SolidColorBrush(bck_color)
                };

                //polygon.Stroke = new SolidColorBrush(ln_color);
                //polygon.StrokeThickness = slider_thickness.Value;
                //polygon.StrokeThickness = ln_thickness;
                //polygon.Fill = new SolidColorBrush(bck_color);


                DrawingCanvas.Children.Add(polygon);
                figures.Add(polygon);
            }
        }

        //private void thick_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    foreach (var child in DrawingCanvas.Children)
        //    {
        //        if (child is Polygon)
        //        {
        //            ((Polygon)child).StrokeThickness = slider_thickness.Value;
        //        }
        //    }
        //}

        private void chng_clr_back_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bck_color = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                SolidColorBrush brush = new SolidColorBrush(bck_color);
            }

        }

        private void chng_clr_border_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ln_color = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                SolidColorBrush brush = new SolidColorBrush(ln_color);
            }
        }

        private void ln_thick_menuItem_Click(object sender, RoutedEventArgs e)
        {
            ThicknessDialog thicknessDialog = new ThicknessDialog();
            if (thicknessDialog.ShowDialog() == true)
            {
                ln_thickness = thicknessDialog.Thickness;
            }
        }

        private void about_menuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Название программы: Графический редактор\nВерсия: 1.0\nАвтор: Кучинская С.А.");
        }

        private Color StringToColor(string colorString)
        {
            var colorParts = colorString.Split(',');
            byte a = byte.Parse(colorParts[0], CultureInfo.InvariantCulture);
            byte r = byte.Parse(colorParts[1], CultureInfo.InvariantCulture);
            byte g = byte.Parse(colorParts[2], CultureInfo.InvariantCulture);
            byte b = byte.Parse(colorParts[3], CultureInfo.InvariantCulture);
            return Color.FromArgb(a, r, g, b);
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DrawingCanvas.Children.Count != 0;
        }


        private void save_to_file(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var figure in figures)
                {
                    if (figure is Polygon polygon)
                    {
                        writer.WriteLine("Информация о фигуре (фигурах)");
                        writer.WriteLine(polygon.StrokeThickness);
                        writer.WriteLine($"{((SolidColorBrush)polygon.Stroke).Color.A},{((SolidColorBrush)polygon.Stroke).Color.R},{((SolidColorBrush)polygon.Stroke).Color.G},{((SolidColorBrush)polygon.Stroke).Color.B}");
                        writer.WriteLine($"{((SolidColorBrush)polygon.Fill).Color.A},{((SolidColorBrush)polygon.Fill).Color.R},{((SolidColorBrush)polygon.Fill).Color.G},{((SolidColorBrush)polygon.Fill).Color.B}");

                        foreach (var point in polygon.Points)
                        {
                            writer.WriteLine($"{point.X},{point.Y}");
                        }

                        writer.WriteLine("Конец информации");
                    }
                }
            }
        }
        private void Save_Executed(object sender, RoutedEventArgs e)
        {
            if (figures.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files|*.txt"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    save_to_file(saveFileDialog.FileName);
                    this.Title = $"Графический редактор - {Path.GetFileName(saveFileDialog.FileName)}";
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Нет фигур для сохранения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = DrawingCanvas.Children.Count != 0;
        //}

        private void load_from_file(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "Информация о фигуре (фигурах)")
                    {
                        Polygon polygon = new Polygon
                        {
                            StrokeThickness = double.Parse(reader.ReadLine(), CultureInfo.InvariantCulture),
                            Stroke = new SolidColorBrush(StringToColor(reader.ReadLine())),
                            Fill = new SolidColorBrush(StringToColor(reader.ReadLine()))
                        };

                        var points = new PointCollection();
                        while ((line = reader.ReadLine()) != "Конец информации")
                        {
                            var pointParts = line.Split(',');
                            double x = double.Parse(pointParts[0], CultureInfo.InvariantCulture);
                            double y = double.Parse(pointParts[1], CultureInfo.InvariantCulture);
                            points.Add(new Point(x, y));
                        }

                        polygon.Points = points;
                        DrawingCanvas.Children.Add(polygon);
                        figures.Add(polygon);
                    }
                }
            }
        }

        private void Open_Executed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                load_from_file(openFileDialog.FileName);
                this.Title = $"Графический редактор - {Path.GetFileName(openFileDialog.FileName)}";
            }
        }
    }
}
