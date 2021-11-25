using System;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using System.Timers;


namespace Television
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
  
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create the startup window
            MainWindow wnd = new MainWindow();
            // Do stuff here, e.g. to the window
            wnd.Title = "Televisie";
            // Show the window
            wnd.Show();

            //	SqlRep.CheckDatabaseOnchange();
         
        }
        

    }
}
