using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NetworkConfig.HMI
{
  internal partial class ProgresReport: Form
  {
    internal ProgresReport()
    {
      InitializeComponent();
    }
    internal ProgressBar progresBar
    {
      get
      {
        return progressBar1;
      }
    }
    internal string LabelText
    {
      get
      {
        return label1.Text;
      }
      set
      {
        label1.Text = value;
      }
    }


  }
}