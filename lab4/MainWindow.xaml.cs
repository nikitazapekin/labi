using System;
using System.Collections.Generic;
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

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Circle myCircle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateCircleDetails()
        {
            if (myCircle != null)
            {
                CircleDetailsTextBlock.Text = $"Радиус: {myCircle.Radius}\n" +
                                              $"Площадь: {myCircle.Area}\n" +
                                              $"Длина окружности: {myCircle.Circumference}\n";
            }
            else
            {
                CircleDetailsTextBlock.Text = "Круг не создан.";
            }
        }

        private void CreateCircleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double radius = double.Parse(RadiusTextBox.Text);

                if (radius <= 0)
                {
                    throw new ArgumentException("Радиус должен быть больше 0.");
                }

                myCircle = new Circle(radius);
                UpdateCircleDetails();
                MessageBox.Show("Круг создан!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }




        private void CheckPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (myCircle == null)
            {
                MessageBox.Show("Сначала создайте круг!");
                return;
            }

            try
            {
                double x = double.Parse(XPointTextBox.Text);
                double y = double.Parse(YPointTextBox.Text);

                bool isInside = myCircle.IsPointInside(x, y);
                string message = isInside ? "Точка внутри круга!" : "Точка вне круга!";
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    


}
    /*
    public class Circle
    {
        public double Radius { get; private set; }

        public double Area
        {
            get => Math.PI * Math.Pow(Radius, 2);
        }

        public double Circumference
        {
            get => 2 * Math.PI * Radius;
        }

        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException("Радиус должен быть больше 0.");
            }
            Radius = radius;
        }

        public bool IsPointInside(double x, double y)
        {
            // Проверка, попадает ли точка (x, y) внутри круга с центром в (0, 0)
            return (x * x + y * y) <= (Radius * Radius);
        } 
    }
    */
}
