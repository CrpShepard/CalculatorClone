using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCalc;

namespace CalculatorForm
{
    public class Calculate
    {
        static public double getResult(string input)
        {
            string[] s = input.Split(' ');
            string newInput = "";
            
            Stack<string> funcStack = new Stack<string>();
            //string subExp = "";
            List<String> subExp = new List<string>();
            int subExpCount = 0;
            int parenthesisCount = 0;

            bool ready = true;
            while (true)
            {
                ready = true;
                newInput = "";

                for (int i = 0; i < s.Length; i++)
                {
                    if (CalcMath.FuncNames.Any(name => s[i].Contains(name)) && s[i].Length > 1)
                    {
                        subExpCount++;
                        funcStack.Push(s[i].Split('(')[0]);
                        subExp.Add("");

                        ready = false;
                    }

                    if (subExpCount > 0)
                    {
                        if (double.TryParse(s[i], out _) || new string[] { "+", "-", "*", "/", "." }.Any(oper => s[i].Contains(oper)))
                        {
                            //subExp += s[i];
                            subExp[subExpCount - 1] += s[i];
                        }

                        if (s[i] == "(")
                        {
                            parenthesisCount++;
                            subExp[subExpCount - 1] += s[i];
                        }

                        if (s[i] == ")")
                        {
                            if (parenthesisCount > 0)
                            {
                                parenthesisCount--;
                                subExp[subExpCount - 1] += s[i];

                            }
                            else
                            {
                                subExp[subExpCount - 1] = new Expression(subExp[subExpCount - 1]).Evaluate().ToString();
                                subExp[subExpCount - 1] = CalcMath.Calculate(funcStack.Pop(), Convert.ToDouble(subExp[subExpCount - 1])).ToString();
                                subExpCount--;

                                if (subExpCount > 0)
                                {
                                    subExp[subExpCount - 1] += subExp[subExpCount];
                                }

                                else if (subExpCount == 0)
                                {
                                    newInput += subExp[0] + " ";
                                    subExp.Clear();
                                }

                            }

                        }
                    }

                    if (!double.TryParse(s[i], out _) && !new string[] { "+", "-", "*", "/" }.Any(oper => s[i].Contains(oper)) && s[i].Length == 1)
                    {
                        foreach (var item in Form1.listBoxGlobal.Items)
                        {
                            if (item.ToString()[0] == Convert.ToChar(s[i]))
                            {
                                ready = false;

                                newInput += item.ToString().Remove(0, 2) + " ";
                            }
                        }
                    }

                    else if (subExpCount == 0)
                    {
                        //subExp = "";
                        newInput += s[i] + " ";
                        //subExp.Clear();
                    }
                }
                s = newInput.Split(' ');
                

                if (ready)
                    break;
            }

            //var expression = new Expression(input);

            //var expression = new Expression(newInput);

            newInput = newInput.Replace(",", ".");

            var expression = new Expression(newInput);

            var result = expression.Evaluate();

            return Convert.ToDouble(result);
        }
    }
}
