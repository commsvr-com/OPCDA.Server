//_______________________________________________________________
//  Title   : ProgressBarWindow
//  System  : Microsoft VisualStudio 2013 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2015, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NetworkConfig.HMI.Import
{
  /// <summary>
  /// Class ProgressBarWindow
  /// </summary>
  public partial class ProgressBarWindow : Form
  {
    private delegate void MethodWithInt(int value);
    private delegate void MethodWithString(string value);
    private ProgressBarWindow()
    {
      InitializeComponent();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressBarWindow"/> class.
    /// </summary>
    /// <param name="eventHandler">The event handler.</param>
    /// <param name="progressMin">The progress minimum.</param>
    /// <param name="progressMax">The progress maximum.</param>
    /// <param name="progressStep">The progress step.</param>
    public ProgressBarWindow(DoWorkEventHandler eventHandler, int progressMin, int progressMax, int progressStep)
      : this()
    {
      backgroundWorker_DoTheJob.DoWork += eventHandler;
      progressBar1.Minimum = progressMin;
      progressBar1.Maximum = progressMax;
      progressBar1.Step = progressStep;
    }

    private void ProgressBarWindow_Load(object sender, EventArgs e)
    {
      backgroundWorker_DoTheJob.RunWorkerAsync(this);
    }
    private void UpdatePercentLabel()
    {
      this.label_percent.Text = String.Format("{0:0.#}%", (float)progressBar1.Value / (float)(progressBar1.Maximum - progressBar1.Minimum) * 100.0f);
    }

    private void backgroundWorker_DoTheJob_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!e.Cancelled)
        this.DialogResult = DialogResult.OK;
      else
        this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void button_cancel_Click(object sender, EventArgs e)
    {
      CancelWork();
    }

    #region public
    /// <summary>
    /// Performs the step.
    /// </summary>
    public void PerformStep()
    {
      if (InvokeRequired)
        BeginInvoke(new MethodInvoker(PerformStep));
      else
      {
        progressBar1.PerformStep();
        UpdatePercentLabel();
      }
    }
    /// <summary>
    /// Sets the progress value.
    /// </summary>
    /// <param name="val">The value.</param>
    public void SetProgressValue(int val)
    {
      if (InvokeRequired)
        BeginInvoke(new MethodWithInt(SetProgressValue), new object[] { val });
      else
      {
        progressBar1.Value = val;
        UpdatePercentLabel();
      }
    }
    /// <summary>
    /// Sets the information.
    /// </summary>
    /// <param name="information">The information.</param>
    public void SetInformation(string information)
    {
      if (InvokeRequired)
        BeginInvoke(new MethodWithString(SetInformation), new object[] { information });
      else
      {
        label_info.Text = information;
      }
    }
    /// <summary>
    /// Cancels the work.
    /// </summary>
    public void CancelWork()
    {
      if (InvokeRequired)
        BeginInvoke(new MethodInvoker(CancelWork));
      else
      {
        backgroundWorker_DoTheJob.CancelAsync();
      }
    }
    #endregion

  }
}