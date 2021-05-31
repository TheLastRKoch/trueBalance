using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Services
{
    public class Calculator : ICalcularor
    {
        public double Divide(int x, int y)
        {
            return x / y;
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }

        public int Substract(int x, int y)
        {
            return x - y;
        }

        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
