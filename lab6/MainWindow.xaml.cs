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
        /*
          private void DeleteElement_Click(object sender, RoutedEventArgs e)
          {
              if (int.TryParse(DeleteTextBox.Text, out int newElement))
              {
                  var selectedSet = ((ComboBoxItem)SetSelector.SelectedItem)?.Content.ToString();

                  if (selectedSet == "Множество 1")
                  {
                      set1.Remove(newElement);
                  }
                  else if (selectedSet == "Множество 2")
                  {
                      set2.Remove(newElement);
                  }

                  UpdateListBoxes();
                  DeleteTextBox.Clear();
              }
              else
              {
                  MessageBox.Show("Введите корректное число.");
              }
          }

          */

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DeleteTextBox.Text, out int index))
            {
                var selectedSet = ((ComboBoxItem)SetSelector.SelectedItem)?.Content.ToString();

                if (selectedSet == "Множество 1")
                {
                    set1.RemoveAt(index);   
                }
                else if (selectedSet == "Множество 2")
                {
                    set2.RemoveAt(index);   
                }

                UpdateListBoxes();
                DeleteTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Введите корректный индекс.");
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
            set2.Sort();
            UpdateListBoxes();
            MessageBox.Show($"Отсортированное множество 1: {set1}");
        }



        void CompareElements_Click(object sender, RoutedEventArgs e)
        {
           
            int[] set1Array = set1.ToArray();
            int[] set2Array = set2.ToArray();

            if (set1Array.Length > 0 && set2Array.Length > 0)
            {
             
                for (int i = 0; i < set1Array.Length; i++)
                {
                    if (i < set1Array.Length - 1)
                    {
                        int elem1 = set1Array[i];
                        int elem2 = set1Array[i + 1];

                        if (elem1 > elem2)
                        {
                            MessageBox.Show($"Элемент {elem1} больше {elem2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else if (elem1 == elem2)
                        {
                            MessageBox.Show($"Элемент {elem1} равен {elem2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Элемент {elem1} меньше {elem2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }

              
                int lastElement1 = set1Array[set1Array.Length - 1];
                int firstElement1 = set1Array[0];
                if (lastElement1 > firstElement1)
                {
                    MessageBox.Show($"Элемент {lastElement1} больше {firstElement1}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (lastElement1 == firstElement1)
                {
                    MessageBox.Show($"Элемент {lastElement1} равен {firstElement1}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Элемент {lastElement1} меньше {firstElement1}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                }

             
                for (int i = 0; i < set2Array.Length; i++)
                {
                    if (i < set2Array.Length - 1)
                    {
                        int elem1 = set2Array[i];
                        int elem2 = set2Array[i + 1];

                        if (elem1 > elem2)
                        {
                            MessageBox.Show($"Элемент {elem1} больше {elem2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else if (elem1 == elem2)
                        {
                            MessageBox.Show($"Элемент {elem1} равен {elem2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Элемент {elem1} меньше {elem2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }

               
                int lastElement2 = set2Array[set2Array.Length - 1];
                int firstElement2 = set2Array[0];
                if (lastElement2 > firstElement2)
                {
                    MessageBox.Show($"Элемент {lastElement2} больше {firstElement2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (lastElement2 == firstElement2)
                {
                    MessageBox.Show($"Элемент {lastElement2} равен {firstElement2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Элемент {lastElement2} меньше {firstElement2}.", "Результат сравнения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
