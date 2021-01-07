using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LichtknarrePlugin.Utils;
using WiimoteLib;

namespace LichtpistolePlugin.config
{
    public partial class PluginCalibration : Form
    {
        Konfigurator konfigurator;

        public PluginCalibration(Konfigurator konfigurator)
        {
            InitializeComponent();
            this.konfigurator = konfigurator;
            checkBox1.Checked = konfigurator.usePresentationMode;

            //<inputspersecond>
            trackBar1.Value = konfigurator.inputPerSecond;
            label1.Text = "value: " + konfigurator.inputPerSecond;
            button1.Text = button1.Text.Replace("?", konfigurator.intputPerSecondDefaultValue.ToString());
            //</inputspersecond>


            WiimoteLib.ButtonState buttonState = new WiimoteLib.ButtonState();
            foreach (var prop in buttonState.GetType().GetProperties())
            {

            }
            
            keybindPanel.Controls.Add(
                new KeyBindControl("home")
            );

            KeyBindControl newControl = new KeyBindControl("1");
            newControl.Top = 230;
            keybindPanel.Controls.Add(
                newControl
            );
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked != konfigurator.usePresentationMode)
            {
                konfigurator.usePresentationMode = checkBox1.Checked;
            }
        }

        //<inputspersecond>
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            konfigurator.inputPerSecond = trackBar1.Value;
            label1.Text = "value: " + konfigurator.inputPerSecond;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            konfigurator.inputPerSecond = konfigurator.intputPerSecondDefaultValue;
            trackBar1.Value = konfigurator.intputPerSecondDefaultValue;
            label1.Text = "value: " + konfigurator.intputPerSecondDefaultValue;
        }
        //</inputspersecond>
    }
}
