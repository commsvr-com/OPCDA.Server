//Program.cs - punkt wejściowy aplikacji
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace SchemaGenerator
{
    static class Program
    {
        public static GraphGUIMainForm f1;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f1 = new GraphGUIMainForm();
            Application.Run(f1);
        }
    }
}