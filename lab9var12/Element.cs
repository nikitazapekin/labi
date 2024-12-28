

 
using System;


public interface IInvertible
{
    void Invert();
   
}

public abstract class Element : IInvertible

{
    private string name;
    private int inputCount;
    private int outputCount;




    public virtual void SetInputs(int[] inputs)
    {
        throw new NotImplementedException("Ошибка");
    }

    public virtual void Invert()
    {
        throw new NotImplementedException("Ошибка");
    }

    public abstract int ComputeOutput();





    public virtual string ToBinaryString()
    {
        throw new NotImplementedException();
    }
    public virtual void FromBinaryString(string dataString)
    {
        throw new NotImplementedException();
    }




    public Element(string name, int inputCount = 1, int outputCount = 1)
    {
        this.name = name;
        this.inputCount = inputCount;
        this.outputCount = outputCount;
    }



    public string Name => name;
    public int InputCount
    {
        get => inputCount;
        set
        {
            if (value < 0)
                throw new ArgumentException("Входы не могут быть отрицательными.");
            inputCount = value;
        }
    }
    public int OutputCount
    {
        get => outputCount;
        set
        {
            if (value < 0)
                throw new ArgumentException("Выходы не могут быть отрицательными.");
            outputCount = value;
        }
    }



   


}


