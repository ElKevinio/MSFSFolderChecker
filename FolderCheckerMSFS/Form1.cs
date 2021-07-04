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

namespace FolderCheckerMSFS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> falseFolder = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fds = new FolderBrowserDialog();
            DialogResult dr = fds.ShowDialog();
            if(dr == DialogResult.OK)
            {
                textBox1.Text = fds.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            falseFolder.Clear();
            textBox2.Text = "";
            lblInfo.Text = "";
            progressBar1.Value = 0;
            if (Directory.Exists(textBox1.Text))
            {
                string[] directories = Directory.GetDirectories(textBox1.Text);
                progressBar1.Maximum = directories.Count();
                foreach (string folder in directories)
                {
                    if (!File.Exists(folder + "\\manifest.json"))
                    {
                        falseFolder.Add(folder);
                        textBox2.Text += folder.Split('\\')[folder.Split('\\').Count() - 1] + Environment.NewLine;
                    }
                    progressBar1.Value++;
                }
                if (falseFolder.Count > 0)
                {
                    lblInfo.Text = falseFolder.Count + " Folders have the wrong structure";
                }
            }
            else
            { MessageBox.Show("Der Link ist nicht korrekt"); }
        }
        List<string> addons = new List<string>();

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            lblInfo.Text = "";
            addons.Clear();
            if (Directory.Exists(textBox1.Text))
            {
                foreach (string f in Directory.GetDirectories(textBox1.Text))
                {
                    addons.Add(f.Split('\\')[f.Split('\\').Count() - 1]);
                }
                progressBar1.Value = 0;
                progressBar1.Maximum = addons.Count - 1;
                for (int a = 0; a < addons.Count - 1;a++)
                {
                    progressBar1.Value++;
                    for (int i = 0; i < addons.Count - 1; i++)
                    {
                        if (a != i)
                        {
                            double result = stringcompare.Compare(addons[a], addons[i]);
                            if (result > 0.95)
                            {
                                textBox2.Text += addons[i] + " & " + addons[i + 1] + Environment.NewLine;
                            }
                            else
                            {

                            }
                        }
                    }
                }
                lblInfo.Text = "Fertig gestellt";
            }
            else
            { MessageBox.Show("Der Link ist nicht korrekt"); }
}

        private void Form1_Load(object sender, EventArgs e)
        {
            if(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ "\\Microsoft Flight Simulator\\Packages\\Community"))
            {
                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft Flight Simulator\\Packages\\Community";
            }
            progressBar1.BackColor = Color.Black;
        }
    }
}
