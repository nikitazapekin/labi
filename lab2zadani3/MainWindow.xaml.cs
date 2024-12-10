using System;
using System.Windows;

namespace lab2zadani3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EvaluateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(TextBoxXMin.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double xmin) ||
                    !double.TryParse(TextBoxXMax.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double xmax) ||
                    !double.TryParse(TextBoxDx.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double dx) ||
                    !double.TryParse(TextBoxEpsilon.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double epsilon))
                {
                    MessageBox.Show("Некорректный ввод. Убедитесь, что все значения заданы числами с использованием точки.");
                    return;
                }

                if (xmax < xmin || dx <= 0 || epsilon <= 0)
                {
                    MessageBox.Show("Проверьте введенные данные: xmax >= xmin, dx > 0, epsilon > 0.");
                    return;
                }

                valuesList.Items.Clear();
                valuesList.Items.Add("Таблица результатов:");

                const int MaxIter = 500;

                for (double x = xmin; x <= xmax; x += dx)
                {
                    if (x >= -1)
                    {
                        valuesList.Items.Add($"x = {x:F4}, Значение функции не определено.");
                        continue;
                    }

                    double sum = -Math.PI / 2;
                    double term;
                    bool converged = false;

                    for (int n = 0; n < MaxIter; n++)
                    {
                        term = Math.Pow(-1, n + 1) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                        sum += term;

                        if (Math.Abs(term) < epsilon)
                        {
                            converged = true;
                            valuesList.Items.Add($"x = {x:F4}, Сумма ряда = {sum:F6}, Членов ряда = {n + 1}");
                            break;
                        }
                    }

                    if (!converged)
                    {
                        valuesList.Items.Add($"x = {x:F4}, Ряд не сошелся после {MaxIter} итераций.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
