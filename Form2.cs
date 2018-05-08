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

namespace JSRF_Song_Mod_Tool
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void addLineToDebugLog(string text)
        {
            string[] tempArray = new string[this.richTextBox1.Lines.Length];
            string[] tempArray2 = new string[this.richTextBox1.Lines.Length + 1];
            tempArray = this.richTextBox1.Lines;

            int count = 0;
            tempArray2[count] = tempArray[0];
            count++;
            tempArray2[count] = text;
            count++;
            for (int counter = 2; counter < tempArray.Length; counter++)
            {
                tempArray2[counter] = tempArray[counter];
            }

            string strfn;
            strfn = Convert.ToString(DateTime.Now.ToFileTime());
            StreamWriter fs = new StreamWriter(strfn, true);
            foreach (string s in tempArray2)
                fs.WriteLine(s);
            fs.Flush();
            fs.Close();
            string line;
            this.richTextBox1.Clear();
            System.IO.StreamReader file = new System.IO.StreamReader(strfn);
            while ((line = file.ReadLine()) != null)
            {
                this.richTextBox1.AppendText("[" + DateTime.Now + "] " +line + "\n");
            }
        }
    }
}
