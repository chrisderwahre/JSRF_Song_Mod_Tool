﻿using Lib.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
//using NAudio.Wave;
using System.Net;

namespace JSRF_Song_Mod_Tool
{
    public partial class Form1 : Form
    {

        public String path = Directory.GetCurrentDirectory();
        public String version = "1.0.7"; // version string
        public static String stVersion = "1.0.7";
        public String mode = "Release"; // mode string

        public void ftpSelectedFileToXbox(string songname)
        {
            FtpClient ftpClient = new FtpClient("ftp://" + XBoxIP.Text + ":" + XBoxPort.Text, XBoxUser.Text, XBoxPassword.Text); // Connects to the selected ip address with the selected port and the selected user and password
            ftpClient.delete(XBoxJSRFGamePath.Text + "/" + songname + ".adx"); // Deletes the old file so the new one can be ftp'd
            ftpClient.upload(XBoxJSRFGamePath.Text + "/" + songname + ".adx", textBox1.Text.Replace(@"\", "/") + "/" + songname + ".adx"); // Uploads the new file
        }

        public string getStringFromConfigXML(string pathToString)
        {
            XmlDocument doc = new XmlDocument(); // Names a new XmlDocument doc
            doc.Load(path + "/Config.xml"); // Loads the xml file

            XmlNode node = doc.SelectSingleNode(pathToString); // Goes to the string
            ListViewItem ItemToReturn = new ListViewItem(node.InnerText); // List Item

            return ItemToReturn.Text; // Returns It

        }

        public string getStringFromLangXML(string pathToString)
        {
            XmlDocument doc = new XmlDocument(); // Names a new XmlDocument doc
            doc.Load(path + "/lang.xml"); // Loads the xml file

            XmlNode node = doc.SelectSingleNode(pathToString); // Goes to the string
            ListViewItem ItemToReturn = new ListViewItem(node.InnerText); // List Item

            return ItemToReturn.Text; // Returns It

        }

        public static string getStringFromLangFile(string pathToString)
        {
            XmlDocument doc = new XmlDocument(); // Names a new XmlDocument doc
            doc.Load("lang.xml"); // Loads the xml file

            XmlNode node = doc.SelectSingleNode(pathToString); // Goes to the string
            ListViewItem ItemToReturn = new ListViewItem(node.InnerText); // List Item

            return ItemToReturn.Text; // Returns It

        }

        public void songChangingFunc(string songName) // Song Changing Function
        {
            if (!File.Exists(textBox1.Text + "/" + songName + ".adx")) // Checks if the Folder is Correct
            {
                if (File.Exists(path + "/lang.xml"))
                {
                    MessageBox.Show(getStringFromLangXML("/Config/NoFTPSettingsOrNoSong"), "Wrong Song Media Folder");
                }
                else
                {
                    MessageBox.Show("Wrong Sound Media Folder: Couldn't find Original File to Replace", "Wrong Song Media Folder"); // Outout
                }
                return;
            }


            OpenFileDialog ofd = new OpenFileDialog(); // Creates a new OpenFileDialog
            ofd.Filter = "Supported Sound Files | *.wav; *.adx; *.mp3"; // Sets a filter for the files to selected

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = Directory.GetCurrentDirectory(); // Gets the current direcory

                
                if (".mp3".Equals(Path.GetExtension(ofd.FileName), StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(textBox1.Text + "/" + songName + ".adx"); // Deletes adx

                    ProcessStartInfo startInfo = new ProcessStartInfo(); // Other shit
                    startInfo.CreateNoWindow = false; // Other shit
                    startInfo.UseShellExecute = false; // Other shit
                    startInfo.FileName = path + "/convert/ffmpeg.exe";  // Filename for the .exe file
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Not showning the cmd 
                    startInfo.Arguments = $"-i {'"'}{ofd.FileName}{'"'} {'"'}{textBox1.Text}/{songName}.adx{'"'}"; // Argument for converting Wav files to adx!

                    Process.Start(startInfo); // Starts the process with the settings from above

                    if (File.Exists(path + "/lang.xml"))
                    {
                        MessageBox.Show(getStringFromLangXML("/Config/SuccesfullyReplaced").Replace("[SELECTED SONG]", listBox1.Text).Replace("[TO REPLACED FILE]", ofd.FileName));
                    }
                    else
                    {
                        MessageBox.Show("Succesfully replaced " + listBox1.Text + " with " + ofd.FileName, "JSRF Song Mod Tool"); // Output
                    }
                }
                
                if (".wav".Equals(Path.GetExtension(ofd.FileName), StringComparison.OrdinalIgnoreCase)) // Checks if the selected file is a wav file
                {
                    File.Delete(textBox1.Text + "/" + songName + ".adx"); // Deletes adx

                    ProcessStartInfo startInfo = new ProcessStartInfo(); // Other shit
                    startInfo.CreateNoWindow = false; // Other shit
                    startInfo.UseShellExecute = false; // Other shit
                    startInfo.FileName = path + "/convert/ffmpeg.exe";  // Filename for the .exe file
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Not showning the cmd 
                    startInfo.Arguments = $"-i {'"'}{ofd.FileName}{'"'} {'"'}{textBox1.Text}/{songName}.adx{'"'}"; // Argument for converting Wav files to adx!

                    Process.Start(startInfo); // Starts the process with the settings from above

                    if (File.Exists(path + "/lang.xml"))
                    {
                        MessageBox.Show(getStringFromLangXML("/Config/SuccesfullyReplaced").Replace("[SELECTED SONG]", listBox1.Text).Replace("[TO REPLACED FILE]", ofd.FileName));
                    }
                    else
                    {
                        MessageBox.Show("Succesfully replaced " + listBox1.Text + " with " + ofd.FileName, "Done Replacing!"); // Output
                    }
                }

                if (".adx".Equals(Path.GetExtension(ofd.FileName), StringComparison.OrdinalIgnoreCase)) // Checks if a adx file is selected
                {
                    File.Delete(textBox1.Text + "/" + songName + ".adx"); // Deleting File
                    File.Copy(ofd.FileName, textBox1.Text + "/" + songName + ".adx"); // Copy File

                    if (File.Exists(path + "/lang.xml"))
                    {
                        MessageBox.Show(getStringFromLangXML("/Config/SuccesfullyReplacedADX").Replace("[SELECTED SONG]", listBox1.Text).Replace("[TO REPLACED FILE]", ofd.FileName));
                    }
                    else
                    {
                        MessageBox.Show("Succesfully replaced " + listBox1.Text + " with " + ofd.FileName, "JSRF Song Mod Tool"); // Output
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent(); // The standard C# shit.
            this.Text = "JSRF Song Mod Tool [" + version + "]"; // Sets the Title with the current version
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(path + "/lang.xml"))
            {
                MessageBox.Show(getStringFromLangXML("/Config/AboutButton").Replace("[VERSION]", version).Replace("[MODE]", mode), "JSRF Song Mod Tool");
            }
            else
            {
                MessageBox.Show("Version: " + version + "  " + mode + "\n\nJSRF Song Mod Tool by ChrisderWahre 2018\n\nContributors:\n -neodos (Helped me with FTP stuff)\n -BURRRR (Helped me with the set files)", "JSRF Song Mod Tool"); // About Button with info about what version is running
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists(path + "/lang.xml"))
            {
                MessageBox.Show(getStringFromLangXML("/Config/HelpButton").ToString().Replace("\n", "\n"), "JSRF Song Mod Tool");
            }
            else
            {
                MessageBox.Show("How to use:\n1. Select Your JSRF Sound Folder (Media/Z_ADX/BGM)\n2. Pick the Song to Replace\n3.Click the Button and Select the audio file of the new File\n4.Configure the FTP Settings! \n5.Click the FTP to XBOX Button to ftp the selected to the XBox!", "JSRF Song Mod Tool"); // Tutorial Button
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Text == "") // Checks if a song is selected
            {
                if (File.Exists(path + "/lang.xml"))
                {
                    MessageBox.Show(getStringFromLangXML("/Config/NoSongSelected"), "JSRF Song Mod Tool");
                }
                else
                {
                    MessageBox.Show("No Song is Selected please Select a song to Continue", "JSRF Song Mod Tool"); // Outpot
                }
            }
            if (textBox1.Text == "") // Checks if the Game files are selected
            {
                if (File.Exists(path + "/lang.xml"))
                {
                    MessageBox.Show(getStringFromLangXML("/Config/NoSongMediaFolder"), "JSRF Song Mod Tool");
                }
                else
                {
                    MessageBox.Show("No Song Media Files Selected!", "JSRF Song Mod Tool"); // Output
                }
            }

            // ** Removed these Comments ** //

            if (listBox1.Text != "" || textBox1.Text != "")         // Checks if the text and list Box aren't Empty
            {

                string txt = listBox1.Text;
                switch (txt) // Switched the songs and makes it easier to use the function and saves lines of code.
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
                    // Failed (Over) Cleared Title Song(Concept of Love) DJ Demo(Ill Victory Beat)
                    case "Failed (Over)":
                        songChangingFunc("g_over");
                        break;
                    case "Cleared":
                        songChangingFunc("clear");
                        break;
                    case "Title Song(Concept of Love":
                        songChangingFunc("title");
                        break;
                    case "DJ Demo(Ill Victory Beat)":
                        songChangingFunc("dj_demo1");
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
                    case "Ending (Sweet Sould Brother (Toronto Remix))":
                        songChangingFunc("ending");
                        break;
                    case "Ending l (Playing various tracks(Ending Screen))":
                        songChangingFunc("ending_l");
                        break;
                    case "Ending s (Playing various tracks(Ending Screen))":
                        songChangingFunc("ending_s");
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
            } else {
                if (File.Exists(path + "/lang.xml"))
                {
                    MessageBox.Show(getStringFromLangXML("/Config/CouldntLoadFolder"), "Option not Completed");
                } else
                {
                    MessageBox.Show("Couldn't load the Folder, option was not completed.", "Option not Completed.");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (File.Exists(path + "/Config.xml"))
            {
                try {
                    XBoxIP.Text = getStringFromConfigXML("/Config/XBoxIP");
                    XBoxPort.Text = getStringFromConfigXML("/Config/XBoxPort");
                    XBoxUser.Text = getStringFromConfigXML("/Config/XBoxUser");
                    XBoxPassword.Text = getStringFromConfigXML("/Config/XBoxPassword");
                    XBoxJSRFGamePath.Text = getStringFromConfigXML("/Config/XBoxSongPath");
                    textBox1.Text = getStringFromConfigXML("/Config/LocalSongPath");
                } catch {
                    if (File.Exists(path + "/lang.xml")) {
                        MessageBox.Show(getStringFromLangXML("/Config/IncompletedConfigFile"), "Config File Error");
                    }
                    else
                    {
                        MessageBox.Show("A Config file is loaded but isn't Complete, please check the Official Github page for an example (github.com/chrisderwahre/JSRF_Song_Mod_Tool) or create a new one.", "Config File Error!");
                    }

                }
            } else {

                // Basic Config if not Config file has been found
                XBoxPassword.Text = "xbox"; // Sets standart settings for the XBox Password textbox
                XBoxPort.Text = "21"; // Sets standart settings for the XBox IP textbox
                XBoxUser.Text = "xbox"; // Sets standart settings for the XBox User textbox
                XBoxJSRFGamePath.Text = "/E/Games/Jet Set Radio Future/Media/Z_ADX/BGM"; // Sets standart settings for the XBox JSRF Path textbox
            }

            if (File.Exists(path + "/lang.xml"))
            {
                button1.Text = getStringFromLangXML("/Config/About"); // About button
                button4.Text = getStringFromLangXML("/Config/Help"); // Help button
                replaceBtn.Text = getStringFromLangXML("/Config/ReplaceThisSong"); // Replace Button
                ftptoxbox_btn.Text = getStringFromLangXML("/Config/FTPtoXBOX"); // Ftp to Xbox button
                whatarethesetfiles.Text = getStringFromLangXML("/Config/WhatAreTheSetFiles"); // What are the set files label
                label4.Text = getStringFromLangXML("/Config/PickSong"); // Pick song label
                tabPage1.Text = getStringFromLangXML("/Config/ToolPageName"); // Sets the Tab page names
                tabPage2.Text = getStringFromLangXML("/Config/SettingsPageName"); // Sets the Tab page names
                jsrflocalfiles.Text = getStringFromLangXML("/Config/JSRFLocalGroupBox"); // JSRF Local Files Group Box
                groupBox2.Text = getStringFromLangXML("/Config/FTPGroupBox"); // FTP Settings Group box
                label3.Text = getStringFromLangXML("/Config/LocalSongPath"); // JSRF Sound Path label

                label5.Text = getStringFromLangXML("/Config/XBoxIP"); // Xbox ip label
                label6.Text = getStringFromLangXML("/Config/XBoxPort"); // Xbox port label
                label7.Text = getStringFromLangXML("/Config/XBoxUser"); // Xbox user label
                label8.Text = getStringFromLangXML("/Config/XBoxPassword"); // Xbox password label

                label9.Text = getStringFromLangXML("/Config/XBoxSongPath"); // Xbox jsrf game sound path label 
                label10.Text = getStringFromLangXML("/Config/JSRFPathtoMedia"); // local jsrf game sound path label
            }

        }

        private void whatarethesetfiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("Set File Info.txt") || File.Exists("set file info.txt")) // Checks if a local copy of the text file exist
            {
                Process.Start(path + "/Set file info.txt"); // Starts the file
            } else if (File.Exists("set_file_info.txt")) {
                Process.Start(path + "/set_file_info.txt"); // Starts the file
            } else {
                Process.Start("https://pastebin.com/raw/spiE5xup"); // Opens a link in Browser for the set file information
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Nothing here, this was a mistake, please remove the function.
        }

        private void ftptoxbox_btn_Click(object sender, EventArgs e)
        {
            if (XBoxIP.Text != "" && XBoxJSRFGamePath.Text != "" && XBoxPassword.Text != "" && XBoxPort.Text != "" && XBoxUser.Text != "" && textBox1.Text != "") // Checks if everythings existing
            {
                string txt = listBox1.Text;
                switch (txt) // Switchs the name and makes it easier and don't take much Lines
                {
                    case "Aisle 10": // If Aisle 10 is selected
                        ftpSelectedFileToXbox("aisle10"); // use the ftpSelectedFileToXbox function with aisle10 (the adx file name)
                        break; // breaks so nothing else will be made
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
                    case "Failed (Over)":
                        ftpSelectedFileToXbox("g_over");
                        break;
                    case "Cleared":
                        ftpSelectedFileToXbox("clear");
                        break;
                    case "Title Song(Concept of Love":
                        ftpSelectedFileToXbox("title");
                        break;
                    case "DJ Demo(Ill Victory Beat)":
                        ftpSelectedFileToXbox("dj_demo1");
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
                    case "Ending (Sweet Sould Brother (Toronto Remix))":
                        ftpSelectedFileToXbox("ending");
                        break;
                    case "Ending l (Playing various tracks(Ending Screen))":
                        ftpSelectedFileToXbox("ending_l");
                        break;
                    case "Ending s (Playing various tracks(Ending Screen))":
                        ftpSelectedFileToXbox("ending_s");
                        break;
                        // Ending (Sweet Sould Brother (Toronto Remix))
                        //Ending l (Playing various tracks(Ending Screen))
                        //Ending s (Playing various tracks(Ending Screen))
                }
            } else {
                if (File.Exists(path + "/lang.xml"))
                {
                    MessageBox.Show(getStringFromLangXML("/Config/NoFTPSettingsOrNoSong"), "Error");
                }
                else
                {
                    MessageBox.Show("XBox FTP Settings NOT correct and/or no Song Selected", "Error"); // Error reporting.
                }
            }
        }

        public static Boolean checkUpdate()
        {
            string versionlink = "https://pastebin.com/raw/7XjGSUGf";

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(versionlink);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            Version a = new Version(content); // the version that is on the server
            Version b = new Version(stVersion); // the local version

            if (content != "")
            {
                if (a > b)
                {
                    return true;
                }

                if (b > a)
                {
                    return false;
                }
            } else {
                return false;
            }
            return false;
        }

        public static void doUpdate()
        {
            if (checkUpdate() == true)
            {   
                if (File.Exists("lang.xml"))
                {
                    DialogResult result = MessageBox.Show(getStringFromLangFile("/Config/Update"), "JSRF Song Mod Tool", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        Process.Start("https://github.com/chrisderwahre/JSRF_Song_Mod_Tool/releases/");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Update Available do you want to download it now?", "JSRF Song Mod Tool", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        Process.Start("https://github.com/chrisderwahre/JSRF_Song_Mod_Tool/releases/");
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkUpdate() == true)
            {
                doUpdate();
            }
        }
    }   
}