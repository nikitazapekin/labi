
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace lab7Var12
{
    public partial class MainWindow : Window
    {
       
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






        /*
        private void SearchNote_Click(object sender, RoutedEventArgs e)
        {

            NotesCommandListBox.Items.Clear();
            string searchText = SearchTextBox.Text;
            string searchCriteria = ((ComboBoxItem)SearchCriteriaComboBox.SelectedItem)?.Content.ToString();
            bool found = false;

            for (int i = 0; i < currentIndex; i++)
            {
                if (searchCriteria == "По дате")
                {
                    DateTime searchDate = DateTime.ParseExact(searchText, "yyyy/MM/dd", null);
                    DateTime noteDate = new DateTime(notes[i].BirthDate[2], notes[i].BirthDate[1], notes[i].BirthDate[0]);

                    if (searchDate == noteDate)
                    {
                        NotesCommandListBox.Items.Add($"Найдено: {notes[i].FullName}, телефон: {notes[i].PhoneNumber}, дата рождения: {notes[i].BirthDate[0]:D2}/{notes[i].BirthDate[1]:D2}/{notes[i].BirthDate[2]}");
                        found = true;
                        break;
                    }
                }
                else if ((searchCriteria == "По телефону" && notes[i].PhoneNumber == searchText) ||
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
        */



        private void SearchNote_Click(object sender, RoutedEventArgs e)
        {
            NotesCommandListBox.Items.Clear();
            string searchText = SearchTextBox.Text.Trim();
            string searchCriteria = ((ComboBoxItem)SearchCriteriaComboBox.SelectedItem)?.Content.ToString();
            bool found = false;

            if (string.IsNullOrEmpty(searchCriteria))
            {
                MessageBox.Show("Выберите критерий поиска.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            for (int i = 0; i < currentIndex; i++)
            {
                try
                {
                    if (searchCriteria == "По дате")
                    {
                        if (!DateTime.TryParseExact(searchText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime searchDate))
                        {
                            MessageBox.Show("Введите дату в формате DD/MM/YYYY.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        DateTime noteDate = new DateTime(notes[i].BirthDate[2], notes[i].BirthDate[1], notes[i].BirthDate[0]);
                        if (searchDate == noteDate)
                        {
                            NotesCommandListBox.Items.Add(notes[i].ToString());
                            found = true;
                        }
                    }
                    else if ((searchCriteria == "По телефону" && notes[i].PhoneNumber.Equals(searchText, StringComparison.OrdinalIgnoreCase)) ||
                             (searchCriteria == "По ФИО" && notes[i].FullName.Equals(searchText, StringComparison.OrdinalIgnoreCase)))
                    {
                        NotesCommandListBox.Items.Add(notes[i].ToString());
                        found = true;
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при обработке поиска.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if (!found)
            {
                MessageBox.Show("Запись не найдена.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SortNotes_Click(object sender, RoutedEventArgs e)
        {
            string sortCriteria = ((ComboBoxItem)SortComboBox.SelectedItem)?.Content.ToString();

            if (string.IsNullOrEmpty(sortCriteria))
            {
                MessageBox.Show("Выберите критерий сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            for (int i = 0; i < currentIndex - 1; i++)
            {
                for (int j = i + 1; j < currentIndex; j++)
                {
                    bool swap = false;

                    try
                    {
                        switch (sortCriteria)
                        {
                            case "По имени":
                                swap = string.Compare(notes[i].FullName, notes[j].FullName, StringComparison.OrdinalIgnoreCase) > 0;
                                break;
                            case "По дате":
                                DateTime date1 = new DateTime(notes[i].BirthDate[2], notes[i].BirthDate[1], notes[i].BirthDate[0]);
                                DateTime date2 = new DateTime(notes[j].BirthDate[2], notes[j].BirthDate[1], notes[j].BirthDate[0]);
                                swap = date1 > date2;
                                break;
                            case "По телефону":
                                swap = string.Compare(notes[i].PhoneNumber, notes[j].PhoneNumber, StringComparison.OrdinalIgnoreCase) > 0;
                                break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при сортировке записей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            NotesCommandListBox.Items.Clear();
            string fullNameToDelete = DeleteNameTextBox.Text;
            bool deleted = false;

            for (int i = 0; i < currentIndex; i++)
            {
                if (notes[i].FullName == fullNameToDelete)
                {
                  
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




        private void AddDefaultNotes_Click(object sender, RoutedEventArgs e)
        {
         
            string[] defaultNames = { "Иван Иванов", "Петр Петров", "Александр Сидоров", "Мария Кузнецова", "Анна Смирнова", "Елена Волкова" };
            string[] defaultPhones = { "111-111-1111", "222-222-2222", "333-333-3333", "444-444-4444", "555-555-5555", "666-666-6666" };
            string[] defaultDates = { "1990/01/01", "1985/05/10", "2000/12/25", "1980/03/15", "1995/07/22", "1992/09/11" };

            for (int i = 0; i < 6; i++)
            {
                string[] dateParts = defaultDates[i].Split('/');
                notes[currentIndex] = new NOTE
                {
                    FullName = defaultNames[i],
                    PhoneNumber = defaultPhones[i],
                    BirthDate = new int[] { int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]) }
                };
                currentIndex++;
            }

            UpdateNoteList();
        }




    }
}
