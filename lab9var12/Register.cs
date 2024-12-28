using System;
using System.Collections.Generic;
using System.IO;
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


  
    public void Shift(int bits)
    {
      
        if (bits != 1)
        {
            throw new ArgumentException("Метод поддерживает только сдвиг на 1 бит.");
        }

     
        foreach (var memory in memories)
        {
            try
            {
            
                int[] memoryInputs = memory.GetInputs();

               
                if (memoryInputs == null || memoryInputs.Length != 3)
                {
                    throw new InvalidOperationException("Неверные входные данные для памяти.");
                }

             
                int last = memoryInputs[memoryInputs.Length - 1]; 
                for (int i = memoryInputs.Length - 1; i > 0; i--)
                {
                    memoryInputs[i] = memoryInputs[i - 1];  
                }
                memoryInputs[0] = last; 

            
                memory.SetInputs(memoryInputs);
            }
            catch (Exception ex)
            {
             
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





    public void LoadFromBinary(string fileName)
    {
        try
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не найден.");

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i][0] = reader.ReadInt32();
                    inputs[i][1] = reader.ReadInt32();
                    inputs[i][2] = reader.ReadInt32();
                }
            }

            MessageBox.Show("Данные успешно загружены из бинарного файла.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке бинарных данных: {ex.Message}");
        }
    }


    public void SaveToBinary(string fileName)
    {
        try
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {

                for (int i = 0; i < inputs.Length; i++)
                {
                    writer.Write(inputs[i][0]);
                    writer.Write(inputs[i][1]);
                    writer.Write(inputs[i][2]);
                }
            }
            MessageBox.Show($"Данные успешно сохранены в файл: {fileName}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении бинарных данных: {ex.Message}");
        }
    }






}

