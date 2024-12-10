using System;
using System.Windows;
using System.Windows.Controls;

namespace lab6
{
    public partial class MainWindow : Window
    {
        private CustomSet set1 = new CustomSet();
        private CustomSet set2 = new CustomSet();

        public MainWindow()
        {
            InitializeComponent();
            UpdateListBoxes();
        }

        private void AddDefaultElements_Click(object sender, RoutedEventArgs e)
        {
            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set2.Add(2);
            set2.Add(3);
            set2.Add(4);

            UpdateListBoxes();
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NewElementTextBox.Text, out int newElement))
            {
                var selectedSet = ((ComboBoxItem)SetSelector.SelectedItem)?.Content.ToString();

                if (selectedSet == "Множество 1")
                {
                    set1.Add(newElement);
                }
                else if (selectedSet == "Множество 2")
                {
                    set2.Add(newElement);
                }

                UpdateListBoxes();
                NewElementTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректное число.");
            }
        }
        private void EditElement_Click(object sender, RoutedEventArgs e)
        {
            string indexText = ElementIndexTextBox.Text;
            string newValueText = NewValueTextBox.Text;

            if (int.TryParse(indexText, out int index) && int.TryParse(newValueText, out int newValue))
            {
                if (SetSelector.SelectedIndex == 0)
                {
                    set1.Update(index, newValue);
                }
                else
                {
                    set2.Update(index, newValue);
                }
                UpdateListBoxes();
                ElementIndexTextBox.Clear();
                NewValueTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректные индекс и новое значение.");
            }
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
            UpdateListBoxes();
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

        private void UpdateListBoxes()
        {
            Set1ListBox.ItemsSource = set1.ToArray();
            Set2ListBox.ItemsSource = set2.ToArray();
        }
    }
}
