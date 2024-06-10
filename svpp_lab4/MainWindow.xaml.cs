using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace svpp_lab4
 {
    public partial class MainWindow : Window
    {
        private double minLim;
        private double maxLim;
        private int parts;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalcDispatcher_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            if (options.ShowDialog() == true)
            {
                minLim = options.MinLim;
                maxLim = options.MaxLim;
                parts = options.Parts;

                CalcDispatcher.IsEnabled = false;
                CalcBgWorker.IsEnabled = false;

                Dispatcher.BeginInvoke(new Action(CalculateIntegral), DispatcherPriority.Background);
            }
        }

        private void CalcBgWorker_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            if (options.ShowDialog() == true)
            {
                minLim = options.MinLim;
                maxLim = options.MaxLim;
                parts = options.Parts;

                CalcDispatcher.IsEnabled = false;
                CalcBgWorker.IsEnabled = false;

                var worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
        }

        private void CalculateIntegral()
        {
            double result = 0.0;
            double h = (maxLim - minLim) / parts;

            for (int i = 0; i < parts; i++)
            {
                double x = minLim + i * h;
                result += 0.25 * Math.Pow(x, 4) * h;

                Dispatcher.Invoke(() =>
                {
                    progressBar.Value = (i + 1) * 100 / parts;
                });

                Thread.Sleep(100); //имитируем долгие вычисления

            }

            Dispatcher.Invoke(() =>
            {
                resultTB.Text = $"Result: {result}";
                CalcDispatcher.IsEnabled = true;
                CalcBgWorker.IsEnabled = true;
            });
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            double result = 0.0;
            double h = (maxLim - minLim) / parts;

            for (int i = 0; i < parts; i++)
            {
                double x = minLim + i * h;
                result += 0.25 * Math.Pow(x, 4) * h;

                (sender as BackgroundWorker).ReportProgress((i + 1) * 100 / parts);
                Thread.Sleep(100); //имитируем долгие вычисления
            }

            e.Result = result;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resultTB.Text = $"Result: {e.Result}";
            CalcDispatcher.IsEnabled = true;
            CalcBgWorker.IsEnabled = true;
        }
    }
}