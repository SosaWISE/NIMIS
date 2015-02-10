using System;
using System.Configuration;
using System.Windows.Forms;
using SOS.Data;

namespace Nxs.Clients.Windows.LicensingManager
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			SOS.Lib.Util.Configuration.ConfigurationSettings.Current.SetProperties("Preferences", ConfigurationManager.AppSettings["Environment"]);
			SubSonicConfigHelper.SetupConnectionStrings();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);


#if DEBUG
			/*

            Application.Run(new UploadExcelFile());

			Environment.Exit(0);

            //*/
#endif

			Application.Run(new Form1());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
#if !DEBUG
			//REVIEW: this feels incomplete. show the user a message or something.
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				DefaultErrorManager errorManager = new DefaultErrorManager();
				errorManager.AddCriticalMessage(ex);
				errorManager.PersistMessages();
			}
			Application.Restart();
#else
			System.Diagnostics.Debugger.Break();
#endif
		}
	}
}
