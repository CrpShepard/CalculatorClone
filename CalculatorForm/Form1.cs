using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorForm
{
    public partial class Form1 : Form
    {
        StateMonitor stateMonitor = new StateMonitor();

        public static ListBox listBoxGlobal { get; set; }

        History historyForm = new History();

        bool secondFunc;
        bool rad;

        public Form1()
        {
            InitializeComponent();
            stateMonitor.ExpressionChanged += HandleExpressionChange;
            stateMonitor.ClearEntryChanged += HandleClearEntryChange;

            stateMonitor.MonitoredClearEntry = false;

            listBoxGlobal = listBox1;
            History.stateMonitor = stateMonitor;
            secondFunc = false;
            rad = true;
        }

        //string oldExpression = string.Empty;

        void HandleExpressionChange(string str)
        {
            //oldExpression = str;
            label1.Text = str;

            if (str.Length > 1 || (str.Length > 0 && Char.IsDigit(str[str.Length - 1])))
            {
                if (str[str.Length - 1] == '=')
                    stateMonitor.MonitoredClearEntry = false;
                else if (str.Length > 1 && Char.IsDigit(str[str.Length - 2]))
                {
                    stateMonitor.MonitoredClearEntry = true;

                    if (str.Length > 3 && Char.IsDigit(str[str.Length - 4]) && str[str.Length - 3] == ' ')
                    {
                        stateMonitor.MonitoredString = stateMonitor.MonitoredString.Remove(str.Length - 3, 1);
                    }
                }
                if (CalcMath.FuncNames.Any(s => str.EndsWith(s)))
                {
                    string func = str.Split(' ')[str.Split(' ').Length - 1];
                    str = str.Remove(str.Length - 1 - func.Length);

                    if (str.Split(' ')[str.Split(' ').Length - 1] == ")")
                    {
                        int index = str.Length - 2;
                        int parenthesisCount = 1;
                        bool subFunc = false;
                        while (parenthesisCount > 0)
                        {
                            if (str[index] == '(')
                            {
                                parenthesisCount--;
                                if (index > 0)
                                if (Char.IsLetter(str[index - 1]))
                                {
                                    subFunc = true;
                                    while (Char.IsLetter(str[index - 1]) && index > 1)
                                        index--;
                                }
                            }
                            else if (str[index] == ')')
                                parenthesisCount++;
                            
                            index--;
                        }

                        if (!subFunc || str[0] == '(')
                        {
                            index++;
                            str = str.Insert(index, func) + " ";
                        }
                        else
                        {
                            if (index > 0)
                            if (str[index - 1] != ' ')
                                index++;
                            str = str.Insert(index, func + "( ") + " ) ";
                        }
                        stateMonitor.MonitoredString = str;
                    }
                    else
                    {
                        int lastNumberIndex = str.LastIndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                        if (lastNumberIndex != -1)
                        {
                            int index = lastNumberIndex;

                            while (index > 0)
                            {
                                if (!Char.IsDigit(str[index]) && str[index] != '.')
                                {
                                    index++;
                                    break;
                                }
                                index--;
                            }
                            
                            // Вставляем "мат функцию(" перед последним числом и закрывающую скобку ")"
                            str = str.Insert(index, func + "( ") + " ) ";
                            stateMonitor.MonitoredString = str;
                        }
                    }
                }
                else if (str.EndsWith(".") && !Char.IsDigit(str[str.Length - 2]))
                {
                    str = str.Remove(str.Length - 2);
                    int lastNumberIndex = str.LastIndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                    if (lastNumberIndex != -1)
                    {
                        str = str.Insert(lastNumberIndex + 1, ".");
                        stateMonitor.MonitoredString = str;
                    }
                }

                if (str.EndsWith("%"))
                {
                    str = str.Remove(str.Length - 2);

                    string[] s = str.Split(' ');

                    if (s[s.Length - 2] == "+" || s[s.Length - 2] == "-")
                    {
                        s[s.Length - 1] = (Double.Parse(s[s.Length - 3]) * Double.Parse(s[s.Length - 1]) / 100).ToString();
                    }
                    else if (s[s.Length - 2] == "*" || s[s.Length - 2] == "/")
                    {
                        s[s.Length - 1] = (Double.Parse(s[s.Length - 1]) / 100).ToString();
                    }

                    stateMonitor.MonitoredString = string.Join(" ", s);
                }

                else if (str[str.Length - 2] == ' ' && str[str.Length - 1] == ' ')
                    stateMonitor.MonitoredString = stateMonitor.MonitoredString.Remove(str.Length - 1);
                else
                    stateMonitor.MonitoredClearEntry = false;
            }
            else
                stateMonitor.MonitoredClearEntry = false;
        }

        void HandleClearEntryChange()
        {
            if (stateMonitor.MonitoredClearEntry)
                button13.Text = "CE";
            else
                button13.Text = "C";
        }

        private void button42_Click(object sender, EventArgs e) // кнопка 0
        {
            stateMonitor.MonitoredString += "0 ";
        }

        private void button38_Click(object sender, EventArgs e) // кнопка 1
        {
            stateMonitor.MonitoredString += "1 ";
        }

        private void button37_Click(object sender, EventArgs e) // кнопка 2
        {
            stateMonitor.MonitoredString += "2 ";
        }

        private void button36_Click(object sender, EventArgs e) // кнопка 3
        {
            stateMonitor.MonitoredString += "3 ";
        }

        private void button33_Click(object sender, EventArgs e) // кнопка 4
        {
            stateMonitor.MonitoredString += "4 ";
        }

        private void button32_Click(object sender, EventArgs e) // кнопка 5
        {
            stateMonitor.MonitoredString += "5 ";
        }

        private void button31_Click(object sender, EventArgs e) // кнопка 6
        {
            stateMonitor.MonitoredString += "6 ";
        }

        private void button28_Click(object sender, EventArgs e) // кнопка 7
        {
            stateMonitor.MonitoredString += "7 ";
        }

        private void button27_Click(object sender, EventArgs e) // кнопка 8
        {
            stateMonitor.MonitoredString += "8 ";
        }

        private void button26_Click(object sender, EventArgs e) // кнопка 9
        {
            stateMonitor.MonitoredString += "9 ";
        }

        private void button23_Click(object sender, EventArgs e) // кнопка (
        {
            stateMonitor.MonitoredString += "( ";
        }

        private void button22_Click(object sender, EventArgs e) // кнопка )
        {
            stateMonitor.MonitoredString += ") ";
        }

        private void button35_Click(object sender, EventArgs e) // кнопка +
        {
            stateMonitor.MonitoredString += "+ ";
        }

        private void button30_Click(object sender, EventArgs e) // кнопка -
        {
            stateMonitor.MonitoredString += "- ";
        }

        private void button25_Click(object sender, EventArgs e) // кнопка *
        {
            stateMonitor.MonitoredString += "* ";
        }

        private void button20_Click(object sender, EventArgs e) // кнопка /
        {
            stateMonitor.MonitoredString += "/ ";
        }

        private void button29_Click(object sender, EventArgs e) // кнопка ^ | x^y
        {
            if (!secondFunc)
                stateMonitor.MonitoredString += "rank ";
            else
                stateMonitor.MonitoredString += "yroot ";
        }

        private void button41_Click(object sender, EventArgs e) // . дробь
        {
            stateMonitor.MonitoredString += ".";
        }

        private void button40_Click(object sender, EventArgs e) // кнопка =
        {
            stateMonitor.MonitoredString += "=";

            //label2.Text = RPN.Calculate(stateMonitor.MonitoredString).ToString();
            label2.Text = Calculate.getResult(stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1), rad).ToString();

            if (label4.Text.Contains("="))
            {
                foreach (var item in listBox1.Items)
                {
                    if (item.ToString()[0] == label4.Text[0])
                    {
                        if (radioButton1.Checked)
                            listBox1.Items[listBox1.Items.IndexOf(item)] = label4.Text + label2.Text;
                        if (radioButton2.Checked)
                            listBox1.Items[listBox1.Items.IndexOf(item)] = label4.Text + stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1);
                        listBoxGlobal = listBox1;
                        return;
                    }
                }
                if (radioButton1.Checked)
                    listBox1.Items.Add(label4.Text + label2.Text);
                if (radioButton2.Checked)
                    listBox1.Items.Add(label4.Text + stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1));
                listBoxGlobal = listBox1;
            }
            else
            {
                historyForm.updateHistory(stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1));
            }
        }

        private void button13_Click(object sender, EventArgs e) // кнопка C | CE
        {
            if (!stateMonitor.MonitoredClearEntry)
                stateMonitor.MonitoredString = String.Empty;
            else
            {
                while(stateMonitor.MonitoredClearEntry)
                {
                    stateMonitor.MonitoredString = stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1);
                    stateMonitor.MonitoredString = stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1);
                    stateMonitor.MonitoredString += " ";
                }
            }
        }

        private void button14_Click(object sender, EventArgs e) // кнопка Erase
        {
            if (stateMonitor.MonitoredClearEntry)
            {
                stateMonitor.MonitoredString = stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1);
                stateMonitor.MonitoredString = stateMonitor.MonitoredString.Remove(stateMonitor.MonitoredString.Length - 1);
                stateMonitor.MonitoredString += " ";
            }
        }

        // кнопка журнала истории 
        private void button1_Click(object sender, EventArgs e) // часы
        {
            historyForm.Show();
        }

        private void button5_Click(object sender, EventArgs e) // экспорт переменных
        {
            string filePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var item in listBox1.Items)
                {
                    sw.WriteLine(item.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e) // импорт переменных
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                listBox1.Items.Clear();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                }
            }
        }

        // VVV переменные VVV

        public string GetVarExpression(string s)
        {
            foreach (var item in listBox1.Items)
            {
                if (item.ToString()[0] == s[0])
                {
                    return item.ToString().Remove(0, 2);
                }
            }
            
            return null;
        }

        void VarHandle(string varLiteral)
        {
            if (checkBox2.Checked)
            {
                label4.Text = varLiteral;
            }
            else if (!checkBox1.Checked)
            {
                label3.Text = varLiteral;
                var s = GetVarExpression(varLiteral);

                if (s != null)
                {
                    textBox1.Text = s;
                }
            }
            
            else if (checkBox1.Checked)
            {
                var s = GetVarExpression(varLiteral);

                if (s != null)
                {
                    //stateMonitor.MonitoredString += s + " ";
                    stateMonitor.MonitoredString += varLiteral[0] + " ";
                }

            }
        }

        private void button4_Click(object sender, EventArgs e) // VC
        {
            label4.Text = "";
        }

        private void button68_Click(object sender, EventArgs e) // a
        {
            VarHandle("a=");
        }

        private void button67_Click(object sender, EventArgs e) // b
        {
            VarHandle("b=");
        }

        private void button66_Click(object sender, EventArgs e) // c
        {
            VarHandle("c=");
        }

        private void button65_Click(object sender, EventArgs e) // d
        {
            VarHandle("d=");
        }

        private void button64_Click(object sender, EventArgs e) // e
        {
            VarHandle("e=");
        }

        private void button63_Click(object sender, EventArgs e) // f
        {
            VarHandle("f=");
        }

        private void button62_Click(object sender, EventArgs e) // g
        {
            VarHandle("g=");
        }

        private void button61_Click(object sender, EventArgs e) // h
        {
            VarHandle("h=");
        }

        private void button60_Click(object sender, EventArgs e) // i
        {
            VarHandle("i=");
        }

        private void button59_Click(object sender, EventArgs e) // j
        {
            VarHandle("j=");
        }

        private void button58_Click(object sender, EventArgs e) // k
        {
            VarHandle("k=");
        }

        private void button57_Click(object sender, EventArgs e) // l
        {
            VarHandle("l=");
        }

        private void button56_Click(object sender, EventArgs e) // m
        {
            VarHandle("m=");
        }

        private void button55_Click(object sender, EventArgs e) // n
        {
            VarHandle("n=");
        }

        private void button54_Click(object sender, EventArgs e) // o
        {
            VarHandle("o=");
        }

        private void button53_Click(object sender, EventArgs e) // p
        {
            VarHandle("p=");
        }

        private void button52_Click(object sender, EventArgs e) // q
        {
            VarHandle("q=");
        }

        private void button51_Click(object sender, EventArgs e) // r
        {
            VarHandle("r=");
        }

        private void button50_Click(object sender, EventArgs e) // s
        {
            VarHandle("s=");
        }

        private void button49_Click(object sender, EventArgs e) // t
        {
            VarHandle("t=");
        }

        private void button48_Click(object sender, EventArgs e) // u
        {
            VarHandle("u=");
        }

        private void button47_Click(object sender, EventArgs e) // v
        {
            VarHandle("v=");
        }

        private void button46_Click(object sender, EventArgs e) // w
        {
            VarHandle("w=");
        }

        private void button45_Click(object sender, EventArgs e) // x
        {
            VarHandle("x=");
        }

        private void button70_Click(object sender, EventArgs e) // y
        {
            VarHandle("y=");
        }

        private void button69_Click(object sender, EventArgs e) // z
        {
            VarHandle("z=");
        }

        private void button71_Click(object sender, EventArgs e) // задать
        {
            if (label3.Text.Contains('=') && textBox1.Text.Length > 0 && !checkBox1.Checked)
            {
                foreach (var item in listBox1.Items)
                {
                    if (item.ToString()[0] == label3.Text[0])
                    {
                        listBox1.Items[listBox1.Items.IndexOf(item)] = label3.Text + textBox1.Text;
                        listBoxGlobal = listBox1;
                        return;
                    }
                }
                listBox1.Items.Add(label3.Text + textBox1.Text);
                listBoxGlobal = listBox1;
            }
        }

        private void button72_Click(object sender, EventArgs e) // очистить
        {
            listBox1.Items.Clear();
            textBox1.Text = string.Empty;
            listBoxGlobal = listBox1;
        }

        // VVV мат функции VVV

        private void button43_Click(object sender, EventArgs e) // +/- (отриц знач)
        {
            stateMonitor.MonitoredString += "negate";
        }

        private void button10_Click(object sender, EventArgs e) // 2nd
        {
            secondFunc = !secondFunc;

            if (!secondFunc)
            {
                button10.BackColor = Color.White;

                button19.Text = "x^2";
                button24.Text = "x^(1/2)";
                button29.Text = "x^y";
                button34.Text = "10^x";
                button39.Text = "log";
                button44.Text = "ln";
            }
            else
            {
                button10.BackColor = Color.RoyalBlue;

                button19.Text = "x^3";
                button24.Text = "x^(1/3)";
                button29.Text = "x^(1/y)";
                button34.Text = "2^x";
                button39.Text = "LOGyX";
                button44.Text = "e^x";
            }
        }

        private void button19_Click(object sender, EventArgs e) // sqr (^2)
        {
            if (!secondFunc)
                stateMonitor.MonitoredString += "sqr";
            else
                stateMonitor.MonitoredString += "cube";
        }

        private void button24_Click(object sender, EventArgs e) // sqrt
        {
            if (!secondFunc)
                stateMonitor.MonitoredString += "sqrt";
            else
                stateMonitor.MonitoredString += "cuberoot";
        }

        private void button34_Click(object sender, EventArgs e) // 10^x | ten
        {
            if (!secondFunc)
                stateMonitor.MonitoredString += "ten";
            else
                stateMonitor.MonitoredString += "two";
        }

        private void button39_Click(object sender, EventArgs e) // log
        {
            if (!secondFunc)
                stateMonitor.MonitoredString += "log";
            else
                stateMonitor.MonitoredString += "logbase ";
        }

        private void button44_Click(object sender, EventArgs e) // ln
        {
            if (!secondFunc)
                stateMonitor.MonitoredString += "ln";
            else
                stateMonitor.MonitoredString += "exp";
        }

        private void button11_Click(object sender, EventArgs e) // pi
        {
            stateMonitor.MonitoredString += Math.PI.ToString() + " ";
        }

        private void button12_Click(object sender, EventArgs e) // e
        {
            stateMonitor.MonitoredString += Math.E.ToString() + " ";
        }

        private void button18_Click(object sender, EventArgs e) // 1/x | onediv
        {
            stateMonitor.MonitoredString += "onediv";
        }

        private void button17_Click(object sender, EventArgs e) // abs
        {
            stateMonitor.MonitoredString += "abs";
        }

        private void button21_Click(object sender, EventArgs e) // fact
        {
            stateMonitor.MonitoredString += "fact";
        }

        // функциональные операторы

        private void button15_Click(object sender, EventArgs e) // mod
        {
            stateMonitor.MonitoredString += "mod ";
        }


        // % процент
        private void button3_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "%";
        }

        // тригонометрия
        private void sinToolStripMenuItem_Click(object sender, EventArgs e) // sin
        {
            stateMonitor.MonitoredString += "sin";
        }

        private void button2_Click(object sender, EventArgs e) // DEG | RAD
        {
            if (rad) 
            {
                button2.Text = "DEG";
                rad = !rad;
            }
            else
            {
                button2.Text = "RAD";
                rad = !rad;
            }
        }

        private void cosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "cos";
        }

        private void tanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "tan";
        }

        private void secToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "sec";
        }

        private void cscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "csc";
        }

        private void cotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "cot";
        }

        private void asinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "asin";
        }

        private void acosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "acos";
        }

        private void atanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "atan";
        }

        private void asecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "asec";
        }

        private void acscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "acsc";
        }

        private void acotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "acot";
        }

        private void sinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "sinh";
        }

        private void coshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "cosh";
        }

        private void tanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "tanh";
        }

        private void sechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "sech";
        }

        private void cschToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "csch";
        }

        private void cothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "coth";
        }

        private void asinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "asinh";
        }

        private void acoshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "acosh";
        }

        private void atanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "atanh";
        }

        private void asechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "asech";
        }

        private void acschToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "acsch";
        }

        private void acothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "acoth";
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "abs";
        }

        private void xToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "ceil";
        }

        private void xToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "floor";
        }

        private void randToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            stateMonitor.MonitoredString += rnd.NextDouble().ToString() + " ";
        }

        private void dmsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "dms";
        }

        private void degToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stateMonitor.MonitoredString += "degrees";
        }
    }
}
