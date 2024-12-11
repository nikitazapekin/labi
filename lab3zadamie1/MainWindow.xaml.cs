using System;
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

                int maxModIndex = GetMaxModIndex();
                MaxModIndex.Text = maxModIndex.ToString();

                double sumAfterFirstPositive = GetSumAfterFirstPositive();
                SumAfterFirstPositive.Text = Math.Round(sumAfterFirstPositive, 3).ToString();


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
            //    UpdateValues();


            int maxModIndex = GetMaxModIndex();
            MaxModIndex.Text = maxModIndex.ToString();

            double sumAfterFirstPositive = GetSumAfterFirstPositive();
            SumAfterFirstPositive.Text = Math.Round(sumAfterFirstPositive, 3).ToString();

        }


        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MinRange.Text, out int min) && int.TryParse(MaxRange.Text, out int max))
            {
            
                if (min > max)
                {
                    MessageBox.Show("Минимальный промежуток не может быть больше максимального.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            
                int inRangeIndex = 0;
                int outOfRangeIndex = randomNumbers.Length - 1;
 
                while (inRangeIndex < outOfRangeIndex)
                {
                   
                    if (randomNumbers[inRangeIndex] >= min && randomNumbers[inRangeIndex] <= max)
                    {
                        inRangeIndex++;
                    }
                   
                    else if (randomNumbers[outOfRangeIndex] < min || randomNumbers[outOfRangeIndex] > max)
                    {
                        outOfRangeIndex--;
                    }
              
                    else
                    {
                        double temp = randomNumbers[inRangeIndex];
                        randomNumbers[inRangeIndex] = randomNumbers[outOfRangeIndex];
                        randomNumbers[outOfRangeIndex] = temp;

                        inRangeIndex++;
                        outOfRangeIndex--;
                    }
                }

               
                FillDataGrid(randomNumbers, TransformedDataGridView);
            }
            else
            {
                MessageBox.Show("Введите корректные значения для промежутков.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


/*
 * 
 * 
 * 
 * 
 private void UpdateValues()
        {
          
            int inRangeIndex = 0;
            int outOfRangeIndex = randomNumbers.Length - 1;

       
            while (inRangeIndex < outOfRangeIndex)
            {
            
                if (randomNumbers[inRangeIndex] >= 1 && randomNumbers[inRangeIndex] < 5)
                {
                    inRangeIndex++;
                }
             
                else if (randomNumbers[outOfRangeIndex] < 1 || randomNumbers[outOfRangeIndex] >= 5)
                {
                    outOfRangeIndex--;
                }
               
                else
                {
                    double temp = randomNumbers[inRangeIndex];
                    randomNumbers[inRangeIndex] = randomNumbers[outOfRangeIndex];
                    randomNumbers[outOfRangeIndex] = temp;

                    inRangeIndex++;
                    outOfRangeIndex--;
                }
            }

          
            FillDataGrid(randomNumbers, TransformedDataGridView);
        }

 * using System;
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

        // Кнопка генерации случайных чисел
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                randomNumbers = new double[n];
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = Math.Round(random.NextDouble() * 121 - 20, 3); // Генерация чисел от -20 до 100
                }

                // Отображаем все сгенерированные числа
                FillDataGrid(randomNumbers, DataGridView);
                UpdateValues();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для заполнения DataGrid
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

        // Метод для обновления значений и трансформации массива
        private void UpdateValues()
        {
            // Создаем массивы для чисел в и вне диапазона
            double[] inRange = new double[randomNumbers.Length];
            double[] outOfRange = new double[randomNumbers.Length];
            int inRangeCount = 0, outOfRangeCount = 0;

            // Разделяем числа на те, что в промежутке [1, 5), и те, что вне этого диапазона
            foreach (var num in randomNumbers)
            {
                if (num >= 1 && num < 5)
                {
                    inRange[inRangeCount++] = num;
                }
                else
                {
                    outOfRange[outOfRangeCount++] = num;
                }
            }

            // Создаем новый массив с числами, сначала из промежутка, потом вне его
            double[] result = new double[randomNumbers.Length];
            Array.Copy(inRange, result, inRangeCount);
            Array.Copy(outOfRange, result, outOfRangeCount);

            // Отображаем новый массив
            FillDataGrid(result, TransformedDataGridView);
        }

        // Кнопка фильтрации чисел в заданном диапазоне
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MinRange.Text, out int min) && int.TryParse(MaxRange.Text, out int max))
            {
                // Проверка, чтобы min не был больше max
                if (min > max)
                {
                    MessageBox.Show("Минимальный промежуток не может быть больше максимального.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем массивы для чисел в и вне диапазона
                double[] inRange = new double[randomNumbers.Length];
                double[] outOfRange = new double[randomNumbers.Length];
                int inRangeCount = 0, outOfRangeCount = 0;

                // Разделяем числа на те, что в промежутке [min, max], и те, что вне этого диапазона
                foreach (var num in randomNumbers)
                {
                    if (num >= min && num <= max)
                    {
                        inRange[inRangeCount++] = num;
                    }
                    else
                    {
                        outOfRange[outOfRangeCount++] = num;
                    }
                }

                // Создаем новый массив с числами, сначала из промежутка, потом вне его
                double[] result = new double[randomNumbers.Length];
                Array.Copy(inRange, result, inRangeCount);
                Array.Copy(outOfRange, result, outOfRangeCount);

                // Отображаем новый массив
                FillDataGrid(result, TransformedDataGridView);
            }
            else
            {
                MessageBox.Show("Введите корректные значения для промежутков.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
*/