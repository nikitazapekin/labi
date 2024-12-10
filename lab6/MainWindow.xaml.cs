using System;
using System.Windows;
using System.Linq;

namespace lab6
{
    public partial class MainWindow : Window
    {
        private CustomSet set1 = new CustomSet();
        private CustomSet set2 = new CustomSet();
        private int[] set1Elements = Array.Empty<int>();
        private int[] set2Elements = Array.Empty<int>();

        public MainWindow()
        {
            InitializeComponent();
            UpdateDataGrids();
        }

        private void AddDefaultElements_Click(object sender, RoutedEventArgs e)
        {
            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set2.Add(2);
            set2.Add(3);
            set2.Add(4);

            UpdateDataGrids();
        }

        private void UnionSets_Click(object sender, RoutedEventArgs e)
        {
            var unionSet = set1.Union(set2);
            MessageBox.Show($"Объединение множеств: {unionSet}");
        }

        private void IntersectSets_Click(object sender, RoutedEventArgs e)
        {
            var intersectionSet = set1.Intersect(set2);
            MessageBox.Show($"Пересечение множеств: {intersectionSet}");
        }

        private void DifferenceSets_Click(object sender, RoutedEventArgs e)
        {
            var differenceSet = set1.Difference(set2);
            MessageBox.Show($"Разность множеств: {differenceSet}");
        }

        private void SortSet_Click(object sender, RoutedEventArgs e)
        {
            set1.Sort();
            UpdateDataGrids();
            MessageBox.Show($"Отсортированное множество 1: {set1}");
        }

        private void CompareElements_Click(object sender, RoutedEventArgs e)
        {
            if (set1.Count > 0 && set2.Count > 0)
            {
                var comparisonResult = set1[0].CompareTo(set2[0]);
                string comparisonMessage = comparisonResult switch
                {
                    0 => "Элементы равны.",
                    > 0 => "Первый элемент множества 1 больше первого элемента множества 2.",
                    _ => "Первый элемент множества 1 меньше первого элемента множества 2."
                };
                MessageBox.Show(comparisonMessage);
            }
            else
            {
                MessageBox.Show("Оба множества должны содержать элементы.");
            }
        }

        private void UpdateDataGrids()
        {
            set1Elements = set1.ToArray();
            set2Elements = set2.ToArray();

            Set1DataGrid.ItemsSource = null;
            Set2DataGrid.ItemsSource = null;

            Set1DataGrid.ItemsSource = set1Elements;
            Set2DataGrid.ItemsSource = set2Elements;
        }
    }
}
