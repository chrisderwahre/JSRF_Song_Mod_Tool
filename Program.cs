using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace JSRF_Song_Mod_Tool
{
    static class Program
    {
        private static Boolean loadsModeFromConfig()
        {
            try
            {
                XmlDocument doc = new XmlDocument(); // Names a new XmlDocument doc
                doc.Load("Config.xml"); // Loads the xml file

                XmlNode node = doc.SelectSingleNode("/Config/DebugMode"); // Goes to the string
                ListViewItem ItemToReturn = new ListViewItem(node.InnerText); // List Item

                if (ItemToReturn.Text == "TRUE")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception e) {
                return false;
            }
        } 

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (loadsModeFromConfig() == true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MultiFormContext(new Form1(), new Form2()));
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
