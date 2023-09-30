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
        RPN rpn = new RPN();
        StringMonitor stringMonitor = new StringMonitor();

        public Form1()
        {
            InitializeComponent();
            stringMonitor.ExpressionChanged += HandleExpressionChange;
        }

        //string oldExpression = string.Empty;

        void HandleExpressionChange(string str)
        {
            //oldExpression = str;
            label1.Text = str;
        }

        private void button42_Click(object sender, EventArgs e) // кнопка 0
        {
            stringMonitor.MonitoredString += "0 ";
        }

        private void button38_Click(object sender, EventArgs e) // кнопка 1
        {
            stringMonitor.MonitoredString += "1 ";
        }

        private void button37_Click(object sender, EventArgs e) // кнопка 2
        {
            stringMonitor.MonitoredString += "2 ";
        }

        private void button36_Click(object sender, EventArgs e) // кнопка 3
        {
            stringMonitor.MonitoredString += "3 ";
        }

        private void button33_Click(object sender, EventArgs e) // кнопка 4
        {
            stringMonitor.MonitoredString += "4 ";
        }

        private void button32_Click(object sender, EventArgs e) // кнопка 5
        {
            stringMonitor.MonitoredString += "5 ";
        }

        private void button31_Click(object sender, EventArgs e) // кнопка 6
        {
            stringMonitor.MonitoredString += "6 ";
        }

        private void button28_Click(object sender, EventArgs e) // кнопка 7
        {
            stringMonitor.MonitoredString += "7 ";
        }

        private void button27_Click(object sender, EventArgs e) // кнопка 8
        {
            stringMonitor.MonitoredString += "8 ";
        }

        private void button26_Click(object sender, EventArgs e) // кнопка 9
        {
            stringMonitor.MonitoredString += "9 ";
        }

        private void button23_Click(object sender, EventArgs e) // кнопка (
        {
            stringMonitor.MonitoredString += "( ";
        }

        private void button22_Click(object sender, EventArgs e) // кнопка )
        {
            stringMonitor.MonitoredString += ") ";
        }

        private void button35_Click(object sender, EventArgs e) // кнопка +
        {
            stringMonitor.MonitoredString += "+ ";
        }

        private void button30_Click(object sender, EventArgs e) // кнопка -
        {
            stringMonitor.MonitoredString += "- ";
        }

        private void button25_Click(object sender, EventArgs e) // кнопка *
        {
            stringMonitor.MonitoredString += "* ";
        }

        private void button20_Click(object sender, EventArgs e) // кнопка /
        {
            stringMonitor.MonitoredString += "/ ";
        }

        private void button29_Click(object sender, EventArgs e) // кнопка ^ | x^y
        {
            stringMonitor.MonitoredString += "^ ";
        }

        private void button40_Click(object sender, EventArgs e) // кнопка =
        {
            stringMonitor.MonitoredString += "=";

            label2.Text = rpn.Calculate(stringMonitor.MonitoredString).ToString();
        }
    }
}
