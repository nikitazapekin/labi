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
        private Combinational combinationalElement;
        private Memory memory;


        public MainWindow()
        {
            InitializeComponent();
            combinationalElement = new Combinational(4);
            memory = new Memory(3);
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
            var inputs = TriggerInputs.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
 

            if (inputs.Length == 0  )
            {
                MessageBox.Show("Введите данные.");
                return;
            }
            if (inputs.Length != 3)
            {
                MessageBox.Show("Введите 3 числа 1 или 0.");
                return;
            }


                foreach (var input in inputs)
            {
                if (input != "1" && input != "0")
                {
                    MessageBox.Show("Все введенные значения должны быть либо 1, либо 0.");
                    return;
                }
            }




            int[] parsedInputs = new int[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                if (!int.TryParse(inputs[i], out parsedInputs[i]))
                {
                    MessageBox.Show("Все значения должны быть числами.");
                    return;
                }
            }

            memory.SetInputs(parsedInputs);








        }
    }
}
