using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorForm
{
    public partial class Form1 : Form
    {
        StateMonitor stateMonitor = new StateMonitor();

        public Form1()
        {
            InitializeComponent();
            stateMonitor.ExpressionChanged += HandleExpressionChange;
            stateMonitor.ClearEntryChanged += HandleClearEntryChange;

            stateMonitor.MonitoredClearEntry = false;
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
            stateMonitor.MonitoredString += "^ ";
        }

        private void button40_Click(object sender, EventArgs e) // кнопка =
        {
            stateMonitor.MonitoredString += "=";

            label2.Text = RPN.Calculate(stateMonitor.MonitoredString).ToString();
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
    }
}
