using System;
using System.Collections.Generic;
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

}
 