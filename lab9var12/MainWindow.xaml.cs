using Microsoft.Win32;
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

namespace lab9var12
{
    public partial class MainWindow : Window
    {
        private Register register;

        private Combinational combinationalElement;
        private Memory memory;


        public MainWindow()
        {
            InitializeComponent();
            combinationalElement = new Combinational(4);
            memory = new Memory(3);
            register = new Register(4);
        }

        private void ComputeOutput_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                int[] inputs = new int[4];
                inputs[0] = ParseInput(Input1.Text, "Вход 1");
                inputs[1] = ParseInput(Input2.Text, "Вход 2");
                inputs[2] = ParseInput(Input3.Text, "Вход 3");
                inputs[3] = ParseInput(Input4.Text, "Вход 4");

                combinationalElement.SetInputs(inputs);
                combinationalElement.SetInputs(inputs);
                int result = combinationalElement.ComputeOutput();
                OutputText.Text = $"Результат: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private int ParseInput(string input, string fieldName)
        {
            if (!int.TryParse(input, out int value) || (value != 0 && value != 1))
            {
                throw new ArgumentException($"Поле \"{fieldName}\" должно содержать только 0 или 1.");
            }
            return value;
        }

   

        private void SetInputs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var inputs = TriggerInputs.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

           /*     if (inputs.Length != 3)
                {
                    MessageBox.Show("Введите ровно 3 числа (0 или 1).");
                    return;
                }
           */

                int[] parsedInputs = new int[inputs.Length];

                for (int i = 0; i < inputs.Length; i++)
                {
                    if (!int.TryParse(inputs[i], out parsedInputs[i]) || (parsedInputs[i] != 0 && parsedInputs[i] != 1))
                    {
                        MessageBox.Show("Все значения должны быть либо 0, либо 1.");
                        return;
                    }
                }

               
                memory.SetInputs(parsedInputs);

            
                TriggerValues.Text = $"Входы: {string.Join(" ", parsedInputs)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void SetOutput_Click(object sender, RoutedEventArgs e)
        {
          TriggerOutput.Text = $"Выход: {memory.ComputeOutput()}";
        }





        /*
        private void SetRandomTriggerButton(object sender, RoutedEventArgs e)
        {
          //  TriggerInputsArray.Text = "Регистры: wddwq";
            try
            {
                int[][] parsedInputs;
                parsedInputs = new int[3][];
                parsedInputs[0] = new int[] { 1, 1, 1 };
                parsedInputs[1] = new int[] { 1, 1, 0 };
                parsedInputs[2] = new int[] { 1, 0, 1 };
                parsedInputs[2] = new int[] { 0, 0, 1 };
                register.SetInputs(parsedInputs);

                var states = register.GetInputs();
                string formattedInputs = "";
                for (int i = 0; i < states.Length; i++)
                {
                    formattedInputs += $"[{states[i][0]}, {states[i][1]}, {states[i][2]}] ";
                }
                TriggerInputsArray.Text = "Регистры: " + formattedInputs;

            } catch
            {
                MessageBox.Show("Ошибка.");
            }
            }

        */



        private void SetRandomTriggerButton(object sender, RoutedEventArgs e)
        {
            try
            {
                int[][] parsedInputs = new int[4][];  // Исправлено: был дублирующий индекс
                parsedInputs[0] = new int[] { 1, 1, 1 };
                parsedInputs[1] = new int[] { 1, 1, 0 };
                parsedInputs[2] = new int[] { 1, 0, 1 };
                parsedInputs[3] = new int[] { 0, 0, 1 }; // Исправлено

                register.SetInputs(parsedInputs);  // Применяем новые значения

                var states = register.GetInputs();
                string formattedInputs = "";
                for (int i = 0; i < parsedInputs.Length; i++)
                {
                    formattedInputs += $"[{parsedInputs[i][0]}, {parsedInputs[i][1]}, {parsedInputs[i][2]}] ";
                }

                // Обновляем текст в UI
                TriggerInputsArray.Text = "Регистры: " + formattedInputs;
            }
            catch
            {
                MessageBox.Show("Ошибка.");
            }
        }






    }
    //   int[][] parsedInputs;

}
