using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace lab11var12
{
    public partial class MainWindow : Window
    {
      

        public MainWindow()
        {
            InitializeComponent();
        
        }

        private async void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtQuotes.Text = "Чтение файла...";
                txtOutput.Text = "";

                string filePath = openFileDialog.FileName;
                string text = await File.ReadAllTextAsync(filePath);

               
                StartBackgroundTask();

                try
                {
                    await Task.Run(() =>
                    {
                        string quotesUsingString = ExtractSentencesUsingString(text);
                        string quotesUsingStringBuilder = ExtractSentencesUsingStringBuilder(text);

                        Dispatcher.Invoke(() =>
                        {
                         
                            txtQuotes.Text = $"Предложения (String):\n{quotesUsingString}\n\nПредложения (StringBuilder):\n{quotesUsingStringBuilder}";
                        });
                    });
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("Операция была отменена.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
                    if (isSentenceValid)
                    {
                        result.AppendLine(currentSentence.ToString().Trim());
                    }
                    currentSentence.Clear();
                    isSentenceValid = false;
                }
                else
                {
                    if (!isSentenceValid && c == '-')
                    {
                        isSentenceValid = true;
                    }
                    currentSentence.Append(c);
                }
            }

            if (isSentenceValid)
            {
                result.AppendLine(currentSentence.ToString().Trim());
            }

            return result.ToString();
        }

        private async void StartBackgroundTask()
        {
            await Task.Run(() => GenerateRandomNumbers());
        }

        private void GenerateRandomNumbers( )
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
               

                int randomNumber = random.Next(1, 10001);

                Dispatcher.Invoke(() =>
                {
                    txtOutput.AppendText($"Случайное число: {randomNumber}\n");
                });

                Thread.Sleep(500); 
            }
        }

      
    }
}
 