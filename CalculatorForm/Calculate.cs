﻿using System;
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
        string FuncRecursion(string input, int index)
        {
            string[] s = input.Split(' ');
            string newInput = "";

            Stack<string> funcStack = new Stack<string>();
            Stack<string> funcOperStack = new Stack<string>();
            List<String> subExp = new List<string>();
            int subExpCount = 0;
            List<int> parenthesisCount = new List<int>();

            for (int i = index; i < s.Length; i++)
            {
                if (CalcMath.FuncNames.Any(name => s[i].Contains(name)) && s[i].Length > 1)
                {
                    if (subExpCount == 0)
                    {
                        subExpCount++;
                        funcStack.Push(s[i].Split('(')[0]);
                        subExp.Add("");
                        parenthesisCount.Add(0);
                    }
                    else
                        subExp[subExpCount - 1] += FuncRecursion(input, i);

                    //ready = false;
                }

                if (subExpCount == 0)
                {
                    //break;
                    //newInput += s[i] + " ";
                }

                if (subExpCount > 0)
                {
                    if (double.TryParse(s[i], out _) || new string[] { "+", "-", "*", "/", "." }.Any(oper => s[i].Contains(oper)))
                    {
                        subExp[subExpCount - 1] += s[i] + " ";
                    }

                    if (s[i] == "(")
                    {
                        parenthesisCount[subExpCount - 1]++;
                        subExp[subExpCount - 1] += s[i] + " ";
                    }

                    if (s[i] == ")")
                    {
                        if (parenthesisCount[subExpCount - 1] > 0)
                        {
                            parenthesisCount[subExpCount - 1]--;
                            subExp[subExpCount - 1] += s[i] + " ";

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

                if (!double.TryParse(s[i], out _) && !new string[] { "+", "-", "*", "/", "(", ")" }.Any(oper => s[i].Contains(oper)) && s[i].Length == 1)
                {
                    foreach (var item in Form1.listBoxGlobal.Items)
                    {
                        if (item.ToString()[0] == Convert.ToChar(s[i]))
                        {
                            //ready = false;

                            newInput += item.ToString().Remove(0, 2) + " ";
                        }
                    }
                }


            }

            return newInput;
        }

        static public double getResult(string input)
        {
            string[] s = input.Split(' ');
            string newInput = "";

            Stack<string> funcStack = new Stack<string>();
            Stack<string> funcOperStack = new Stack<string>();
            List<String> subExp = new List<string>();
            int subExpCount = 0;
            List<int> parenthesisCount = new List<int>();

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
                        parenthesisCount.Add(0);

                        ready = false;
                    }

                    if (subExpCount == 0)
                    {
                        newInput += s[i] + " ";
                    }

                    if (subExpCount > 0)
                    {
                        if (double.TryParse(s[i], out _) || new string[] { "+", "-", "*", "/", "." }.Any(oper => s[i].Contains(oper)))
                        {
                            subExp[subExpCount - 1] += s[i] + " ";
                        }

                        if (s[i] == "(")
                        {
                            parenthesisCount[subExpCount - 1]++;
                            subExp[subExpCount - 1] += s[i] + " ";
                        }

                        if (s[i] == ")")
                        {
                            if (parenthesisCount[subExpCount - 1] > 0)
                            {
                                parenthesisCount[subExpCount - 1]--;
                                subExp[subExpCount - 1] += s[i] + " ";

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

                    if (!double.TryParse(s[i], out _) && !new string[] { "+", "-", "*", "/", "(", ")" }.Any(oper => s[i].Contains(oper)) && s[i].Length == 1)
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


                }
                s = newInput.Split(' ');


                if (ready)
                {
                    subExpCount = 0;
                    parenthesisCount.Clear();
                    subExp.Clear();
                    funcStack.Clear();
                    funcOperStack.Clear();
                    break;
                }

            }

            //var expression = new Expression(input);

            //var expression = new Expression(newInput);

            newInput = newInput.Replace(",", ".");

            (string, int) OperRecursion(int index)
            {
                string[] ss = newInput.Split(' ');

                Stack<string> funcOperStackFlat = new Stack<string>();
                //string subExp = "";
                List<String> subExpOper = new List<string>();
                int subExpCountOper = 0;
                List<int> parenthesisCountOper = new List<int>();

                //int index = index_;
                while (!CalcMath.isFuncOper(ss[index]))
                {
                    index++;
                }

                int l_index = index - 1;
                int r_index = index + 1;

                double a = 0, b = 0;

                if (ss[l_index] == ")")
                {
                    subExpCountOper++;
                    funcOperStackFlat.Push(ss[index]);
                    subExpOper.Add("");
                    parenthesisCountOper.Add(0);

                    l_index--;

                    while (subExpCountOper > 0)
                    {
                        if (ss[l_index] == ")")
                        {
                            //subExpCountFlat++;
                            //subExpFlat.Add("");
                            //parenthesisCountOper++;
                            parenthesisCountOper[subExpCountOper - 1]++;
                            subExpOper[subExpCountOper - 1] += " )";
                        }
                        if (ss[l_index] == "(")
                        {
                            //if (parenthesisCountOper > 0)
                            if (parenthesisCountOper[subExpCountOper - 1] > 0)
                            {
                                //parenthesisCountOper--;
                                parenthesisCountOper[subExpCountOper - 1]--;
                                subExpOper[subExpCountOper - 1] += " (";
                            }
                            else
                            {
                                subExpOper[subExpCountOper - 1] = new string(subExpOper[subExpCountOper - 1].Reverse().ToArray());
                                subExpOper[subExpCountOper - 1] = new Expression(subExpOper[subExpCountOper - 1]).Evaluate().ToString();
                                subExpCountOper--;

                                if (subExpCountOper > 0)
                                {
                                    subExpOper[subExpCountOper - 1] += subExpOper[subExpCountOper];
                                }

                                else if (subExpCountOper == 0)
                                {
                                    a = Double.Parse(subExpOper[0]);
                                    subExpOper.Clear();
                                }
                            }
                        }
                        if (ss[l_index] != "(" && ss[l_index] != ")")
                        {
                            subExpOper[subExpCountOper - 1] += " " + ss[l_index];
                        }
                        l_index--;
                    }
                }
                else
                {
                    a = Double.Parse(ss[l_index]);
                }
                if (ss[r_index] == "(")
                {
                    subExpCountOper++;
                    funcOperStackFlat.Push(ss[index]);
                    subExpOper.Add("");
                    parenthesisCountOper.Add(0);

                    r_index++;

                    while (subExpCountOper > 0)
                    {
                        if (CalcMath.isFuncOper(ss[r_index]))
                        {
                            var localResult = OperRecursion(r_index);

                            string[] tempS = subExpOper[subExpCountOper - 1].Split(' ');
                            tempS[tempS.Length - 1] = localResult.Item1;
                            subExpOper[subExpCountOper - 1] = string.Join(" ", tempS);

                            r_index = localResult.Item2;
                            r_index++;
                        }

                        if (ss[r_index] == "(")
                        {
                            //subExpCountFlat++;
                            //subExpFlat.Add("");
                            //parenthesisCountOper++;
                            parenthesisCountOper[subExpCountOper - 1]++;
                            subExpOper[subExpCountOper - 1] += "( ";
                        }
                        if (ss[r_index] == ")")
                        {
                            //if (parenthesisCountOper > 0)
                            if (parenthesisCountOper[subExpCountOper - 1] > 0)
                            {
                                //parenthesisCountOper--;
                                parenthesisCountOper[subExpCountOper - 1]--;
                                subExpOper[subExpCountOper - 1] += ") ";
                            }
                            else
                            {
                                //subExpFlat[subExpCountFlat - 1] = new string(subExpFlat[subExpCountFlat - 1].Reverse().ToArray());
                                subExpOper[subExpCountOper - 1] = new Expression(subExpOper[subExpCountOper - 1]).Evaluate().ToString();
                                subExpCountOper--;

                                if (subExpCountOper > 0)
                                {
                                    subExpOper[subExpCountOper - 1] += subExpOper[subExpCountOper];
                                }

                                else if (subExpCountOper == 0)
                                {
                                    b = Double.Parse(subExpOper[0]);
                                    subExpOper.Clear();
                                }
                            }
                        }
                        if (ss[r_index] != "(" && ss[r_index] != ")")
                        {
                            subExpOper[subExpCountOper - 1] += ss[r_index] + " ";
                        }
                        r_index++;
                    }
                }
                else
                {
                    b = Double.Parse(ss[r_index]);
                }

                string resultFuncOper = CalcMath.CalculateFuncOper(ss[index], a, b).ToString();
                return (resultFuncOper, r_index);

            }

            if (CalcMath.FuncOperNames.Any(name => newInput.Contains(name)) && newInput.Length > 1)
            {
                string[] ss = newInput.Split(' ');

                Stack<string> funcOperStackFlat = new Stack<string>();
                //string subExp = "";
                List<String> subExpOper = new List<string>();
                int subExpCountOper = 0;
                List<int> parenthesisCountOper = new List<int>();

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
                    subExpCountOper++;
                    funcOperStackFlat.Push(ss[index]);
                    subExpOper.Add("");
                    parenthesisCountOper.Add(0);

                    l_index--;

                    while (subExpCountOper > 0)
                    {
                        if (ss[l_index] == ")")
                        {
                            //subExpCountFlat++;
                            //subExpFlat.Add("");
                            //parenthesisCountOper++;
                            parenthesisCountOper[subExpCountOper - 1]++;
                            subExpOper[subExpCountOper - 1] += " )";
                        }
                        if (ss[l_index] == "(")
                        {
                            //if (parenthesisCountOper > 0)
                            if (parenthesisCountOper[subExpCountOper - 1] > 0)
                            {
                                //parenthesisCountOper--;
                                parenthesisCountOper[subExpCountOper - 1]--;
                                subExpOper[subExpCountOper - 1] += " (";
                            }
                            else
                            {
                                subExpOper[subExpCountOper - 1] = new string(subExpOper[subExpCountOper - 1].Reverse().ToArray());
                                subExpOper[subExpCountOper - 1] = new Expression(subExpOper[subExpCountOper - 1]).Evaluate().ToString();
                                subExpCountOper--;

                                if (subExpCountOper > 0)
                                {
                                    subExpOper[subExpCountOper - 1] += subExpOper[subExpCountOper];
                                }

                                else if (subExpCountOper == 0)
                                {
                                    a = Double.Parse(subExpOper[0]);
                                    subExpOper.Clear();
                                }
                            }
                        }
                        if (ss[l_index] != "(" && ss[l_index] != ")")
                        {
                            subExpOper[subExpCountOper - 1] += " " + ss[l_index];
                        }
                        l_index--;
                    }
                }
                else
                {
                    a = Double.Parse(ss[l_index]);
                }
                if (ss[r_index] == "(")
                {
                    subExpCountOper++;
                    funcOperStackFlat.Push(ss[index]);
                    subExpOper.Add("");
                    parenthesisCountOper.Add(0);

                    r_index++;

                    while (subExpCountOper > 0)
                    {
                        if (CalcMath.isFuncOper(ss[r_index]))
                        {
                            var localResult = OperRecursion(r_index);

                            string[] tempS = subExpOper[subExpCountOper - 1].Split(' ');
                            if (tempS.Length > 1)
                                tempS[tempS.Length - 2] = localResult.Item1;
                            else
                                tempS[tempS.Length - 1] = localResult.Item1;
                            subExpOper[subExpCountOper - 1] = string.Join(" ", tempS);

                            r_index = localResult.Item2;
                            r_index++;
                        }

                        if (ss[r_index] == "(")
                        {
                            //subExpCountFlat++;
                            //subExpFlat.Add("");
                            //parenthesisCountOper++;
                            parenthesisCountOper[subExpCountOper - 1]++;
                            subExpOper[subExpCountOper - 1] += "( ";
                        }
                        if (ss[r_index] == ")")
                        {
                            //if (parenthesisCountOper > 0)
                            if (parenthesisCountOper[subExpCountOper - 1] > 0)
                            {
                                //parenthesisCountOper--;
                                parenthesisCountOper[subExpCountOper - 1]--;
                                subExpOper[subExpCountOper - 1] += ") ";
                            }
                            else
                            {
                                //subExpFlat[subExpCountFlat - 1] = new string(subExpFlat[subExpCountFlat - 1].Reverse().ToArray());
                                subExpOper[subExpCountOper - 1] = new Expression(subExpOper[subExpCountOper - 1]).Evaluate().ToString();
                                subExpCountOper--;

                                if (subExpCountOper > 0)
                                {
                                    subExpOper[subExpCountOper - 1] += subExpOper[subExpCountOper];
                                }

                                else if (subExpCountOper == 0)
                                {
                                    b = Double.Parse(subExpOper[0]);
                                    subExpOper.Clear();
                                }
                            }
                        }
                        if (ss[r_index] != "(" && ss[r_index] != ")")
                        {
                            subExpOper[subExpCountOper - 1] += ss[r_index] + " ";
                        }
                        r_index++;
                    }
                }
                else
                {
                    b = Double.Parse(ss[r_index]);
                }

                string resultFuncOper = CalcMath.CalculateFuncOper(ss[index], a, b).ToString();
                string fixedInput = "";
                for (int i = 0; i < ss.Length; i++)
                {
                    if (i < l_index || i > r_index)
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
