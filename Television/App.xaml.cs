using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace Television
{
	
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
		SqlRepository SqlRep = new SqlRepository();

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			// Create the startup window
			MainWindow wnd = new MainWindow();
			// Do stuff here, e.g. to the window
			wnd.Title = "Televisie";
			// Show the window
			wnd.Show();
			
			SqlRep.CheckDatabaseOnchange();
			SqlRep.DbChanged += DbChanged;
			SqlRep.StartProcess();
			

		}
		public static void DbChanged()
		{
			Console.WriteLine("Process Completed!");
		}
	}
}
