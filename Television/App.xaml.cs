using System;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;



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

            //	SqlRep.CheckDatabaseOnchange();
            onDbChanged();
        }
        public void onDbChanged()
        {

            string connectionString = Defaults.DbConnString;
            var changeListener = new DatabaseChangeListener(connectionString);

            changeListener.OnChange += () =>
            {
                changeListener.Start(@"SELECT [button] FROM [dbo].[Commands]");
                DbChanged();
            };
            changeListener.Start(@"SELECT [button] FROM [dbo].[Commands]");

        }
        private void DbChanged()
        {
            List<Command> commands = new List<Command>();
            commands = SqlRep.GetAll();
            InterpreteDbCommands(commands);
        }
        private int InterpreteDbCommands(List<Command> Commands)
        {
            int sendCommand=0;
            List<int> intCommands = TranslateCommand(Commands);
            for (int i = 0; i < Commands.Count; i++)
            {
                if (intCommands[i] < 10 && i != Commands.Count-1)
                {
                    double difference = ((Commands[i+1].DT).Subtract(Commands[i].DT)).TotalSeconds;

                    if (difference<3)
                    {
                        sendCommand = intCommands[i] * 10 + intCommands[i + 1];
                        i++;
                    }
                    else
                    {
                        sendCommand = intCommands[i];
                    }
                              
                }
                else if (intCommands[i] == 255)
                {
                    Debug.WriteLine("ongeldige invoer");
                    break;
                }
                else
                {
                    sendCommand = intCommands[i] + 1000;
                }
                Debug.WriteLine(sendCommand);
            }
           
            return sendCommand;

        }

        private int TranslateCommand(Command Command)
        {

            int Result;
            bool succes = int.TryParse(Command.command, out Result);
            if (succes)
            {
                return Result;
            }
            return 255;

        }
        private List<int> TranslateCommand(List<Command> Commands)
        {
            List<int> NewList =new List<int>();
            for (int i = 0; i < Commands.Count; i++)
            {
                NewList.Add(TranslateCommand(Commands[i]));
            }
            return NewList;
        }

    }
}
