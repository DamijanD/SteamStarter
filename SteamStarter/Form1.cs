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

namespace SteamStarter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool cancel = false;

        private void button1_Click(object sender, EventArgs e)
        {
            StartSteam();
        }

        private void StartSteam()
        {
            int cnt = 0;
            Random r = new Random();
            cancel = false;

            bool steamOK = false;
            do
            {
                label1.Text = $"Starting {cnt++}";

                var process = Process.Start("steam.exe");
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);
                Application.DoEvents();

                label1.Text += "... checking";
                Application.DoEvents();
                steamOK = Process.GetProcessesByName("steamwebhelper").Any();

                if (!steamOK)
                {
                    steamOK = Process.GetProcesses().Where(x => x.ProcessName.StartsWith("steam")).Count() > 2;
                }

                Application.DoEvents();

                if (!steamOK)
                {
                    label1.Text += "... killing";
                    Application.DoEvents();
                    process.Kill();
                }

                System.Threading.Thread.Sleep(r.Next(2000, 4000));
                Application.DoEvents();
            }
            while (!steamOK && !cancel);

            label1.Text += "... OK!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartSteam();
        }
    }
}
