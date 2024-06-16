using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace svpp_lab7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class EntityContext : DbContext
        {
            public EntityContext(string TestDbConnection) : base(TestDbConnection)
            {
                Database.SetInitializer(new DataBaseInitializer());
            }
            public DbSet<Flower7> Flowers7 { get; set; }
        }

        class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
        {
            protected override void Seed(EntityContext context)
            {
                context.Flowers7.AddRange(new Flower7[] 
                { 
                    new Flower7 { Name="Rose Bordo", Country="Equador", Color="Dark red", FlowerPrice=3.98 },
                    new Flower7 { Name="Royal Tulip", Country="Netherlands", Color="White", FlowerPrice=4.2 },
                    new Flower7 { Name="Chrisantemum", Country="Austria", Color="Yellow", FlowerPrice=7.62 },
                });
                context.SaveChanges();
            }
        }


        EntityContext context;

        public MainWindow()
        {
            context = new EntityContext("TestDbConnection");
            InitializeComponent();
            InitFlower7List();

        }
        private void InitFlower7List()
        {
            context.Flowers7.Load();
            dGrid.DataContext = context.Flowers7.Local;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var flwr = new Flower7();
            EditWindow editWin = new EditWindow(flwr);
            var result = editWin.ShowDialog();
            if (result == true)
            {
                context.Flowers7.Add(flwr);
                context.SaveChanges();
                editWin.Close();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Flower7 flwr = dGrid.SelectedItem as Flower7;

            EditWindow editWin = new EditWindow(flwr);
            var result = editWin.ShowDialog();
            if (result == true)
            {
                context.SaveChanges();
                editWin.Close();
            }
            else
            {
                // вернуть начальное значение
                context.Entry(flwr).Reload();
                // перегрузить DataContext
                dGrid.DataContext = null;
                dGrid.DataContext = context.Flowers7.Local;
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Flower7 flwr = dGrid.SelectedItem as Flower7;
                context.Flowers7.Remove(flwr);
                context.SaveChanges();
            }
        }
        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}

