using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using static svpp_lab2.MainWindow;

namespace svpp_lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public class Calc
    {
        double x;
        double s;
        double y;
        bool flag;

        //public double X { get => x; set => x = value; }
        //public double S { get => s; set => s = value; }
        //public double Y { get => y; set => y = value; }
        //public bool Flag { get => flag; set => flag = value; }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double S
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public bool Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        public Calc() { }

        public override string ToString()
        {
            return $"x={x}, Y({x})={y}, S({x})={s}, r={flag}";
        }
    }

    public class Values : INotifyPropertyChanged
    {
        private double start;
        private double stop;
        private double step_val;
        private int n;


        public double xStart
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged("xStart");
            }
        }
        public double xStop
        {
            get { return stop; }
            set
            {
                stop = value;
                OnPropertyChanged("xStop");
            }
        }
        public double step
        {
            get { return step_val; }
            set
            {
                step_val = value;
                OnPropertyChanged("step");
            }
        }
        public int nMembers

        {
            get { return n; }
            set
            {
                if (value <= 5)
                    throw new ArgumentException("Значение должно быть больше 5");
                else
                    n = value;
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

        Values values;
        ObservableCollection<string> results;
        Calc obj = new Calc();

        public MainWindow()
        {
            InitializeComponent();
            obj = new Calc();

            values = new Values();
            myGrid.DataContext = values;

            results = new ObservableCollection<string>();
            ResultBox.DataContext = results;

        }


        private void Calculate(object sender, RoutedEventArgs e)
        {
            results.Clear();

            double a = values.xStart, b = values.xStop, step = values.step;
            int n = values.nMembers;

            for (double i = a; i <= b; i += step)
            {

                // double y = Math.Exp(i * Math.Cos(Math.PI / 4)) * Math.Cos(i * Math.Sin(Math.PI / 4));
                double y = Math.Cos(i);

                double s = 0;
                for (int k = 0; k <= n; k++)
                {
                    //s += (Math.Cos((k * Math.PI) / 4) / calc_factorial(k)) * Math.Pow(i, k);
                    s += (Math.Pow((-1), k)) * ((Math.Pow(i, 2 * k)) / calc_factorial(2 * k)); //вариант 7
                }

                obj.S = Math.Round(s, 3);
                obj.X = i;
                obj.Y = Math.Round(y, 3);
                // obj.Flag = obj.S == obj.Y ? true : false;

                if (obj.S == obj.Y)
                    obj.Flag = true;
                else
                    obj.Flag = false;

                results.Add(obj.ToString());
            }

        }

        //    for (double i = Convert.ToDouble(tbStart.Text);  i <= Convert.ToDouble(tbStop.Text); i += Convert.ToDouble(tbStep.Text))
        //    {
        //        // double y = (Math.Pow(Math.E, (i * (Math.Cos(Math.PI / 4))))) * Math.Cos (i * Math.Sin(Math.PI / 4));
        //        double y = (Math.Exp(i * (Math.Cos(Math.PI / 4)))) * Math.Cos(i * Math.Sin(Math.PI / 4));
        //        results.Add("if x= " + Convert.ToString(i));
        //        results.Add("Y(x)= " + Convert.ToString(y));

        //        //double s = 0;
        //        double p = 1;
        //        double s = 1;
        //        for (int k = 1; k < Convert.ToInt32(tbNMember.Text); k++)
        //        {
        //            //p += i / k;
        //            //s += p * (Math.Cos((k * Math.PI) / 4));
        //             p *= Math.Cos((k * Math.PI )/ 4) * i / k;
        //            s += p;
        //            // p *= (((Math.Pow((-1), k)) * (Math.Pow((Math.PI / 4), 2 * k)) * (Math.Pow(i, 2 * k))) / calc_factorial(2*k)) * i / k;


        //          // s += ((((Math.Cos((k * Math.PI) / 4)))) / (calc_factorial(k))) * Math.Pow(i, k);
        //            //s += ((((Math.Cos((k * Math.PI) / 4))))) / (((k-1) * k) * Math.Pow(i, k));
        //        }

        //        results.Add("S(x)= " + Convert.ToString(s));
        //    }

        //}

        private double calc_factorial(int num)
        {


            double fact = 1;
            for (int i = 1; i <= num; i++)
            {
                fact *= i;
            }
            return fact;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Button clearButton = (Button)sender;
            tbStart.Clear();
            tbStop.Clear();
            tbStep.Clear();
            tbNMember.Clear();
            results.Clear();
        }
    }
}
