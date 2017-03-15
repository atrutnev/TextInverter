using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextInverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string userString = richTextBox1.Text;
            richTextBox2.Clear();
            if (radioButton1.Checked)
            {
                richTextBox2.Text = ReverseSimple(userString);
            }
            else if (radioButton2.Checked)
            {
                richTextBox2.Text = ReverseLinq(userString);
            }
            else if (radioButton3.Checked)
            {
                richTextBox2.Text = ReverseArrayManual(userString);
            }
            else if (radioButton4.Checked)
            {
                richTextBox2.Text = ReverseManualHalf(userString);
            }
            else if (radioButton5.Checked)
            {
                richTextBox2.Text = ReverseUnsafeCopy(userString);
            }
            else if (radioButton6.Checked)
            {
                richTextBox2.Text = ReverseStringBuilder(userString);
            }
            else
            {
                stopWatch.Stop();
                MessageBox.Show("Не выбран вариант алгоритма!");
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            label1.Text = "Время выполнения: " + elapsedTime;

        }

        static private string ReverseSimple(string s)
        {
            if (s.Length <= 1) return s;
            string copy = "";
            for (int i = s.Length - 1; i >= 0; i--)
            {
                copy += s[i];
            }
            return copy;
        }

        static private string ReverseLinq(string s)
        {
            return new string(s.Reverse().ToArray());
        }

        static string ReverseArrayManual(string s)
        {
            char[] reversedCharArray = new char[s.Length];
            for (int i = s.Length - 1; i >= 0; i--)
            {
                reversedCharArray[s.Length - i - 1] = s[i];
            }
            return new string(reversedCharArray);
        }

        static string ReverseManualHalf(string s)
        {
            char[] reversedCharArray = new char[s.Length];
            int i = 0;
            int j = s.Length - 1;
            while (i <= j)
            {
                reversedCharArray[i] = s[j];
                reversedCharArray[j] = s[i];
                i++;
                j--;
            }
            return new string(reversedCharArray);
        }

        static unsafe string ReverseUnsafeCopy(string s)
        {
            if (s.Length <= 1) return s;
            char tmp;
            string copy = string.Copy(s);
            fixed (char* buf = copy)
            {
                char* p = buf;
                char* q = buf + s.Length - 1;
                while (p < q)
                {
                    tmp = *p;
                    *p = *q;
                    *q = tmp;
                    p++; q--;
                }
            }
            return copy;
        }

        static private string ReverseStringBuilder(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = s.Length; i-- != 0;)
            {
                sb.Append(s[i]);
            }
            return sb.ToString();
        }
    }
}
