﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorForm
{
    public class RPN
    {
        //static private bool IsDelimeter(char c)
        //{
        //    if ((" =".IndexOf(c) != -1))
        //        return true;
        //    return false;
        //}

        //static private bool IsOperator(char с)
        //{
        //    if (("+-/*^()".IndexOf(с) != -1))
        //        return true;
        //    return false;
        //}

        static private bool IsDelimeter(string s)
        {
            if (s == "=" || s == " ") 
                return true;
            return false;
        }

        static private bool IsOperator(string s)
        {
            if ("+-/*^()".Contains(s))
                return true;
            return false;
        }

        static private bool IsFunction(string s)
        {
            if (CalcMath.isFunc(s))
                return true;
            return false;
        }

        static private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(": return 0;
                case ")": return 1;
                case "+": return 2;
                case "-": return 3;
                case "*": return 4;
                case "/": return 4;
                case "^": return 5;
                default: return 6;
            }
        }

        static public double Calculate(string input)
        {
            string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
            double result = Counting(output); //Решаем полученное выражение
            return result; //Возвращаем результат
        }

        static private string GetExpression(string input)
        {
            string output = string.Empty; //Строка для хранения выражения
            //Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов
            Stack<string> operStack = new Stack<string>(); //Стек для хранения операторов

            //for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            //{
            //    //Разделители пропускаем
            //    if (IsDelimeter(input[i]))
            //        continue; //Переходим к следующему символу

            //    //Если символ - цифра, то считываем все число
            //    if (Char.IsDigit(input[i])) //Если цифра
            //    {
            //        //Читаем до разделителя или оператора, что бы получить число
            //        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
            //        {
            //            output += input[i]; //Добавляем каждую цифру числа к нашей строке
            //            i++; //Переходим к следующему символу

            //            if (i == input.Length) break; //Если символ - последний, то выходим из цикла
            //        }

            //        output += " "; //Дописываем после числа пробел в строку с выражением
            //        i--; //Возвращаемся на один символ назад, к символу перед разделителем
            //    }

            //    //Если символ - оператор
            //    if (IsOperator(input[i])) //Если оператор
            //    {
            //        if (input[i] == '(') //Если символ - открывающая скобка
            //            operStack.Push(input[i]); //Записываем её в стек
            //        else if (input[i] == ')') //Если символ - закрывающая скобка
            //        {
            //            //Выписываем все операторы до открывающей скобки в строку
            //            char s = operStack.Pop();

            //            while (s != '(')
            //            {
            //                output += s.ToString() + ' ';
            //                s = operStack.Pop();
            //            }
            //        }
            //        else //Если любой другой оператор
            //        {
            //            if (operStack.Count > 0) //Если в стеке есть элементы
            //                if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
            //                    output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

            //            operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

            //        }
            //    }
            //}

            string[] inputArr = input.Split(' ');

            foreach (string s in inputArr)
            {
                //Разделители пропускаем
                if (IsDelimeter(s))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (s.All(c => Char.IsDigit(c))) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    if (!IsDelimeter(s) && !IsOperator(s))
                    {
                        output += s; //Добавляем каждую цифру числа к нашей строке
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                }

                //Если символ - оператор
                if (IsOperator(s)) //Если оператор
                {
                    if (s == "(") //Если символ - открывающая скобка
                        operStack.Push(s); //Записываем ее в стек
                    else if (s == ")") //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        string c = operStack.Pop();

                        while (c != "(")
                        {
                            output += c.ToString() + ' ';
                            c = operStack.Pop();
                        }
                    }

                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(s) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(s); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }

                if (IsFunction(s))
                {

                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        static private double Counting(string input)
        {
            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); //Временный стек для решения

            //for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            //{
            //    //Если символ - цифра, то читаем все число и записываем на вершину стека
            //    if (Char.IsDigit(input[i]))
            //    {
            //        string a = string.Empty;

            //        while (!IsDelimeter(input[i].ToString()) && !IsOperator(input[i].ToString())) //Пока не разделитель
            //        {
            //            a += input[i]; //Добавляем
            //            i++;
            //            if (i == input.Length) break;
            //        }
            //        temp.Push(double.Parse(a)); //Записываем в стек
            //        i--;
            //    }
            //    else if (IsOperator(input[i].ToString())) //Если символ - оператор
            //    {
            //        //Берем два последних значения из стека
            //        double a = temp.Pop();
            //        double b = temp.Pop();

            //        switch (input[i]) //И производим над ними действие, согласно оператору
            //        {
            //            case '+': result = b + a; break;
            //            case '-': result = b - a; break;
            //            case '*': result = b * a; break;
            //            case '/': result = b / a; break;
            //            case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
            //        }
            //        temp.Push(result); //Результат вычисления записываем обратно в стек
            //    }
            //}

            string[] inputArr = input.Remove(input.Length - 1).Split(' ');
            
            foreach (string s in inputArr) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (s.All(c => Char.IsDigit(c)))
                {
                    string a = string.Empty;

                    if (!IsDelimeter(s) && !IsOperator(s)) //Пока не разделитель
                    {
                        a += s; //Добавляем
                    }
                    temp.Push(double.Parse(a)); //Записываем в стек
                }
                else if (IsOperator(s)) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (s) //И производим над ними действие, согласно оператору
                    {
                        case "+": result = b + a; break;
                        case "-": result = b - a; break;
                        case "*": result = b * a; break;
                        case "/": result = b / a; break;
                        case "^": result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;

                        default: result = CalcMath.Calculate(s, a); temp.Push(b); break;
                    }
                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
    }
}
