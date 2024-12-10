using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace lab3zadamie1
{
    public partial class MainWindow : Window
    {
        private double[] randomNumbers;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                randomNumbers = new double[n];
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = Math.Round(random.NextDouble() * 121 - 20, 3);  
                }

                FillDataGrid(randomNumbers, DataGridView);
                UpdateValues();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillDataGrid(double[] array, DataGrid dataGrid)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Число", typeof(double));
            foreach (var num in array)
            {
                dt.Rows.Add(Math.Round(num, 3));
            }
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            DataView dv = (DataView)DataGridView.ItemsSource;
            randomNumbers = new double[dv.Table.Rows.Count];
            for (int i = 0; i < dv.Table.Rows.Count; i++)
            {
                randomNumbers[i] = Math.Round(Convert.ToDouble(dv.Table.Rows[i]["Число"]), 3);
            }
            UpdateValues();
        }

        private void UpdateValues()
        {
            int maxModIndex = GetMaxModIndex();
            MaxModIndex.Text = maxModIndex.ToString();

            double sumAfterFirstPositive = GetSumAfterFirstPositive();
            SumAfterFirstPositive.Text = Math.Round(sumAfterFirstPositive, 3).ToString();

            double[] transformedArray = TransformArray();
            FillDataGrid(transformedArray, TransformedDataGridView);
        }

        private int GetMaxModIndex()
        {
            double maxMod = Math.Abs(randomNumbers[0]);
            int maxModIndex = 0;

            for (int i = 1; i < randomNumbers.Length; i++)
            {
                if (Math.Abs(randomNumbers[i]) > maxMod)
                {
                    maxMod = Math.Abs(randomNumbers[i]);
                    maxModIndex = i;
                }
            }

            return maxModIndex;
        }

        private double GetSumAfterFirstPositive()
        {
            double sum = 0;
            bool foundFirstPositive = false;

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                if (randomNumbers[i] > 0 && !foundFirstPositive)
                {
                    foundFirstPositive = true;
                }
                else if (foundFirstPositive)
                {
                    sum += Math.Abs(randomNumbers[i]);
                }
            }

            return sum;
        }

        private double[] TransformArray()
        {
            List<double> inRange = new List<double>();
            List<double> outRange = new List<double>();

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                int intPart = (int)Math.Floor(randomNumbers[i]);
                if (intPart >= 1 && intPart < 5)
                {
                    inRange.Add(randomNumbers[i]);
                }
                else
                {
                    outRange.Add(randomNumbers[i]);
                }
            }

            // Объединяем два списка
            List<double> transformedList = new List<double>();
            transformedList.AddRange(inRange);
            transformedList.AddRange(outRange);

            return transformedList.ToArray();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(MinRange.Text, out int min) && int.TryParse(MaxRange.Text, out int max))
            {
                // Проверим, что min не больше max
                if (min > max)
                {
                    MessageBox.Show("Минимальный промежуток не может быть больше максимального.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                List<double> filteredNumbers = new List<double>();

                // Проходим по массиву и фильтруем элементы по целой части
                foreach (var num in randomNumbers)
                {
                    int intPart = (int)Math.Floor(num);  // Берем целую часть числа

                    // Проверяем, попадает ли целая часть в указанный диапазон
                    if (intPart >= min && intPart <= max)
                    {
                        filteredNumbers.Add(num);  // Если да, добавляем число в список
                    }
                }

                // Заполняем DataGrid отфильтрованными значениями
                FillDataGrid(filteredNumbers.ToArray(), TransformedDataGridView);
            }
            else
            {
                MessageBox.Show("Введите корректные значения для промежутков.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
