using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace lb2zd1
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

        private void OnCalculateClick(object sender, RoutedEventArgs e)
        {
            // Парсинг входных значений
            if (!double.TryParse(XStartTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double xstart) ||
                !double.TryParse(XEndTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double xend) ||
                !double.TryParse(DxTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double dx))
            {
                MessageBox.Show("Please enter valid numbers.");
                return;
            }

            // Проверка на положительный шаг
            if (dx <= 0)
            {
                MessageBox.Show("Пожалуйста, введите положительное значение шага (dx).");
                return;
            }

            // Проверка, что начальное значение меньше конечного
            if (xstart >= xend)
            {
                MessageBox.Show("Пожалуйста, введите корректные промежутки.");
                return;
            }

            StringBuilder output = new StringBuilder();
            output.AppendLine("    x      f(x)");

            int amountOfElements = 0;

            // Основной цикл расчета значений функции
            for (double i = xstart; i <= xend; i += dx)
            {
                double f;
                if (i >= -6 && i <= -2) // 1. Прямая y = 0.25x + 0.5
                {
                    f = 0.25 * i + 0.5;
                    output.AppendLine($"{i,7:F2} {f,10:F5}");
                    amountOfElements++;
                }
                else if (i > -2 && i <= 0) // 2. Окружность радиус 2, центр (-2, 2)
                {
                    double ySquared = 4 - Math.Pow(i + 2, 2);
                    if (ySquared >= 0)
                    {
                        f = -Math.Sqrt(ySquared) + 2;
                        output.AppendLine($"{i,7:F2} {f,10:F5}");
                        amountOfElements++;
                    }
                    else
                    {
                        output.AppendLine($"{i,7:F2} Значение находится вне диапазона");
                    }
                }
                else if (i > 0 && i <= 2) // 3. Окружность радиус 2, центр (0, 0)
                {
                    double ySquared = 4 - Math.Pow(i, 2);
                    if (ySquared >= 0)
                    {
                        f = -Math.Sqrt(ySquared);
                        output.AppendLine($"{i,7:F2} {f,10:F5}");
                        amountOfElements++;
                    }
                    else
                    {
                        output.AppendLine($"{i,7:F2} Значение находится вне диапазона");
                    }
                }
                else if (i > 2 && i <= 3) // 4. Прямая y = -x + 2
                {
                    f = -i + 2;
                    output.AppendLine($"{i,7:F2} {f,10:F5}");
                    amountOfElements++;
                }
                else // Остальные точки
                {
                    output.AppendLine($"{i,7:F2} Значение находится вне диапазона");
                }
            }

            // Сообщение, если элементов нет
            if (amountOfElements == 0)
            {
                output.AppendLine("Элементы отсутствуют");
            }

            // Вывод результата
            OutputTextBlock.Text = output.ToString();
        }
    }
}


/*     else if (i >= -6 && i <= 2)
     {
         double f = (0.5) * i + 1;
         output.AppendLine($"{i,7:F2} {f,10:F5}");
         amountOfElements += 1;
     }
     else if (i >= 2 && i <= 6)
     {
         double f = Math.Pow(i - 4, 2) + 2;
         output.AppendLine($"{i,7:F2}  {0,10:F5}");
         amountOfElements += 1;
     }
     else if (i >= 6 && i <= 8)
     {
         double f = Math.Pow(i - 6, 2);
         output.AppendLine($"{i,7:F2} {f,10:F5}");
         amountOfElements += 1;
     }
*/