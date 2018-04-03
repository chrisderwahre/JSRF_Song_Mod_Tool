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

    public partial class Form1 : Form
    {
        public void songChangingFunc(string songName) // Song Changing Function
        {
            if (!File.Exists(textBox1.Text + "/" + songName + ".adx")) // Checks if the Folder is Correct
            {
                MessageBox.Show("Wrong Sound Media Folder: Couldn't find Original File to Replace", "Wrong Sound Media Folder");
                return;
            }


            OpenFileDialog ofd = new OpenFileDialog(); // OpenFileDialog
            ofd.Filter = "ADX Files | *.adx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                File.Delete(textBox1.Text + "/" + songName + ".adx"); // Deleting File
                File.Copy(ofd.FileName, textBox1.Text + "/" + songName + ".adx"); // Copy File

                MessageBox.Show("Successfully replaced the file!", "Replaced Successful!");
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JSRF Song Mod Tool by ChrisderWahre 2018", "JSRF Song Mod Tool"); // About Button
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to use:\n1. Select Your JSRF Sound Folder (Media/Z_ADX/BGM)\n2. Pick the Song to Replace\n3.Click the Button and Select the audio file of the new File\n4. FTP the BGM Folder to your Installed Game on the XBOX and Done!", "Help"); // Tutorial Button
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Text == "") // Checks if a song is selected
            {
                MessageBox.Show("No Song is Selected please Select a song to Continue","No Song Selected");
            }
            if (textBox1.Text == "") // Checks if the Game files are selected
            {
                MessageBox.Show("No Sound Media Files Selected!", "No Sound Files!");
            }

            if (listBox1.Text != "" || textBox1.Text != "")         // Checks if the text and list Box are'nt Empty
            {
                if (listBox1.Text == "Aisle 10")
                {
                     songChangingFunc("aisle10");
                }

                if (listBox1.Text == "The Answer")
                {
                    songChangingFunc("answer");
                }

                if (listBox1.Text == "Baby-T")
                {
                    songChangingFunc("baby_t");
                }

                if (listBox1.Text == "Birthday Cake")
                {
                    songChangingFunc("birthday");
                }

                if (listBox1.Text == "Bokfresh")
                {
                    songChangingFunc("bokfresh");
                }

                if (listBox1.Text == "Latch Brother Bounce")
                {
                    songChangingFunc("bounce");
                }

                if (listBox1.Text == "Fly Like a Butterfly")
                {
                    songChangingFunc("buttrfly");
                }

                if (listBox1.Text == "The Concept of Love")
                {
                    songChangingFunc("concept");
                }

                if (listBox1.Text == "Funky Dealer")
                {
                    songChangingFunc("dealer");
                }

                if (listBox1.Text == "Shape Da Future")
                {
                    songChangingFunc("future");
                }

                if (listBox1.Text == "Statement of Intent")
                {
                    songChangingFunc("intent");
                }

                if (listBox1.Text == "Koto Stomp")
                {
                    songChangingFunc("koto");
                }

                if (listBox1.Text == "Count Latchula")
                {
                    songChangingFunc("latchula");
                }

                if (listBox1.Text == "Let Mom Sleep (No Sleep Remix)")
                {
                    songChangingFunc("letmom");
                }

                if (listBox1.Text == "I Love Love You")
                {
                    songChangingFunc("lovelove");
                }

                if (listBox1.Text == "Rockin' da Mic (The Latch Bros Remix)")
                {
                    songChangingFunc("mic");
                }

                if (listBox1.Text == "I'm Not a Model")
                {
                    songChangingFunc("model");
                }

                if (listBox1.Text == "Oldies But Happies")
                {
                    songChangingFunc("oldies");
                }

                if (listBox1.Text == "Me Likey the Poom Poom")
                {
                    songChangingFunc("poompoom");
                }

                if (listBox1.Text == "Rock it On (D.S. Mix)")
                {
                    songChangingFunc("rockiton");
                }

                if (listBox1.Text == "Humming the Bassline (D.S. Remix)")
                {
                    songChangingFunc("g_park");
                }

                if (listBox1.Text == "The Scrappy (The Latch Bros Remix)")
                {
                    songChangingFunc("scrappy");
                }

                if (listBox1.Text == "Sneakman (Toronto Mix)")
                {
                    songChangingFunc("sneakman");
                }

                if (listBox1.Text == "Ill Victory Beat")
                {
                    songChangingFunc("victory");
                }

                if (listBox1.Text == "What About The Future")
                {
                    songChangingFunc("whatbout");
                }

                if (listBox1.Text == "Teknopathetic")
                {
                    songChangingFunc("pathetic");
                }

                if (listBox1.Text == "Like It Like This Like That")
                {
                    songChangingFunc("likeit");
                }

                if (listBox1.Text == "Sweet Soul Brother (B.B. Rights Mix)")
                {
                    songChangingFunc("sweet");
                }

                if (listBox1.Text == "That's Enough (B.B. Rights Mix)")
                {
                    songChangingFunc("thats");
                }

                if (listBox1.Text == "Grace And Glory")
                {
                    songChangingFunc("grace");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;               // Sets the Selected Folder to the BGM Folder
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
