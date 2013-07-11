using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MartingaleCalculateBets
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("options.txt"))
            {
                checkedListBox1.Enabled = false;
                return;
            }
            using (FileStream fs = File.OpenRead("options.txt"))
            {
                using (StreamReader file = new StreamReader(fs))
                {
                    string[] lines = file.ReadToEnd().Split("\n".ToCharArray());
                    label2.Text = lines[0].Trim();
                    label4.Text = lines[1].Trim();
                    label6.Text = lines[2].Trim();
                    numericUpDown1.Value = decimal.Parse(lines[3].Trim());
                    label9.Text = lines[4].Trim();
                    label15.Text = lines[5].Trim();
                    numericUpDown3.Value = decimal.Parse(lines[6].Trim());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkedListBox1.Enabled)
            {
                label10.Text = ((numericUpDown1.Value) / ((numericUpDown2.Value - 1) - (numericUpDown2.Value - 1) * numericUpDown3.Value / 100.0m)).ToString("F");//numericUpDown1.Value.ToString("F");
                label9.Text = label10.Text;
                label15.Text = numericUpDown2.Value.ToString();
                checkedListBox1.Enabled = true;
                return;
            }
            if (checkedListBox1.CheckedIndices.Count == 0)
            {
                MessageBox.Show("Выберите проиграла последняя ставка или выиграла");
                return;
            }
            if (!checkedListBox1.Enabled || checkedListBox1.CheckedIndices[0] == 0)
            {
                label10.Text = ((numericUpDown1.Value) / ((numericUpDown2.Value - 1) - (numericUpDown2.Value - 1) * numericUpDown3.Value / 100.0m)).ToString("F");
                label9.Text = label10.Text;
                label15.Text = numericUpDown2.Value.ToString("F");
                label6.Text = "0";
                label2.Text = "0";
                return;
            }
            label2.Text = (int.Parse(label2.Text) + 1).ToString();
            if (int.Parse(label2.Text) > int.Parse(label4.Text))
            {
                label4.Text = label2.Text;
            }
            label6.Text = (decimal.Parse(label6.Text) + decimal.Parse(label9.Text)).ToString("F");
            label10.Text = ((numericUpDown1.Value + decimal.Parse(label6.Text)) / ((numericUpDown2.Value - 1) - (numericUpDown2.Value - 1) * numericUpDown3.Value / 100.0m)).ToString("F");
            label9.Text = label10.Text;
            label15.Text = numericUpDown2.Value.ToString("F");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FileStream fs = File.OpenWrite("options.txt"))
            {
                using (StreamWriter file = new StreamWriter(fs))
                {
                    file.WriteLine(label2.Text);
                    file.WriteLine(label4.Text);
                    file.WriteLine(label6.Text);
                    file.WriteLine(numericUpDown1.Value.ToString());
                    file.WriteLine(label9.Text);
                    file.WriteLine(label15.Text);
                    file.WriteLine(numericUpDown3.Value.ToString());
                }
            }
        }
    }
}
