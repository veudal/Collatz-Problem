using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace CollatzProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long currrentNum;
        List<long> data = new List<long>();
        KeyValuePair<int, long>[] valuePairs;
        readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CollatzProblem\\";

        public MainWindow()
        {
            InitializeComponent();
            Title = "Collatz-Problem";
            Directory.CreateDirectory(path);
        }

        private void CalculateValues(long startNum)
        {
            valuePairs = null;
            data = new List<long>();
            File.Delete(path + "data.txt");

            currrentNum = startNum;
            bool reached1 = false;
            while (reached1 == false)
            {
                if (currrentNum == 1)
                {
                    reached1 = true;
                }

                data.Add(currrentNum);
                if (currrentNum % 2 == 0)
                {
                    currrentNum /= 2;
                }
                else
                {
                    currrentNum = (currrentNum * 3) + 1;
                }

            }
            DrawChart();
        }
        private void DrawChart()
        {
            valuePairs = new KeyValuePair<int, long>[data.Count];
            int i = 0;
            foreach (var n in data)
            {
                valuePairs[i] = new KeyValuePair<int, long>(i, n);
                File.AppendAllText(path + "data.txt", n + Environment.NewLine);
                i++;
            }
            ((LineSeries)Chart.Series[0]).ItemsSource = valuePairs;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CalculateValues(Convert.ToInt64(StartValue.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RandomBtn_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int rndNum = random.Next(999999);
            StartValue.Text = Convert.ToString(rndNum);
            CalculateValues(rndNum);
        }
    }
}

