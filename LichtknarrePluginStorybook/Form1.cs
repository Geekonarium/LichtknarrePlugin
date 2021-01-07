using LichtknarrePlugin.Utils;
using LichtpistolePlugin.config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LichtknarrePluginStorybook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Konfigurator konfigurator = new Konfigurator(0);
            PluginCalibration form = new PluginCalibration(konfigurator);
            form.Show();


        }
    }
}
