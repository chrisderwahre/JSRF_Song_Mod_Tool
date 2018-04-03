using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            ofd.Filter = "Supported Sound Files|*.wav;*.adx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string path = Directory.GetCurrentDirectory();

                if (".wav".Equals(Path.GetExtension(ofd.FileName), StringComparison.OrdinalIgnoreCase))
                {
                    // the path to the wav2adx file

                    File.Delete(textBox1.Text + "/" + songName + ".adx"); // Deleting File

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = path + "/convert/WAV2ADX.EXE";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = '"' + ofd.FileName + '"' + " " + '"' + textBox1.Text + @"\" + songName + ".adx" + '"';


                    Process.Start(startInfo);

                    MessageBox.Show("Succesfully replaced " + listBox1.Text + " with " + ofd.FileName, "Done Replacing!");
                }

                if (".adx".Equals(Path.GetExtension(ofd.FileName), StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(textBox1.Text + "/" + songName + ".adx"); // Deleting File
                    File.Copy(ofd.FileName, textBox1.Text + "/" + songName + ".adx"); // Copy File

                    MessageBox.Show("Succesfully replaced " + listBox1.Text + " with " + ofd.FileName, "Done Replacing!");
                }
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
                MessageBox.Show("No Song is Selected please Select a song to Continue", "No Song Selected");
            }
            if (textBox1.Text == "") // Checks if the Game files are selected
            {
                MessageBox.Show("No Sound Media Files Selected!", "No Sound Files!");
            }

            if (listBox1.Text != "" || textBox1.Text != "")         // Checks if the text and list Box aren't Empty
            {

                string txt = listBox1.Text;
                switch (txt)
                {
                    case "Aisle 10":
                        songChangingFunc("aisle10");
                        break;
                    case "The Answer":
                        songChangingFunc("answer");
                        break;
                    case "Baby-T":
                        songChangingFunc("baby_t");
                        break;
                    case "Birthday Cake":
                        songChangingFunc("birthday");
                        break;
                    case "Bokfresh":
                        songChangingFunc("bokfresh");
                        break;
                    case "Latch Brothers Bounce":
                        songChangingFunc("bounce");
                        break;
                    case "Fly Like a Butterfly":
                        songChangingFunc("buttrfly");
                        break;
                    case "The Concept of Love":
                        songChangingFunc("concept");
                        break;
                    case "Funky Dealer":
                        songChangingFunc("dealer");
                        break;
                    case "Shape Da Future":
                        songChangingFunc("future");
                        break;
                    case "Statement of Intent":
                        songChangingFunc("intent");
                        break;
                    case "Koto Stomp":
                        songChangingFunc("koto");
                        break;
                    case "Count Latchula":
                        songChangingFunc("latchula");
                        break;
                    case "Let Mom Sleep (No Sleep Remix)":
                        songChangingFunc("letmom");
                        break;
                    case "I Love Love You":
                        songChangingFunc("lovelove");
                        break;
                    case "Rockin' da Mic (The Latch Bros Remix)":
                        songChangingFunc("mic");
                        break;
                    case "I'm Not a Model":
                        songChangingFunc("model");
                        break;
                    case "Oldies But Happies":
                        songChangingFunc("oldies");
                        break;
                    case "Me Likey the Poom Poom":
                        songChangingFunc("poompoom");
                        break;
                    case "Rock it On (D.S. Mix)":
                        songChangingFunc("rockiton");
                        break;
                    case "Humming the Bassline (D.S. Remix)":
                        songChangingFunc("g_park");
                        break;
                    case "The Scrappy (The Latch Bros Remix)":
                        songChangingFunc("scrappy");
                        break;
                    case "Sneakman (Toronto Mix)":
                        songChangingFunc("sneakman");
                        break;
                    case "Ill Victory Beat":
                        songChangingFunc("victory");
                        break;
                    case "What About The Future":
                        songChangingFunc("whatabout");
                        break;
                    case "Teknopathetic":
                        songChangingFunc("scrappy");
                        break;
                    case "Like It Like This Like That":
                        songChangingFunc("likeit");
                        break;
                    case "Sweet Soul Brother (B.B. Rights Mix)":
                        songChangingFunc("sweet");
                        break;
                    case "That's Enough (B.B. Rights Mix)":
                        songChangingFunc("thats");
                        break;
                    case "Grace And Glory":
                        songChangingFunc("grace");
                        break;
                    case "Set 1a":
                        songChangingFunc("s_set_01a");
                        break;
                    case "Set 1b":
                        songChangingFunc("s_set_01b");
                        break;
                    case "Set 2a":
                        songChangingFunc("s_set_02a");
                        break;
                    case "Set 2b":
                        songChangingFunc("s_set_02b");
                        break;
                    case "Set 3a":
                        songChangingFunc("s_set_03a");
                        break;
                    case "Set 3b":
                        songChangingFunc("s_set_03b");
                        break;
                    case "Set 4a":
                        songChangingFunc("s_set_04a");
                        break;
                    case "Set 4b":
                        songChangingFunc("s_set_04b");
                        break;
                    case "Set 5a":
                        songChangingFunc("s_set_05a");
                        break;
                    case "Set 5b":
                        songChangingFunc("s_set_05b");
                        break;
                    case "Set 6a":
                        songChangingFunc("s_set_06a");
                        break;
                    case "Set 6b":
                        songChangingFunc("s_set_06b");
                        break;
                    case "Set 7a":
                        songChangingFunc("s_set_07a");
                        break;
                    case "Set 7b":
                        songChangingFunc("s_set_07b");
                        break;
                    case "Set 8a":
                        songChangingFunc("s_set_08a");
                        break;
                    case "Set 8b":
                        songChangingFunc("s_set_08b");
                        break;
                    case "Set 9a":
                        songChangingFunc("s_set_09a");
                        break;
                    case "Set 9b":
                        songChangingFunc("s_set_09b");
                        break;
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
