//GraphGUIAbout.cs - zawiera funkcjonalność okienka informacji o programie
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
namespace SchemaGenerator
{
    public partial class GraphGUIAbout : Form
    {
        public GraphGUIAbout()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.button1_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}