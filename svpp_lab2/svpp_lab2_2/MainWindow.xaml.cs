using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static svpp_lab2_2.MainWindow;

namespace svpp_lab2_2
{

    public class Person : INotifyPropertyChanged
    {
        private string name;
        private double salary;
        private string occup;
        private string city;
        private string street;
        private int build;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException("Данное поле не может быть пустым!");
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public double Salary
        {
            get { return salary; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Заработная плата не может быть меньше или равна нулю!");
                else
                {
                    salary = value;
                    OnPropertyChanged("Salary");
                }
            }
        }

        public string Occup
        {
            get { return occup; }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException("Данное поле не может быть пустым!");
                else
                {
                    occup = value;
                    OnPropertyChanged("Occup");
                }
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException("Данное поле не может быть пустым!");
                else
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException("Данное поле не может быть пустым!");
                else
                {
                    street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        public int Build
        {
            get { return build; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Номер дома не может быть отрицательным или равен нулю!");
                else
                {
                    build = value;
                    OnPropertyChanged("Build");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public partial class MainWindow : Window
    {

        Person person;
        ObservableCollection<string> people;

        public MainWindow()
        {
            InitializeComponent();

            person = new Person();
            myGrid.DataContext = person;

            people = new ObservableCollection<string>();

            ResultBox.DataContext = people;

        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            string perName = person.Name, perOccup = person.Occup, perCity = person.City, perStreet = person.Street;
            double perSal = person.Salary;
            int perBuild = person.Build;

            if (string.IsNullOrWhiteSpace(perName))
                throw new Exception("Поле не может быть пустым!");
            if (string.IsNullOrWhiteSpace(perOccup))
                throw new Exception("Поле не может быть пустым!");
            if (string.IsNullOrWhiteSpace(perCity))
                throw new Exception("Поле не может быть пустым!");
            if (string.IsNullOrWhiteSpace(perStreet))
                throw new Exception("Поле не может быть пустым!");
            else
                people.Add("ФИО: " + perName + "   Заработная плата: " + perSal + "\nДолжность: " + perOccup + "   Город: " + perCity + "   Улица: " + perStreet + "   Дом: " + perBuild + "\n");

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Button clearButton = (Button)sender;
            tbName.Clear();
            tbSalary.Clear();
            cbOccup.Text = string.Empty;
            cbCity.Text = string.Empty;
            cbStreet.Text = string.Empty;
            tbBuild.Clear(); ;
            //results.Clear();
        }

        //private void Clear_List_Click(object sender, RoutedEventArgs e)
        //{
        //    Button clearButton = (Button)sender;
        //    ResultBox.Items.Clear();
        //}

        private void Add_Occup_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                cbOccup.Items.Add(cbOccup.Text);

                if (string.IsNullOrEmpty(cbOccup.Text))
                {
                    throw new Exception("Добавляемая строка не может быть пустой!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Add_City_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                cbCity.Items.Add(cbCity.Text);

                if (string.IsNullOrEmpty(cbCity.Text))
                {
                    throw new Exception("Добавляемая строка не может быть пустой!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Add_Street_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                cbStreet.Items.Add(cbStreet.Text);

                if (string.IsNullOrEmpty(cbStreet.Text))
                {
                    throw new Exception("Добавляемая строка не может быть пустой!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Save_to_file(object sender, RoutedEventArgs e)
        {
            using (StreamWriter myStreamWrite = new StreamWriter("Workers.txt", true)) //true - добавить новую инфу, false - перезаписать файл
            for (int i = 0; i<people.Count; i++)
            {
                    myStreamWrite.Write(people[i].ToString());
            }

        }

        private void Download_from_file(object sender, RoutedEventArgs e)
        {
            string[] lines = File.ReadAllLines("Workers.txt"); //читаю весь файл целиком в массив
            foreach (string str_for_reading in lines) { 
                //сюда запишется прочитанная строка

                    people.Add(str_for_reading);
            
                // что-нибудь делаем с прочитанной строкой s
            }

            //string str_for_reading; //сюда запишется прочитанная строка
            //using (var myStreamRead = new StreamReader("Workers.txt"))
            //{
            //    while ((str_for_reading = myStreamRead.ReadLine()) != null)
            //    {
            //       str_for_reading = myStreamRead.ReadLine();   //сюда запишется прочитанная строка

            //        people.Add(str_for_reading);

            //    }
            //}

        }
    }
}
