//GraphGUISettings.cs - zawiera funkcjonalność okienka z opcjami programu
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
namespace SchemaGenerator
{
    public partial class GraphGUISettings : Form
    {
        public GraphGUISettings()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.textBox1.Text = Properties.Settings.Default.labelFontSize.ToString();
            this.textBox2.Text = Properties.Settings.Default.labelFont2Size.ToString();
            this.textBox3.Text = Properties.Settings.Default.infoFontSize.ToString();
        }
        private void button1_Click(object sender, EventArgs e) //ok
        {
            Properties.Settings.Default.labelFontSize = Convert.ToInt16(this.textBox1.Text);
            Properties.Settings.Default.labelFont2Size = Convert.ToInt16(this.textBox2.Text);
            Properties.Settings.Default.infoFontSize = Convert.ToInt16(this.textBox3.Text);
            Properties.Settings.Default.Save();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e) //cancel
        {
            this.Close();
        }
    }
}