using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorForm
{
    public class CalcMath
    {
        static public string[] FuncNames =
        {
            "sqr", "abs", "sqrt", "fact",
            "log", "ln", "negate", "cube", "cuberoot", "exp", // negate это +/- (отриц знач)
            //"yroot", "logbase", ",exp+", "Mod", - операторы скорее
            "sin", "cos", "tan", "sec", "csc", "cot",
            "asin", "acos", "atan", "asec", "acsc", "acot", // asin => sin^(-1)
            "sinh", "cosh", "tanh", "sech", "csch", "coth",
            "asinh", "acosh", "atanh", "asech", "acsch", "acoth",
            "floor", "ceil", "dms", "degrees",
            "ten", "two", "onediv"
        };

        static public bool isFunc(string s)
        {
            if (FuncNames.Any(s.Contains))
                return true;
            return false;
            //return FuncNames.Contains(s);
        }

        static public double Calculate(string s, double a)
        {
            double result = 0;
            switch (s) 
            { 
                case "sqr": result = Math.Pow(a, 2); break;
                case "abs": result = Math.Abs(a); break;
                case "sqrt": result = Math.Sqrt(a); break;
                case "fact": if (a > 1) { for (int i = 2; i <= a; i++) { result *= i; } } else result = 1; break;
                case "log": result = Math.Log10(a); break;
                case "ln": result = Math.Log(a); break;
                case "negate": result = -a; break;
                case "cube": result = Math.Pow(a, 3); break;
                case "cuberoot": result = Math.Pow(a, 1 / 3); break;
                case "exp": result = Math.Exp(a); break;

                case "sin": result = Math.Sin(a); break;
                case "cos": result = Math.Cos(a); break;
                case "tan": result = Math.Tan(a); break;
                case "sec": result = 1 / Math.Cos(a); break;
                case "csc": result = 1 / Math.Sin(a); break;
                case "cot": result = 1 / Math.Tan(a); break;

                case "asin": result = Math.Asin(a); break;
                case "acos": result = Math.Acos(a); break;
                case "atan": result = Math.Atan(a); break;
                case "asec": result = Math.Acos(1 / a); break;
                case "acsc": result = Math.Asin(1 / a); break;
                case "acot": result = Math.Atan(1 / a); break;

                case "sinh": result = (Math.Exp(a) - Math.Exp(-a)) / 2; break;
                case "cosh": result = (Math.Exp(a) + Math.Exp(-a)) / 2; break;
                case "tanh": result = ((Math.Exp(a) - Math.Exp(-a)) / 2) / ((Math.Exp(a) + Math.Exp(-a)) / 2); break;
                case "sech": result = 1 / ((Math.Exp(a) + Math.Exp(-a)) / 2); break;
                case "csch": result = 1 / ((Math.Exp(a) - Math.Exp(-a)) / 2); break;
                case "coth": result = 1 / (((Math.Exp(a) - Math.Exp(-a)) / 2) / ((Math.Exp(a) + Math.Exp(-a)) / 2)); break;

                case "asinh": result = Math.Log(a + Math.Sqrt(a * a + 1)); break;
                case "acosh": result = Math.Log(a + Math.Sqrt(a * a - 1)); break;
                case "atanh": result = (1 / 2) * Math.Log((1 + a) / (1 - a)); break;
                case "asech": result = Math.Log((1 + Math.Sqrt(1 - a * a)) / a); break;
                case "acsch": result = Math.Log(1 / a + Math.Sqrt(1 + 1 / a * a)); break;
                case "acoth": result = (1 / 2) * Math.Log((a + 1) / (a - 1)); break;

                case "floor": result = Math.Floor(a); break;
                case "ceil": result = Math.Ceiling(a); break;
                case "dms": result = a + Math.Floor(((a - Math.Floor(a)) * 60) % 60) / 100 + Math.Floor(((a - Math.Floor(a)) * 60) % 60) / 10000; break;
                case "degrees": result = a - Math.Floor(a) + (a - Math.Floor(a)) / 100 / 60 + Math.Floor(a) + (a - Math.Floor(a)) / 10000 / 3600; break;

                case "ten": result = Math.Pow(10, a); break;
                case "two": result = Math.Pow(2, a); break;
                case "onediv": result = 1 / a; break;
            }
            return result;
        }
    }
}
