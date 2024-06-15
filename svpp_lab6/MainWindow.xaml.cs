using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace svpp_lab6
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Flower> flowerList;

        public MainWindow()
        {
            InitializeComponent();
            flowerList = new ObservableCollection<Flower>();
            lBox.DataContext = flowerList;
        }

        private void FillData()
        {
            flowerList.Clear();
            try
            {
                foreach (Flower flower in Flower.GetAllFlowers())
                {
                    flowerList.Add(flower);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при заполнении данных: {ex.Message}");
            }
        }

      

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Flower flower = new Flower()
                {
                    Name = "Rose Bordo",
                    Country = "Ecuador",
                    Color = "dark burgundy",
                    FlowerPrice = 4.7
                };
                flower.Insert();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lBox.SelectedItem is Flower selectedFlwr)
                {
                    selectedFlwr.Name = "Royal Tulip";
                    selectedFlwr.Update();
                    FillData();
                }
                else
                {
                    MessageBox.Show("Выберите цветок для редактирования.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании записи: {ex.Message}");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lBox.SelectedItem is Flower selectedFlwr)
                {
                    Flower.Delete(selectedFlwr.FlowerID);
                    FillData();
                }
                else
                {
                    MessageBox.Show("Выберите цветок для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }
    }
}
