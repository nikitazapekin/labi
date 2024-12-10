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
                    // Если предложение начинается с тире, добавляем его в результат
                    result += trimmedSentence + Environment.NewLine;
                }
            }

            return result;
        }

        private string ExtractSentencesUsingStringBuilder(string text)
        {
            StringBuilder result = new StringBuilder();
            string[] sentences = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sentence in sentences)
            {
                string trimmedSentence = sentence.TrimStart();
                if (trimmedSentence.StartsWith("-"))
                {
                    // Если предложение начинается с тире, добавляем его в StringBuilder
                    result.AppendLine(trimmedSentence);
                }
            }

            return result.ToString();
        }

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
