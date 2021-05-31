using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Services
{
    interface ICalcularor
    {
        int Sum(int x, int y);
        int Substract(int x, int y);
        double Divide(int x, int y);
        int Multiply(int x, int y);
    }
}
