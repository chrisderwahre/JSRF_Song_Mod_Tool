﻿using Lib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSRF_Song_Mod_Tool
{

    public partial class Form1 : Form
    {

        public void ftpSelectedFileToXbox(string songname)
        {      

            FtpClient ftpClient = new FtpClient("ftp://" + XBoxIP.Text + ":" + XBoxPort.Text, XBoxUser.Text, XBoxPassword.Text);
            ftpClient.delete(XBoxJSRFGamePath.Text + "/" + songname + ".adx");
            ftpClient.upload(XBoxJSRFGamePath.Text + "/" + songname + ".adx", textBox1.Text.Replace(@"\", "/") + "/" + songname + ".adx");
        }

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
            String version = "1.0.4";
            String mode = "Release";
            MessageBox.Show("Version: " + version + "  " + mode + "\n\nJSRF Song Mod Tool by ChrisderWahre 2018\n\nContributors:\n -neodos (Helped me with FTP stuff)\n -BURRRR (Helped me with the set files)", "JSRF Song Mod Tool"); // About Button
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String set1a = "Set 1a: Funky Dealer, I Love love you, Let Mom Sleep (No Sleep Remix), Fly Like a Butterfly, What about the future, Sneakman(Toronto Remix)\n\n";
            String set1b = "Set 1b: Fly like a Butterfly, What about the future, Sneakman (Toronto Remix), Funky Dealer, I Love love you, Let mom sleep (No Sleep Remix)\n\n";
            String set2a = "Set 2a: Statement of Intent, UNKNOWN, Aisle 10, Rockin da Mic, Oldies but Happies, Birthday Cake, Like this like that, Sweet Soul Broter(Remix)\n\n";
            String set2b = "Set 2b: Oldies but Happies, Birthday Cake, Like this like that, Sweet Soul Brother, Statement of Intent, UNKNOWN, Aisle 10, Rockin da Mic\n\n";
            String set3a = "Set 3a: Sneakman, Teknopathetic, Like this like that, Let Mom Sleep (No Sleep Remix), Baby-t";
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

            // Set 1a Funky Dealer - I Love love you - Let Mom Sleep - Fly Like a Butter Fly - What about the future - Sneakman
            // Set 1b Fly like a butterfly - What about the future - Sneakman - Funky Dealer - I love love you - Let mom sleep
            // Set 2a Statement of Intent - UNKNOWN - Aisle 10 - Rockin da mic - Oldies but Happies - Birthday Cake - Like this like that - sweet soul brother
            // Set 2b Oldies but Happies - Birthday Cake
            // Set 3a
            // Set 3b
            // Set 4
            // Set 5a
            // Set 5b
            // Set 6
            // Set 7a
            // Set 7b
            // Set 8a
            // Set 8b
            // Set 9a
            // Set 9b

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
                    case "Set 4":
                        songChangingFunc("set_04");
                        break;
                    case "Set 5a":
                        songChangingFunc("s_set_05a");
                        break;
                    case "Set 5b":
                        songChangingFunc("s_set_05b");
                        break;
                    case "Set 6":
                        songChangingFunc("set_06");
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
            tabPage1.Text = @"Tool";
            tabPage2.Text = @"Settings";

            XBoxPassword.Text = "xbox";
            XBoxPort.Text = "21";
            XBoxUser.Text = "xbox";
            XBoxJSRFGamePath.Text = "/E/Games/Jet Set Radio Future/Media/Z_ADX/BGM";
        }

        private void whatarethesetfiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://pastebin.com/raw/spiE5xup");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ftptoxbox_btn_Click(object sender, EventArgs e)
        {
                if (XBoxIP.Text != "" && XBoxJSRFGamePath.Text != "" && XBoxPassword.Text != "" && XBoxPort.Text != "" && XBoxUser.Text != "" && textBox1.Text != "")
                {
                    string txt = listBox1.Text;
                    switch (txt)
                    {
                        case "Aisle 10":
                            ftpSelectedFileToXbox("aisle10");
                            break;
                        case "The Answer":
                            ftpSelectedFileToXbox("answer");
                            break;
                        case "Baby-T":
                            ftpSelectedFileToXbox("baby_t");
                            break;
                        case "Birthday Cake":
                            ftpSelectedFileToXbox("birthday");
                            break;
                        case "Bokfresh":
                            ftpSelectedFileToXbox("bokfresh");
                            break;
                        case "Latch Brothers Bounce":
                            ftpSelectedFileToXbox("bounce");
                            break;
                        case "Fly Like a Butterfly":
                            ftpSelectedFileToXbox("buttrfly");
                            break;
                        case "The Concept of Love":
                            ftpSelectedFileToXbox("concept");
                            break;
                        case "Funky Dealer":
                            ftpSelectedFileToXbox("dealer");
                            break;
                        case "Shape Da Future":
                            ftpSelectedFileToXbox("future");
                            break;
                        case "Statement of Intent":
                            ftpSelectedFileToXbox("intent");
                            break;
                        case "Koto Stomp":
                            ftpSelectedFileToXbox("koto");
                            break;
                        case "Count Latchula":
                            ftpSelectedFileToXbox("latchula");
                            break;
                        case "Let Mom Sleep (No Sleep Remix)":
                            ftpSelectedFileToXbox("letmom");
                            break;
                        case "I Love Love You":
                            ftpSelectedFileToXbox("lovelove");
                            break;
                        case "Rockin' da Mic (The Latch Bros Remix)":
                            ftpSelectedFileToXbox("mic");
                            break;
                        case "I'm Not a Model":
                            ftpSelectedFileToXbox("model");
                            break;
                        case "Oldies But Happies":
                            ftpSelectedFileToXbox("oldies");
                            break;
                        case "Me Likey the Poom Poom":
                            ftpSelectedFileToXbox("poompoom");
                            break;
                        case "Rock it On (D.S. Mix)":
                            ftpSelectedFileToXbox("rockiton");
                            break;
                        case "Humming the Bassline (D.S. Remix)":
                            ftpSelectedFileToXbox("g_park");
                            break;
                        case "The Scrappy (The Latch Bros Remix)":
                            ftpSelectedFileToXbox("scrappy");
                            break;
                        case "Sneakman (Toronto Mix)":
                            ftpSelectedFileToXbox("sneakman");
                            break;
                        case "Ill Victory Beat":
                            ftpSelectedFileToXbox("victory");
                            break;
                        case "What About The Future":
                            ftpSelectedFileToXbox("whatabout");
                            break;
                        case "Teknopathetic":
                            ftpSelectedFileToXbox("scrappy");
                            break;
                        case "Like It Like This Like That":
                            ftpSelectedFileToXbox("likeit");
                            break;
                        case "Sweet Soul Brother (B.B. Rights Mix)":
                            ftpSelectedFileToXbox("sweet");
                            break;
                        case "That's Enough (B.B. Rights Mix)":
                            ftpSelectedFileToXbox("thats");
                            break;
                        case "Grace And Glory":
                            ftpSelectedFileToXbox("grace");
                            break;
                        case "Set 1a":
                            ftpSelectedFileToXbox("s_set_01a");
                            break;
                        case "Set 1b":
                            ftpSelectedFileToXbox("s_set_01b");
                            break;
                        case "Set 2a":
                            ftpSelectedFileToXbox("s_set_02a");
                            break;
                        case "Set 2b":
                            ftpSelectedFileToXbox("s_set_02b");
                            break;
                        case "Set 3a":
                            ftpSelectedFileToXbox("s_set_03a");
                            break;
                        case "Set 3b":
                            ftpSelectedFileToXbox("s_set_03b");
                            break;
                        case "Set 4":
                            ftpSelectedFileToXbox("set_04");
                            break;
                        case "Set 5a":
                            ftpSelectedFileToXbox("s_set_05a");
                            break;
                        case "Set 5b":
                            ftpSelectedFileToXbox("s_set_05b");
                            break;
                        case "Set 6":
                            ftpSelectedFileToXbox("set_06");
                            break;
                        case "Set 7a":
                            ftpSelectedFileToXbox("s_set_07a");
                            break;
                        case "Set 7b":
                            ftpSelectedFileToXbox("s_set_07b");
                            break;
                        case "Set 8a":
                            ftpSelectedFileToXbox("s_set_08a");
                            break;
                        case "Set 8b":
                            ftpSelectedFileToXbox("s_set_08b");
                            break;
                        case "Set 9a":
                            ftpSelectedFileToXbox("s_set_09a");
                            break;
                        case "Set 9b":
                            ftpSelectedFileToXbox("s_set_09b");
                            break;
                    }
                } else {
                    MessageBox.Show("XBox FTP Settings NOT correct and/or no Song Selected", "Error");
                }
            }
        }
}