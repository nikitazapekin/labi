using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab10Var12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {





        private double scale = 100;
        private Brush lineColor = Brushes.Orange;
        private double lineWidth = 4;
        private Ellipse currentScalingPoint = null;
        private Point previousMousePosition;
        private double offsetX = 0, offsetY = 0;
        private double greenPointX, greenPointY;



        public delegate void GraphBuiltEventHandler(object sender, EventArgs e);
        public event GraphBuiltEventHandler GraphBuilt;



        public MainWindow()
        {
            InitializeComponent();


          
            TextFont.ItemsSource = Fonts.SystemFontFamilies;
            TextWeight.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold, FontWeights.UltraBold };
            GraphBuilt += (sender, args) =>
            {
                MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            };


        }

      

        private void DrawCycloid()
        {
          
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
           
            double centerX =(canvasWidth /2) +offsetY;
            double centerY = (canvasHeight / 2) + offsetY;
            UpdateScalingPoints(centerX, centerY);
          
            Polyline cycloid = new Polyline
            {
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };

            const double a = 1.0;  
            const int numPoints = 1000; 
            const double step = 0.01; 

         
            for (int i = 0; i < numPoints; i++)
            {
                double t = i * step;
                double x = a * (t - Math.Sin(t)) * scale + centerX;
                double y = -a * (1 - Math.Cos(t)) * scale + centerY;  

                cycloid.Points.Add(new Point(x, y));
            }

           
            MainCanvas.Children.Add(GraphTitle);  
            MainCanvas.Children.Add(cycloid);
            MainCanvas.Children.Add(BackgroundImage);  

      
        //    GraphBuilt?.Invoke(this, EventArgs.Empty);
        }


        private void UpdateScalingPoints(double centerX, double centerY)
        {
            const double a = 1.0;
            const int numPoints = 1000;
            const double step = 0.01;

         
            double tStart = 0.0;
            double startX = a * (tStart - Math.Sin(tStart)) * scale + centerX;
            double startY = -a * (1 - Math.Cos(tStart)) * scale + centerY;

          
            double tEnd = (numPoints - 1) * step;
            double endX = a * (tEnd - Math.Sin(tEnd)) * scale + centerX;
            double endY = -a * (1 - Math.Cos(tEnd)) * scale + centerY;

          
            double tMid = tEnd / 2;
            double midX = a * (tMid - Math.Sin(tMid)) * scale + centerX;
            double midY = -a * (1 - Math.Cos(tMid)) * scale + centerY;

            // Обновляем точки
            UpdatePoint(Brushes.Gray, startX, startY);    
            UpdatePoint(Brushes.Black, endX, endY);     
            UpdatePoint(Brushes.Orange, midX, midY);    
        }

        
        private void UpdatePoint(Brush color, double x, double y)
        {
            Ellipse point = MainCanvas.Children.OfType<Ellipse>().FirstOrDefault(p => p.Fill == color);

            if (point == null)
            {
                point = new Ellipse
                {
                    Width = 40,
                    Height = 40,
                    Fill = color
                };
                point.MouseDown += StartScaling;
                point.MouseMove += PerformScaling;
                point.MouseUp += StopScaling;
                MainCanvas.Children.Add(point);
            }

            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);
        }

        private void StartScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            currentScalingPoint = sender as Ellipse;
            previousMousePosition = e.GetPosition(MainCanvas);

            currentScalingPoint?.CaptureMouse();



        }


        private void PerformScaling(object sender, MouseEventArgs e)
        {
            if (currentScalingPoint == null || e.LeftButton != MouseButtonState.Pressed) return;

            Point currentMousePosition = e.GetPosition(MainCanvas);
            double deltaY = currentMousePosition.Y - previousMousePosition.Y;

            double scaleFactor = 1.0 + (deltaY / 100);

            if (Math.Abs(deltaY) > 0.1)
            {
                if (currentScalingPoint.Fill == Brushes.Gray)
                {
                    if (deltaY < 0) scale *= scaleFactor;
                    else scale /= scaleFactor;
                    lineColor = Brushes.Orange;
                }
                else if (currentScalingPoint.Fill == Brushes.Black)
                {
                    if (deltaY > 0) scale *= scaleFactor;
                    else scale /= scaleFactor;
                    lineColor = Brushes.Orange;
                }

                if (currentScalingPoint.Fill == Brushes.Orange)
                {
                    offsetX += currentMousePosition.X - previousMousePosition.X;
                    offsetY += currentMousePosition.Y - previousMousePosition.Y;
                    lineColor = Brushes.Red;
                }
                DrawCycloid();
           //     DrawHyperbolicSpiral();
                previousMousePosition = currentMousePosition;
            }
        }

        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;


            lineColor = Brushes.Orange;

            currentScalingPoint?.ReleaseMouseCapture();
            currentScalingPoint = null;
            //   DrawHyperbolicSpiral();
            DrawCycloid();
        }







        private void handleDraw(object sender, RoutedEventArgs e)
        {
            //  DrawHyperbolicSpiral();
            DrawCycloid();
        }



        private void drawDelegate(object sender, RoutedEventArgs e)
        {


            //    DrawHyperbolicSpiral();
            DrawCycloid();

            OnGraphBuilt();


        }

        private void OnGraphBuilt()
        {
            GraphBuilt?.Invoke(this, EventArgs.Empty);
        }





        private void graphScale(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphScaleTextBox.Text, out double newScale))
            {
                scale = newScale;
                //    DrawHyperbolicSpiral();
                DrawCycloid();
            }
            else
            {
                if (GraphScaleTextBox.Text.Length > 0)
                {

                    MessageBox.Show("Введите корректное значение!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void graphWidth(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphWidthTextBox.Text, out double newWidth))
            {
                lineWidth = newWidth;
                DrawCycloid();
                //   DrawHyperbolicSpiral();
            }
            else
            {
                if (GraphWidthTextBox.Text.Length > 0)
                {
                    MessageBox.Show("Введите корректное значение для толщины!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }



        private void graphColor(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (LineColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedColor == "Синий")
            {
                lineColor = Brushes.Blue;
            }
            else if (selectedColor == "Красный")
            {
                lineColor = Brushes.Red;
            }
            else if (selectedColor == "Черный")
            {
                lineColor = Brushes.Black;
            }
            else
            {
                lineColor = Brushes.Orange;
            }
            DrawCycloid();
            //   DrawHyperbolicSpiral();
        }

        private void graphText(object sender, TextChangedEventArgs e)
        {
            GraphTitle.Text = TextValue.Text;
        }





        private void textFont(object sender, SelectionChangedEventArgs e)
        {
            if (TextFont.SelectedItem is FontFamily selectedFont)
            {
                GraphTitle.FontFamily = selectedFont;
            }
        }

        private void textWeight(object sender, SelectionChangedEventArgs e)
        {
            if (TextWeight.SelectedItem is FontWeight selectedWeight)
            {
                GraphTitle.FontWeight = selectedWeight;
            }
        }



        private void textColor(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (TextColor.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedColor == "Черный")
            {
                GraphTitle.Foreground = Brushes.Black;
            }
            else if (selectedColor == "Синий")
            {
                GraphTitle.Foreground = Brushes.Blue;
            }
            else if (selectedColor == "Красный")
            {
                GraphTitle.Foreground = Brushes.Red;
            }
            else
            {
                GraphTitle.Foreground = Brushes.Black;
            }
        }



        private void AddBackground(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var imageSource = new ImageSourceConverter().ConvertFromString(openFileDialog.FileName) as ImageSource;
                    if (imageSource != null)
                    {
                        BackgroundImage.Source = imageSource;
                        BackgroundImage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка загрузки изображения.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }


        private void handleDownload(object sender, RoutedEventArgs e)
        {
            saveCanvas(MainCanvas);
        }
        private void saveCanvas(Canvas canvas)
        {
            RenderTargetBitmap renderTarget = new RenderTargetBitmap(
                (int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(canvas);

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PNG Files|*.png",
                FileName = "Graph.png"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                    encoder.Save(fs);
                }

                MessageBox.Show("График сохранен как PNG", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }




    }
}
