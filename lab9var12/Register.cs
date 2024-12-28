using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public interface IShiftable
{
    void Shift(int bits);
}

public class Register : Element, IShiftable
{

    private static int ResetState = 0;
    private static int SetState = 0;
    private Memory[] memories;
    private int[][] inputs;


    public Register( int inputCount = 1) : base("Register", inputCount, 1)
    {

        memories = new Memory[inputCount];
        inputs = new int[inputCount][];

        for (int i = 0; i < inputCount; i++)
        {
            memories[i] = new Memory(3);
            inputs[i] = new int[3];
        }


    }

    public override int ComputeOutput()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override void FromBinaryString(string dataString)
    {
        base.FromBinaryString(dataString);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void Invert()
    {
        foreach (var memory in memories)
        {
            memory.Invert();
        }
    }

    public void SetInputs(int[][] inputValues)
    {
        if (inputValues.Length != memories.Length)
            throw new ArgumentException($"Ошибка: Размерность входных данных не совпадает с размером памяти.");

        for (int i = 0; i < memories.Length; i++)
        {
            if (inputValues[i].Length != 3)
                throw new ArgumentException($"Ошибка: Каждый входной массив должен иметь длину 3.");


            inputs[i] = inputValues[i];
            memories[i].SetInputs(inputValues[i]);
        }
    }


    /*
    public void Shift(int bits)
    {
     
        foreach (var memory in memories)
        { 
            int[] memoryInputs = memory.GetInputs();

        
            for (int i = 0; i < memoryInputs.Length; i++)
            {
           
                memoryInputs[i] = bits > 0 ? memoryInputs[i] << bits : memoryInputs[i] >> Math.Abs(bits);
            }
 
            memory.SetInputs(memoryInputs);
        }
    }
    */
    /*
    public void Shift(int bits)
    {
      
        if (bits != 1)
        {
            throw new ArgumentException("Метод поддерживает только сдвиг на 1 бит.");
        }
 
        foreach (var memory in memories)
        {
         
            int[] memoryInputs = memory.GetInputs();

         
            int last = memoryInputs[memoryInputs.Length - 1]; 
            for (int i = memoryInputs.Length - 1; i > 0; i--)
            {
                memoryInputs[i] = memoryInputs[i - 1];  
            }
            memoryInputs[0] = last; 
            memory.SetInputs(memoryInputs);
        }
    }
    */
    public void Shift(int bits)
    {
        // Проверяем, что сдвиг равен 1
        if (bits != 1)
        {
            throw new ArgumentException("Метод поддерживает только сдвиг на 1 бит.");
        }

        // Проходим по каждому элементу памяти
        foreach (var memory in memories)
        {
            try
            {
                // Получаем текущие входные данные
                int[] memoryInputs = memory.GetInputs();

                // Проверяем, что входные данные корректны
                if (memoryInputs == null || memoryInputs.Length != 3)
                {
                    throw new InvalidOperationException("Неверные входные данные для памяти.");
                }

                // Циркулярный сдвиг вправо на 1
                int last = memoryInputs[memoryInputs.Length - 1]; // Сохраняем последний элемент
                for (int i = memoryInputs.Length - 1; i > 0; i--)
                {
                    memoryInputs[i] = memoryInputs[i - 1]; // Сдвигаем элементы вправо
                }
                memoryInputs[0] = last; // Перемещаем последний элемент в начало

                // Обновляем входные данные в памяти
                memory.SetInputs(memoryInputs);
            }
            catch (Exception ex)
            {
                // Выводим ошибку, если что-то пошло не так с этим элементом
                MessageBox.Show($"Ошибка при сдвиге памяти: {ex.Message}");
            }
        }
    }





    public override string ToBinaryString()
    {
        return base.ToBinaryString();
    }

    public override string? ToString()
    {
        return base.ToString();
    }

    public int[][] GetInputs()
    {
        return inputs;
    }

}

