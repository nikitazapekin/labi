using System;
using System.Collections.Generic;
using System.Data;
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

namespace lab3zadanie2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = int.Parse(((TextBox)this.FindName("matrixRows")).Text);
                int columns = int.Parse(((TextBox)this.FindName("matrixColumns")).Text);

                DataTable table = new DataTable();

                for (int i = 0; i < columns; i++)
                {
                    table.Columns.Add("Col " + (i + 1));
                }

                Random random = new Random();

                for (int i = 0; i < rows; i++)
                {
                    DataRow newRow = table.NewRow();
                    for (int j = 0; j < columns; j++)
                    {
                        newRow[j] = random.Next(0, 1);
                    }
                    table.Rows.Add(newRow);
                }

                matrixGrid.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void InputButton_Click(object sender, RoutedEventArgs e)
        {

         
        }

       
        private void matrixRows_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Логика, связанная с изменением текста, если потребуется
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (matrixGrid.CurrentCell != null)
            {
                var currentCell = matrixGrid.CurrentCell;
                var dataRowView = currentCell.Item as DataRowView;

                if (dataRowView != null)
                {
                    matrixGrid.BeginEdit();
                }
            }
        }

        private void matrixGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editingElement = e.EditingElement as TextBox;
                if (editingElement != null)
                {
                    if (!int.TryParse(editingElement.Text, out int value))
                    {
                        MessageBox.Show("Пожалуйста, введите корректное целое число.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                        editingElement.Text = "0"; // Устанавливаем значение ячейки в 0
                        e.Cancel = true; // Отменяем изменение ячейки
                        return;
                    }
                    // Если число корректное, значение будет применено к DataGrid
                }
            }
        }


        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DataView dataView = matrixGrid.ItemsSource as DataView;

            if (dataView != null)
            {
                int rowCount = dataView.Count;
                int columnCount = dataView.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];

                // Заполнение массива из DataGrid
                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = dataView[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }

                // Удаление строк и столбцов, заполненных нулями
                List<int> nonZeroRows = new List<int>();
                List<int> nonZeroCols = new List<int>();

                for (int i = 0; i < rowCount; i++)
                {
                    bool hasNonZero = false;
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (matrix[i, j] != 0)
                        {
                            hasNonZero = true;
                            break;
                        }
                    }
                    if (hasNonZero) nonZeroRows.Add(i);
                }

                for (int j = 0; j < columnCount; j++)
                {
                    bool hasNonZero = false;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (matrix[i, j] != 0)
                        {
                            hasNonZero = true;
                            break;
                        }
                    }
                    if (hasNonZero) nonZeroCols.Add(j);
                }

                // Формирование новой матрицы без строк и столбцов, заполненных нулями
                int[,] compressedMatrix = new int[nonZeroRows.Count, nonZeroCols.Count];
                for (int i = 0; i < nonZeroRows.Count; i++)
                {
                    for (int j = 0; j < nonZeroCols.Count; j++)
                    {
                        compressedMatrix[i, j] = matrix[nonZeroRows[i], nonZeroCols[j]];
                    }
                }

                // Отображение уплотненной матрицы в втором DataGrid
                DataTable compressedTable = new DataTable();
                for (int j = 0; j < nonZeroCols.Count; j++)
                {
                    compressedTable.Columns.Add("Col " + (nonZeroCols[j] + 1));
                }

                for (int i = 0; i < nonZeroRows.Count; i++)
                {
                    DataRow newRow = compressedTable.NewRow();
                    for (int j = 0; j < nonZeroCols.Count; j++)
                    {
                        newRow[j] = compressedMatrix[i, j];
                    }
                    compressedTable.Rows.Add(newRow);
                }

                compressedMatrixGrid.ItemsSource = compressedTable.DefaultView;


                int firstPositiveRow = FindFirstRowWithPositiveElement(matrix, rowCount, columnCount);

                // Если строка найдена, выводим ее номер в модальном окне
                if (firstPositiveRow != -1)
                {
                    MessageBox.Show($"Первая строка с положительным элементом: {firstPositiveRow + 1}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Не найдено строк с положительными элементами.", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
            else
            {
                MessageBox.Show("Матрица пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /*


        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DataView dataView = matrixGrid.ItemsSource as DataView;

            if (dataView != null)
            {
                int rowCount = dataView.Count;
                int columnCount = dataView.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];

                // Заполнение матрицы
                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = dataView[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }

                // Поиск первой строки с хотя бы одним положительным элементом
                int firstPositiveRow = FindFirstRowWithPositiveElement(matrix, rowCount, columnCount);

                // Если строка найдена, выводим ее номер в модальном окне
                if (firstPositiveRow != -1)
                {
                    MessageBox.Show($"Первая строка с положительным элементом: {firstPositiveRow + 1}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Не найдено строк с положительными элементами.", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Матрица пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        */

        // Метод для поиска первой строки с положительным элементом
        private int FindFirstRowWithPositiveElement(int[,] matrix, int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        return i; // Возвращаем номер строки (индекс)
                    }
                }
            }
            return -1; // Если строк с положительными элементами нет, возвращаем -1
        }


    }
}
