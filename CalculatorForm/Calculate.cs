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
            Stack<string> funcOperStack = new Stack<string>();
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
                            subExp[subExpCount - 1] += s[i] + " ";
                        }

                        if (s[i] == "(")
                        {
                            parenthesisCount++;
                            subExp[subExpCount - 1] += s[i] + " ";
                        }

                        if (s[i] == ")")
                        {
                            if (parenthesisCount > 0)
                            {
                                parenthesisCount--;
                                subExp[subExpCount - 1] += s[i] + " ";

                            }
                            else
                            {
                                if (CalcMath.FuncOperNames.Any(name => subExp[subExpCount - 1].Contains(name)) && subExp[subExpCount - 1].Length > 1)
                                {
                                    string[] ss = subExp[subExpCount - 1].Split(' ');

                                    int index = 0;
                                    while (!CalcMath.isFuncOper(ss[index])) 
                                    {
                                        index++;
                                    }

                                    int l_index = index - 1;
                                    int r_index = index + 1;

                                    double a = 0, b = 0;

                                    if (ss[l_index] == ")")
                                    {

                                    }
                                    else
                                    {
                                        a = Double.Parse(ss[l_index]);
                                    }
                                    if (ss[r_index] == "(")
                                    {

                                    }
                                    else
                                    {
                                        b = Double.Parse(ss[r_index]);
                                    }
                                }

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

            if (CalcMath.FuncOperNames.Any(name => newInput.Contains(name)) && newInput.Length > 1)
            {
                string[] ss = newInput.Split(' ');

                int index = 0;
                while (!CalcMath.isFuncOper(ss[index]))
                {
                    index++;
                }

                int l_index = index - 1;
                int r_index = index + 1;

                double a = 0, b = 0;

                if (ss[l_index] == ")")
                {

                }
                else
                {
                    a = Double.Parse(ss[l_index]);
                }
                if (ss[r_index] == "(")
                {

                }
                else
                {
                    b = Double.Parse(ss[r_index]);
                }

                string resultFuncOper = CalcMath.CalculateFuncOper(ss[index], a, b).ToString();
                string fixedInput = "";
                for (int i = 0; i < ss.Length; i++)
                {
                    if (i != l_index && i != r_index && i != index)
                        fixedInput += ss[i] + " ";
                    if (i == index)
                        fixedInput += resultFuncOper + " ";
                }
                newInput = fixedInput;
            }

            var expression = new Expression(newInput);

            var result = expression.Evaluate();

            return Convert.ToDouble(result);
        }
    }
}
