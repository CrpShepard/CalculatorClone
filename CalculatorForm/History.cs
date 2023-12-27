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
    public partial class History : Form
    {
        public static StateMonitor stateMonitor { get; set; }

        public History()
        {
            InitializeComponent();
        }

        public void updateHistory(string s)
        {
            this.listBox1.Items.Add(s);
        }

        private void button3_Click(object sender, EventArgs e) // очистить
        {
            listBox1.Items.Clear();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) // двойнок клик по элементу
        {
            stateMonitor.MonitoredString = listBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e) // экспорт
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

        private void button2_Click(object sender, EventArgs e) // импорт
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
    }
}
