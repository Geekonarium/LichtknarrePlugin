﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LichtknarrePlugin.Utils;

namespace LichtknarrePlugin
{
    public partial class PluginCalibration : Form
    {
        Konfigurator konfigurator;

        public PluginCalibration(Konfigurator konfigurator)
        {
            InitializeComponent();
            this.konfigurator = konfigurator;
            checkBox1.Checked = konfigurator.usePresentationMode;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked != konfigurator.usePresentationMode)
            {
                konfigurator.usePresentationMode = checkBox1.Checked;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
