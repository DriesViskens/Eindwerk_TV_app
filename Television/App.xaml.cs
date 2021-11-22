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
        SqlRepository SqlRep = new SqlRepository();
        private static Timer buttonTimer;
        private static bool timeOut = false;
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
            SetTimer();
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
            commands = SqlRep.GetFirstThree();
            InterpreteDbCommands(commands);
        }
        private int InterpreteDbCommands(List<Command> Commands)
        {
            int sendCommand = 0;
            List<int> intCommands = TranslateCommand(Commands);

            if (intCommands[0] < 10)
            {
                sendCommand = NumberInput(intCommands, Commands);
            }

            else if (intCommands[0] == 255)
            {
                Debug.WriteLine("ongeldige invoer");
            }
            else
            {
                sendCommand = intCommands[0] + 1000;
            }
            Debug.WriteLine(sendCommand);

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
            List<int> NewList = new List<int>();
            for (int i = 0; i < Commands.Count; i++)
            {
                NewList.Add(TranslateCommand(Commands[i]));
            }
            return NewList;
        }
        private int NumberInput(List<int> intcommands, List<Command> commands)
        {
            int value = -1;
            buttonTimer.Enabled = true;

            switch (intcommands.Count)
            {
                case 1:
                    Debug.WriteLine(DateTime.Now.Subtract(commands[0].DT).TotalSeconds);
                    if (DateTime.Now.Subtract(commands[0].DT).TotalSeconds > 3 || timeOut)
                    {
                        Debug.WriteLine("1 waarde");
                        value = intcommands[0];
                    }
                    break;
                case 2:
                    Debug.WriteLine(DateTime.Now.Subtract(commands[0].DT).TotalSeconds);
                    if (intcommands[1] < 10)
                    {
                        if (DateTime.Now.Subtract(commands[0].DT).TotalSeconds > 3 || timeOut)
                        {
                            Debug.WriteLine("2 waarde");
                            value = intcommands[0] * 10 + intcommands[1];
                        }
                    }
                    if (intcommands[1] > 10)
                    {
                        Debug.WriteLine("2 waarde");
                        value = intcommands[0];
                        buttonTimer.Enabled = false;
                    }
                    break;
                case 3:
                    Debug.WriteLine(DateTime.Now.Subtract(commands[0].DT).TotalSeconds);
                    if (intcommands[1] < 10 && intcommands[2] < 10)
                    {
                        Debug.WriteLine("3 waarde");
                        value = intcommands[0] * 100 + intcommands[1] * 10 + intcommands[2];
                    }
                    else if (intcommands[1] < 10 && intcommands[2] >= 10)
                    {
                        value = intcommands[0] * 10 + intcommands[1];
                    }
                    else if (intcommands[1] >= 10)
                    {
                        value = intcommands[0];

                    }
                    buttonTimer.Enabled = false;
                    break;

                default:
                    break;
            }
            if (value >= 0)
            {
                buttonTimer.Enabled = false;
            }

            return value;
        }
        private void SetTimer()
        {
            Debug.WriteLine("starttimer");
            // Create a timer with a three second interval.
            buttonTimer = new System.Timers.Timer(3000);
            // Hook up the Elapsed event for the timer. 
            buttonTimer.Elapsed += OnTimedButtonEvent;
            buttonTimer.AutoReset = false;
            //buttonTimer.Enabled = true;
        }
        private void OnTimedButtonEvent(Object source, ElapsedEventArgs e)
        {
            timeOut = true;
            DbChanged();
            buttonTimer.Enabled = false;
            timeOut = false;
        }

    }
}
