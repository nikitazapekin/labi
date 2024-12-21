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
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
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

                // Запуск фоновой задачи для генерации случайных чисел
                StartBackgroundTask();

                try
                {
                    await Task.Run(() =>
                    {
                        string quotesUsingString = ExtractSentencesUsingString(text);
                        string quotesUsingStringBuilder = ExtractSentencesUsingStringBuilder(text);

                        Dispatcher.Invoke(() =>
                        {
                            // Отображаем оба результата в одном TextBox
                            txtQuotes.Text = $"Предложения (String):\n{quotesUsingString}\n\nПредложения (StringBuilder):\n{quotesUsingStringBuilder}";
                        });
                    }, _cancellationTokenSource.Token);
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
            await Task.Run(() => GenerateRandomNumbers(_cancellationTokenSource.Token));
        }

        private void GenerateRandomNumbers(CancellationToken token)
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                int randomNumber = random.Next(1, 10001);

                Dispatcher.Invoke(() =>
                {
                    txtOutput.AppendText($"Случайное число: {randomNumber}\n");
                });

                Thread.Sleep(500); // Задержка для демонстрации
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}

/*using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace lab11var12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
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
                            txtQuotes.Text = "Предложения (String):\n" + quotesUsingString;
                            txtOutput.Text = "Предложения (StringBuilder):\n" + quotesUsingStringBuilder;
                        });
                    }, _cancellationTokenSource.Token);
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
            await Task.Run(() => GenerateRandomNumbers(_cancellationTokenSource.Token));
        }

        private void GenerateRandomNumbers(CancellationToken token)
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                int randomNumber = random.Next(1, 10001); 

                Dispatcher.Invoke(() =>
                {
                    txtOutput.AppendText($"Случайное число: {randomNumber}\n");
                });

                Thread.Sleep(500);  
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }


    }
}
*/