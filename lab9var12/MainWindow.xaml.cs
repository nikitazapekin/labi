﻿using Microsoft.Win32;
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
         
        private void InvertComb_Click(object sender, RoutedEventArgs e)
        {
            combinationalElement.Invert();

            int[] inputs = new int[4];
          inputs=   combinationalElement.GetInputs();
            Input1.Text = inputs[0].ToString();
            Input2.Text = inputs[1].ToString();
            Input3.Text = inputs[2].ToString();
            Input4.Text = inputs[3].ToString();


        }

 

        private void InvertMemory_Click(object sender, RoutedEventArgs e)
        {

            try
            {

          memory.Invert();

            int[] inputs = new int[2];
            inputs = memory.GetInputs();

            string inputsString = string.Join(", ", inputs);

         
            TriggerInputs.Text = inputsString;
            } catch
            {
                MessageBox.Show("Ошибка");
            }



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

 
       

        private void SetRandomTriggerButton(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                int[][] parsedInputs = new int[4][];  

                for (int i = 0; i < parsedInputs.Length; i++)
                {
               
                    parsedInputs[i] = new int[]
                    {
                random.Next(0, 2), // R
                random.Next(0, 2), // S
                random.Next(0, 2)  // CLK
                    };
                }

              
                register.SetInputs(parsedInputs);

            
                string formattedInputs = "";
                for (int i = 0; i < parsedInputs.Length; i++)
                {
                    formattedInputs += $"[{parsedInputs[i][0]}, {parsedInputs[i][1]}, {parsedInputs[i][2]}] ";
                }

             
                TriggerInputsArray.Text = "Регистры: " + formattedInputs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }




        private void SetTriggerIndexButton(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int triggerIndex = int.Parse(TriggerIndex.Text);
 
                string[] triggerValues = TriggerRegisterValues.Text.Split(' ');

               
                if (triggerValues.Length != 3)
                {
                    MessageBox.Show("Ошибка: Введите 3 значения (0 или 1).");
                    return;
                }

              
                int[] parsedValues = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    parsedValues[i] = int.Parse(triggerValues[i]);

               
                    if (parsedValues[i] != 0 && parsedValues[i] != 1)
                    {
                        MessageBox.Show("Ошибка: Значения должны быть 0 или 1.");
                        return;
                    }
                }

               
                if (triggerIndex < 0 || triggerIndex >= register.GetInputs().Length)
                {
                    MessageBox.Show("Ошибка: Индекс выходит за пределы массива.");
                    return;
                }

               
                var currentInputs = register.GetInputs();

              
                currentInputs[triggerIndex] = parsedValues;

              
                register.SetInputs(currentInputs);

               
                string formattedInputs = "";
                foreach (var input in currentInputs)
                {
                    formattedInputs += $"[{input[0]}, {input[1]}, {input[2]}] ";
                }

               
                TriggerInputsArray.Text = "Регистры: " + formattedInputs;
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Неверный формат ввода.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }



   

        private void InvertRegister(object sender, RoutedEventArgs e)
        {
            try
            {
             
                register.Invert();

            
                int[][] parsedInputs = register.GetInputs();

             
                string formattedInputs = "";
                for (int i = 0; i < parsedInputs.Length; i++)
                {
                    formattedInputs += $"[{parsedInputs[i][0]}, {parsedInputs[i][1]}, {parsedInputs[i][2]}] ";
                }

            
                TriggerInputsArray.Text = "Регистры: " + formattedInputs;
            }
            catch
            {
                MessageBox.Show("Ошибка инвертирования.");
            }
        }

        
        private void ByteMove(object sender, RoutedEventArgs e)
        {

            try
            {

            register.Shift(1);

            int[][] parsedInputs = register.GetInputs();


            string formattedInputs = "";
            for (int i = 0; i < parsedInputs.Length; i++)
            {
                formattedInputs += $"[{parsedInputs[i][0]}, {parsedInputs[i][1]}, {parsedInputs[i][2]}] ";
            }


            TriggerInputsArray.Text = "Регистры: " + formattedInputs;

            } catch
            {
                MessageBox.Show("Ошибка");
            }
        
        }
        private void  SaveComb(object sender, RoutedEventArgs e)

        {
         int[] inputs = new int[4];
    

                   try
            {
                string fileName = $"comb_save";
                using (var writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(combinationalElement.ToBinaryString());
                }
                MessageBox.Show($"Комбинированный элемент сохранен в файл: {fileName}");

            } catch
            {
                MessageBox.Show("Ошибка");
            }

            }

        private void LoadComb(object sender, RoutedEventArgs e)

        {
            string fileName = $"comb_save";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);






            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {
                string binaryData = File.ReadAllText(filePath);


                combinationalElement.FromBinaryString(binaryData);

                MessageBox.Show("Данные для Combinational успешно загружены.");

                int[] inputs = new int[4];
                inputs = combinationalElement.GetInputs();

                Input1.Text = inputs[0].ToString();
                Input2.Text = inputs[1].ToString();
                Input3.Text = inputs[2].ToString();
                Input4.Text = inputs[3].ToString();



            }
            catch
            {
                MessageBox.Show("Ошибка");
            }


        }


    
        private void SaveMemory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = "memory_save.bin";  
                memory.SaveToBinary(fileName); 
                MessageBox.Show($"Элемент триггера сохранен в файл: {fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

         
        private void LoadMemory_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "memory_save.bin";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {
               
                memory.LoadFromBinary(filePath);

                MessageBox.Show("Данные для Memory успешно загружены.");

               
                int[] inputs = memory.GetInputs();

                
                string inputsString = string.Join(", ", inputs);
 
                TriggerInputs.Text = inputsString;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }



        private void SaveRegister(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = "register_save.bin";
               register.SaveToBinary(fileName);
                MessageBox.Show($"Элемены регистров  сохранен в файл: {fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }



        private void LoadRegister(object sender, RoutedEventArgs e)
        {

            string fileName = "register_save.bin";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {

           register.LoadFromBinary(filePath);

                MessageBox.Show("Данные для Register успешно загружены.");


               


                int[][] parsedInputs = register.GetInputs();


                string formattedInputs = "";
                for (int i = 0; i < parsedInputs.Length; i++)
                {
                    formattedInputs += $"[{parsedInputs[i][0]}, {parsedInputs[i][1]}, {parsedInputs[i][2]}] ";
                }


                TriggerInputsArray.Text = "Регистры: " + formattedInputs;



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }
    }

}

 