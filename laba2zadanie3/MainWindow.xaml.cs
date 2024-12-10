using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace laba2zadanie3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int countOfStrikes;  // Количество попаданий
        private int countOfAttempts; // Количество попыток
        private double x, y, r1, r2; // Координаты и радиусы

        /// <summary>
        /// Отображение итогов попыток
        /// </summary>
        private void DisplayOfResults()
        {
            MessageBox.Show($"Количество попыток: {countOfAttempts}\nКоличество попаданий: {countOfStrikes}");
        }

        /// <summary>
        /// Обработчик кнопки "Выстрел"
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Проверяем, заполнены ли все поля
            if (string.IsNullOrWhiteSpace(X.Text) || string.IsNullOrWhiteSpace(Y.Text) ||
                string.IsNullOrWhiteSpace(R1.Text) || string.IsNullOrWhiteSpace(R2.Text))
            {
                MessageBox.Show("Для выстрела нужно заполнить все поля");
                return;
            }

            try
            {
                // Проверяем, что не превышено количество попыток
                if (countOfAttempts < 10)
                {
                    countOfAttempts++;

                    // Пробуем преобразовать введенные значения
                    if (!double.TryParse(R1.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out r1) ||
                        !double.TryParse(R2.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out r2) ||
                        !double.TryParse(X.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out x) ||
                        !double.TryParse(Y.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out y))
                    {
                        MessageBox.Show("Пожалуйста, введите численные значения");
                        return;
                    }

                    // Проверка, что R1 > R2
                    if (r1 <= r2)
                    {
                        MessageBox.Show("Радиус R1 должен быть больше R2.");
                        return;
                    }

                    // Логика попадания
                    double distance = Math.Sqrt(x * x + y * y); // Расстояние до центра
                    bool isInOuterCircle = distance <= r1;      // Точка в пределах внешнего радиуса
                    bool isOutOfInnerCircle = distance > r2;    // Точка за пределами внутреннего радиуса
                    bool isInValidAngles = (x <= 0 && y >= 0) || (x <= 0 && y <= 0); // Допустимые углы (90°–180°, 270°–360°)

                    // Проверяем условия попадания
                    if (isInOuterCircle && isOutOfInnerCircle && !isInValidAngles)
                    {
                        MessageBox.Show("Попал!");
                        countOfStrikes++;
                    }
                    else
                    {
                        MessageBox.Show("Не попал!");
                    }

                    // Если все попытки использованы, показываем результаты
                    if (countOfAttempts == 10)
                    {
                        DisplayOfResults();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик изменения текста (для будущей функциональности)
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
