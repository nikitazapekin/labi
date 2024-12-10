using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal class Circle
    {



        public double Radius { get; private set; }

        public double Area
        {
            get => Math.PI * Math.Pow(Radius, 2);
        }

        public double Circumference
        {
            get => 2 * Math.PI * Radius;
        }

        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException("Радиус должен быть больше 0.");
            }
            Radius = radius;
        }

        public bool IsPointInside(double x, double y)
        {
            // Проверка, попадает ли точка (x, y) внутри круга с центром в (0, 0)
            return (x * x + y * y) <= (Radius * Radius);
        }



    }
}
