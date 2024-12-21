﻿
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace lab7Var12
{
    public partial class MainWindow : Window
    {
        // Описание структуры NOTE
        public struct NOTE
        {
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public int[] BirthDate { get; set; }

            public override string ToString()
            {
                return $"{FullName} - {PhoneNumber} - {BirthDate[0]:D2}/{BirthDate[1]:D2}/{BirthDate[2]}";
            }
        }

        private NOTE[] notes = new NOTE[10];
        private int currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateNoteList();
        }


        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex >= 10)
            {
                MessageBox.Show("Массив заполнен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string fullName = FullNameTextBox.Text;
                string phoneNumber = PhoneNumberTextBox.Text;
                string[] birthDateParts = BirthDateTextBox.Text.Split('/');

                if (birthDateParts.Length != 3)
                {
                    MessageBox.Show("Неверный формат даты! Используйте DD/MM/YYYY.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int day = int.Parse(birthDateParts[0]);
                int month = int.Parse(birthDateParts[1]);
                int year = int.Parse(birthDateParts[2]);

                notes[currentIndex] = new NOTE
                {
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    BirthDate = new int[] { day, month, year }
                };

                currentIndex++;

             //   SortNotesByBirthDate();
                UpdateNoteList();

                FullNameTextBox.Clear();
                PhoneNumberTextBox.Clear();
                BirthDateTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

 


        private void SortNotesByBirthDate()
        {
            for (int i = 0; i < currentIndex - 1; i++)
            {
                for (int j = i + 1; j < currentIndex; j++)
                {
                    var date1 = notes[i].BirthDate;
                    var date2 = notes[j].BirthDate;

                    if (CompareDates(date1, date2) > 0)
                    {
                        var temp = notes[i];
                        notes[i] = notes[j];
                        notes[j] = temp;
                    }
                }
            }
        }

        // Сравнение двух дат
        private int CompareDates(int[] date1, int[] date2)
        {
            if (date1[2] != date2[2])
                return date1[2].CompareTo(date2[2]);
            if (date1[1] != date2[1])
                return date1[1].CompareTo(date2[1]);
            return date1[0].CompareTo(date2[0]);
        }
 
        private void UpdateNoteList()
        {
            NotesListBox.Items.Clear();
            for (int i = 0; i < currentIndex; i++)
            {
                NotesListBox.Items.Add(notes[i].ToString());
            }
        }












        private void SearchNote_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            string searchCriteria = ((ComboBoxItem)SearchCriteriaComboBox.SelectedItem)?.Content.ToString();
            bool found = false;

            for (int i = 0; i < currentIndex; i++)
            {
                if ((searchCriteria == "По телефону" && notes[i].PhoneNumber == searchText) ||
                    (searchCriteria == "По ФИО" && notes[i].FullName == searchText))
                {
                    NotesCommandListBox.Items.Add($"Найдено: {notes[i].FullName}, телефон: {notes[i].PhoneNumber}, дата рождения: {notes[i].BirthDate[0]:D2}/{notes[i].BirthDate[1]:D2}/{notes[i].BirthDate[2]}");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Запись не найдена.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Сортировка записей
        private void SortNotes_Click(object sender, RoutedEventArgs e)
        {
            string sortCriteria = ((ComboBoxItem)SortComboBox.SelectedItem)?.Content.ToString();

            for (int i = 0; i < currentIndex - 1; i++)
            {
                for (int j = i + 1; j < currentIndex; j++)
                {
                    bool swap = false;

                    switch (sortCriteria)
                    {
                        case "По имени":
                            if (string.Compare(notes[i].FullName, notes[j].FullName) > 0)
                            {
                                swap = true;
                            }
                            break;
                        case "По дате":
                            DateTime date1 = new DateTime(notes[i].BirthDate[2], notes[i].BirthDate[1], notes[i].BirthDate[0]);
                            DateTime date2 = new DateTime(notes[j].BirthDate[2], notes[j].BirthDate[1], notes[j].BirthDate[0]);
                            if (date1 > date2)
                            {
                                swap = true;
                            }
                            break;
                        case "По телефону":
                            if (string.Compare(notes[i].PhoneNumber, notes[j].PhoneNumber) > 0)
                            {
                                swap = true;
                            }
                            break;
                    }

                    if (swap)
                    {
                        NOTE temp = notes[i];
                        notes[i] = notes[j];
                        notes[j] = temp;
                    }
                }
            }

            UpdateNoteList();
        }

        // Удаление записи по ФИО
        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            string fullNameToDelete = DeleteNameTextBox.Text;
            bool deleted = false;

            for (int i = 0; i < currentIndex; i++)
            {
                if (notes[i].FullName == fullNameToDelete)
                {
                    // Сдвигаем все элементы влево
                    for (int j = i; j < currentIndex - 1; j++)
                    {
                        notes[j] = notes[j + 1];
                    }

                    currentIndex--;
                    deleted = true;
                    break;
                }
            }

            if (deleted)
            {
                UpdateNoteList();
                NotesCommandListBox.Items.Add($"Запись с ФИО '{fullNameToDelete}' удалена.");
            }
            else
            {
                MessageBox.Show("Запись с таким ФИО не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }







        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "notes.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < currentIndex; i++)
                    {
                        var note = notes[i];
                        string line = $"{note.FullName}-{note.PhoneNumber}-{note.BirthDate[0]:D2}/{note.BirthDate[1]:D2}/{note.BirthDate[2]}";
                        writer.WriteLine(line);
                    }
                }

                MessageBox.Show($"Файл успешно сохранён по пути: {filePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "notes.txt");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл notes.txt не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    currentIndex = 0;
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('-');
                        if (parts.Length == 3)
                        {
                            string fullName = parts[0];
                            string phoneNumber = parts[1];
                            string[] dateParts = parts[2].Split('/');

                            if (dateParts.Length == 3 &&
                                int.TryParse(dateParts[0], out int day) &&
                                int.TryParse(dateParts[1], out int month) &&
                                int.TryParse(dateParts[2], out int year))
                            {
                                notes[currentIndex++] = new NOTE
                                {
                                    FullName = fullName,
                                    PhoneNumber = phoneNumber,
                                    BirthDate = new int[] { day, month, year }
                                };
                            }
                        }
                    }
                }

           //     SortNotesByBirthDate();
                UpdateNoteList();
                MessageBox.Show("Данные успешно загружены из файла.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
