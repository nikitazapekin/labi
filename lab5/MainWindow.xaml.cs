using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace lab5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string text = File.ReadAllText(openFileDialog.FileName);

                // Используем два метода для извлечения цитат
                string quotesUsingString = ExtractSentencesUsingString(text);
                string quotesUsingStringBuilder = ExtractSentencesUsingStringBuilder(text);

                // Отображаем результаты в TextBox
                txtQuotes.Text = "Предложения (String):\n" + quotesUsingString + "\n\n" +
                                 "Предложения (StringBuilder):\n" + quotesUsingStringBuilder;
            }
            else
            {
                MessageBox.Show("Ошибка чтения файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ExtractSentencesUsingString(string text)
        {
            string result = "";

            string[] sentences = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sentence in sentences)
            {
                string trimmedSentence = sentence.TrimStart();
                if (trimmedSentence.StartsWith("-"))
                {
                
                    result += trimmedSentence + Environment.NewLine;
                }
            }

            return result;
        }


        private string ExtractSentencesUsingStringBuilder(string text)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder currentSentence = new StringBuilder();
            bool isSentenceValid = false;

            foreach (char c in text)
            {
                if (c == '.')
                {
                    // Проверяем, начинается ли предложение с '-'
                    if (isSentenceValid)
                    {
                        result.AppendLine(currentSentence.ToString().Trim());
                    }
                    currentSentence.Clear(); // Очищаем для нового предложения
                    isSentenceValid = false; // Сбрасываем флаг
                }
                else
                {
                    if (!isSentenceValid && c == '-')
                    {
                        isSentenceValid = true; // Устанавливаем флаг, что предложение начинается с '-'
                    }
                    currentSentence.Append(c); // Добавляем символ в предложение
                }
            }

            // Проверяем последнее предложение (если текст не заканчивается точкой)
            if (isSentenceValid)
            {
                result.AppendLine(currentSentence.ToString().Trim());
            }

            return result.ToString();
        }

        /*
        private string ExtractSentencesUsingStringBuilder(string text)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder currentSentence = new StringBuilder();

            foreach (char c in text)
            {
                if (c == '.')
                {
                 
                    if (currentSentence.Length > 0 && currentSentence[0] == '-')
                    {
                        result.AppendLine(currentSentence.ToString().Trim());
                    }
                    currentSentence.Clear();
                }
                else
                {
                    currentSentence.Append(c);
                }
            }
 
            if (currentSentence.Length > 0 && currentSentence[0] == '-')
            {
                result.AppendLine(currentSentence.ToString().Trim());
            }

            return result.ToString();
        }
        */

    }
}


/*
 * using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace lab5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileContent = File.ReadAllText(openFileDialog.FileName);

                string filteredSentences = GetSentencesStartingWithDash(fileContent);

                txtOutput.Text = "Предложения, начинающиеся с тире:\n" + filteredSentences;
            }
            else
            {
                MessageBox.Show("Ошибка чтения файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetSentencesStartingWithDash(string text)
        {
            StringBuilder result = new StringBuilder();
            string[] sentences = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sentence in sentences)
            {
                string trimmedSentence = sentence.TrimStart();
                if (trimmedSentence.StartsWith("-"))
                {
                    result.AppendLine(trimmedSentence);
                }
            }

            return result.ToString();
        }
    }
} */
