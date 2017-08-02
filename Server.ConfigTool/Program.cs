using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using CAS.CommServer.DA.Server.ConfigTool.ServersModel;

namespace CAS.CommServer.DA.Server.ConfigTool
{
    class Program
    {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
                if (ProcessCommandLine())
                {
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ComServerListDlg());
			}
			catch (Exception exception)
			{
				ConfigUtilities.HandleException(Application.ProductName, MethodBase.GetCurrentMethod(), exception);
			}		
		}

		/// <summary>
		/// Processes the command line arguments.
		/// </summary>
		private static bool ProcessCommandLine()
		{
			string commandLine = Environment.CommandLine;

            List<string> tokens = new List<string>();

            bool quotedToken = false;
            StringBuilder token = new StringBuilder();

            for (int ii = 0; ii < commandLine.Length; ii++)
            {
                char ch = commandLine[ii];
                             
                if (quotedToken)
                {
                    if (ch == '"')
                    {
                        if (token.Length > 0)
                        {
                            tokens.Add(token.ToString());
                            token = new StringBuilder();
                        }
                        
                        quotedToken = false;
                        continue;
                    }

                    token.Append(ch);
                }
                else
                {
                    if (token.Length == 0)
                    {
                        if (ch == '"')
                        {                            
                            quotedToken = true;
                            continue;
                        }
                    }

                    if (Char.IsWhiteSpace(ch))
                    {
                        if (token.Length > 0)
                        {
                            tokens.Add(token.ToString());
                            token = new StringBuilder();
                        }
                           
                        continue;
                    }

                    token.Append(ch);
                }               
            }

            if (token.Length > 0)
            {
                tokens.Add(token.ToString());
            }

            // launch gui if no arguments provided.
            if (tokens.Count == 1)
            {
                return false;
            }
            
            bool silent = false;

            for (int ii = 1; ii < tokens.Count; ii++)
            {                
                if (tokens[ii] == "-?")
                {				
					StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Opc.ConfigTool.HelpText.txt"));
                    MessageBox.Show(reader.ReadToEnd(), "Opc.ConfigTool");
                    reader.Close();                    
                    return true;
                }

                if (tokens[ii] == "-s")
                {
                    silent = true;
                    continue;
                }

                try
                {
                    if (tokens[ii] == "-ra")
                    {
                        if (tokens.Count - ii != 2)
                        {
                            throw new ArgumentException("Incorrect number of parameters specified with the -ra option.");
                        }

                        DotNetOpcServer.RegisterAssembly(tokens[ii+1]); 
                        return true;
                    }

                    if (tokens[ii] == "-ua")
                    {
                        if (tokens.Count - ii != 2)
                        {
                            throw new ArgumentException("Incorrect number of parameters specified with the -ua option.");
                        }

                        DotNetOpcServer.UnregisterAssembly(tokens[ii+1]); 
                        return true;
                    }

                    if (tokens[ii] == "-rx")
                    {
                        if (tokens.Count - ii != 2)
                        {
                            throw new ArgumentException("Incorrect number of parameters specified with the -rx option.");
                        }

                        RegisteredDotNetOpcServer.Import(tokens[ii+1], true);
                        return true;
                    }

                    if (tokens[ii] == "-ux")
                    {
                        if (tokens.Count - ii != 2)
                        {
                            throw new ArgumentException("Incorrect number of parameters specified with the -ux option.");
                        }

                        RegisteredDotNetOpcServer.Import(tokens[ii+1], false);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    if (!silent)
                    {
                        new ExceptionDlg().ShowDialog("Opc.ConfigTool", e);
                        return true;
                    }
                }
            }

			return true;
		}
    }
}
