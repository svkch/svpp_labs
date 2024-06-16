using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace svpp_lab7
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        
        Flower7 oneFlower;
        public EditWindow()
        {
            InitializeComponent();
            DataContext = oneFlower;
        }
        public EditWindow(Flower7 oneFlower) : this()
        {
            this.oneFlower = oneFlower;
            editGrid.DataContext = oneFlower;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var flower = DataContext as Flower7;
            if (!string.IsNullOrWhiteSpace(oneFlower.Name) && !string.IsNullOrWhiteSpace(oneFlower.Country) &&
                !string.IsNullOrWhiteSpace(oneFlower.Color) && oneFlower.FlowerPrice > 0)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    
    }
}
