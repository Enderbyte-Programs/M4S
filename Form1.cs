using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMacro4Skaterva
{
    public partial class Form1 : Form
    {
        public string torun;
        public Thread activethread;
        public int secondstosleep;
        public Form1()
        {
            InitializeComponent();
        }
        private void ShutUp()
        {
            try
            {
                activethread.Abort();
            } catch
            {

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            button2.BackColor = Color.LightGreen;
        }

        private void RunThread()
        {
            SendKeys.Flush();
            //TODO Run macro
            while (true)
            {
                Thread.Sleep(secondstosleep * 1000);
                SendKeys.SendWait(torun);
                SendKeys.Flush();
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            torun = textBox1.Text;
            if (string.IsNullOrEmpty(torun))
            {
                MessageBox.Show("Please type what you want to run before you start.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                if (numericUpDown1.Value <= 0)
                {
                    MessageBox.Show("Invalid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    if (comboBox1.SelectedItem == "seconds")
                    {
                        secondstosleep = (int)numericUpDown1.Value;
                    } else if (comboBox1.SelectedItem == "minutes")
                    {
                        secondstosleep = (int)numericUpDown1.Value * 60;
                    } else if (comboBox1.SelectedItem == "hours")
                    {
                        secondstosleep = (int)numericUpDown1.Value * 3600;
                    }

                    activethread = new Thread(new ThreadStart(RunThread));
                    activethread.Start();
                    button2.BackColor = Color.White;
                    button2.Enabled = false;
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShutUp();
            Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutUp();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
                ShutUp();
            button2.BackColor = Color.LightGreen;
            button2.Enabled = true;
        }
    }
}
