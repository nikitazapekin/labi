using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Memory : Element
{
    private int[] inputs;

   
    public Memory(int inputCount) : base("Память", inputCount, 1)
    {
        inputs = new int[inputCount];
    }


    public override int ComputeOutput()
    {
        int reset = inputs[0]; // R
        int set = inputs[1];   // S
        int clk = inputs[2];   // CLK

        int output=0;
        if (reset == 1)
        {
            output = 0; 
        }
        else if (set == 1)
        {
            output = 1; 
        }
      

        return output;
      
    }
    public override void SetInputs(int[] inputValues)
    {
        if (inputValues.Length != 3)
        {
            throw new ArgumentException($"Ожидалось 3 числа: R, S, CLK.");
        }

       
        if (inputs[0] == 1 && inputs[1] == 1)
        {
            throw new InvalidOperationException("Ошибка: R и S не могут быть активны одновременно");
        }

        
        inputs = inputValues;
    }


    public int[] GetInputs()
    {
        return inputs;
    }


    public override void Invert()
    {
       for(int i=0; i<inputs.Length; i++)
        {
            inputs[i] = inputs[i] == 0 ? 1 : 0;

        }
    }
    /*
 
    public void SaveToBinary(string fileName)
    {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        using (var writer = new BinaryWriter(fs))
        {
           
            writer.Write(inputs.Length);  
            foreach (var input in inputs)
            {
                writer.Write(input);  
            }

           
            writer.Write(Name);   
        }
    }
    */

    /*
    
    public void LoadFromBinary(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException("Файл не найден.");

        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (var reader = new BinaryReader(fs))
        {
            inputs[0] = reader.ReadInt32();
            inputs[1] = reader.ReadInt32();
            inputs[2] = reader.ReadInt32();
        }
    }

    */


    public void SaveToBinary(string fileName)
    {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        using (var writer = new BinaryWriter(fs))
        {
            // Сохраняем длину массива
            writer.Write(inputs.Length);

            // Сохраняем элементы массива
            foreach (var input in inputs)
            {
                writer.Write(input);
            }
        }
    }




    public void LoadFromBinary(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException("Файл не найден.");

        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (var reader = new BinaryReader(fs))
        {
            // Читаем длину массива
            int length = reader.ReadInt32();  // Читаем размер массива
            inputs = new int[length];  // Создаём новый массив нужного размера

            // Читаем значения массива
            for (int i = 0; i < length; i++)
            {
                inputs[i] = reader.ReadInt32();  // Читаем каждое значение
            }
        }
    }






}
