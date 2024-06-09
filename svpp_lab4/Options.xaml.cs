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
using System.Windows.Shapes;

namespace svpp_lab4
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public double MinLim { get; private set; }
        public double MaxLim { get; private set; }
        public int Parts { get; private set; }

        public Options()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(minLimTB.Text, out double min) &&
                double.TryParse(maxLimTB.Text, out double max) &&
                int.TryParse(partsTB.Text, out int parts))
            {
                MinLim = min;
                MaxLim = max;
                Parts = parts;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите корректные значения!");
            }
        }
    }
}
