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
        base.Invert();
    }

    public void SetInputs(int[][] inputValues)
    {
        if (inputValues.Length != memories.Length)
            throw new ArgumentException($"Ошибка.");

        for (int i = 0; i < memories.Length; i++)
        {
            if (inputValues[i].Length != 3)
                throw new ArgumentException($"Ошибка.");

            inputs[i] = inputValues[i];
         MessageBox.Show($"Регистр {i+1} после обновления: [{inputs[i][0]}, {inputs[i][1]}, {inputs[i][2]}]");
        }
    }


    public void Shift(int bits)
    {
        throw new NotImplementedException();
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

