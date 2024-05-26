﻿using System;
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

namespace svpp_lab3
{
    /// <summary>
    /// Логика взаимодействия для ThicknessDialog.xaml
    /// </summary>

    public partial class ThicknessDialog : Window
    {
        public double Thickness { get;  set; }

        public ThicknessDialog()
        {
            InitializeComponent();
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ThicknessTextBox.Text, out double thickness) && thickness > 0)
            {
                Thickness = thickness;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите корректное значение толщины.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
    
