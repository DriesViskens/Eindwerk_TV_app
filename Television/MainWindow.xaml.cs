using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace Television
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecvBroadcst Receiver = new RecvBroadcst();
        TV tv = new TV();
        SqlRepository SqlRep = new SqlRepository();
        private static Timer buttonTimer;
        private static bool timeOut = false;
        private static List<int> longchannel = new List<int>();
        

        public MainWindow()
        {
            InitializeComponent();

            onDbChanged();
            SetTimer();
            Thread t = new Thread(CheckData);
            t.Start();
        }

        public void CheckData()
        {
            while (true)
            {
                if (RecvBroadcst.receivedinputcommands.Count() != 0)
                {
                    byte[] received = RecvBroadcst.receivedinputcommands.First();
                    RecvBroadcst.receivedinputcommands.Remove(received);                    
                    if (received[0] == 1 ) // 1 = from remote
                    {
                        debuginfo($"from socket:{received[1]}");
                        InterpreteDbCommand(received[1].ToString());
                    }
                }
            }
        }
        private void OnOff_Click(object sender, RoutedEventArgs e)
        {
            if (tv.Active)
            {
                tv.ShutDown();
                OnOff.Background = new SolidColorBrush(Colors.Red);
                DispCh.Content = "--";
                DispVol.Content = "--";
                DispSrc.Content = "--";

            }
            else
            {
                tv.StartUp();
                OnOff.Background = new SolidColorBrush(Colors.Green);
                DispCh.Content = (Defaults.Channels)tv.Channel;
                DispVol.Content = tv.Volume;
                DispSrc.Content = (Defaults.Sources)tv.Source;

            }
        }
        private void VolUp_Click(object sender, RoutedEventArgs e)
        {
            tv.VolumeUp();
            Debug.WriteLine(tv.Volume);
            DispVol.Content = tv.Volume;
        }
        private void VolDown_Click(object sender, RoutedEventArgs e)
        {
            tv.VolumeDown();
            Debug.WriteLine(tv.Volume);
            DispVol.Content = tv.Volume;
        }
        private void ChUp_Click(object sender, RoutedEventArgs e)
        {
            tv.ChannelUp();
            Debug.WriteLine(tv.Channel);
            DispCh.Content = (Defaults.Channels)tv.Channel;
        }
        private void ChDown_Click(object sender, RoutedEventArgs e)
        {
            tv.ChannelDown();
            Debug.WriteLine(tv.Channel);
            DispCh.Content = (Defaults.Channels)tv.Channel;
        }
        private void SrcUp_Click(object sender, RoutedEventArgs e)
        {
            tv.SourceUp();
            DispSrc.Content = (Defaults.Sources)tv.Source;
        }
        private void SrcDown_Click(object sender, RoutedEventArgs e)
        {
            tv.SourceDown();
            DispSrc.Content = (Defaults.Sources)tv.Source;
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
            Command command = SqlRep.GetFirst();
            if(command.command != null) InterpreteDbCommand(command.command);
        }

        private void InterpreteDbCommand(string data)
        {            
            SqlRep.DeleteFirst();
            bool result = int.TryParse(data, out int tag);

            if (tag < 10)
            {
                if (buttonTimer.Enabled) buttonTimer.Enabled = false;                
                longchannel.Add(tag);
                if (longchannel.Count == 3)
                {                    
                    makeChannelSetCommand();
                    return;
                }
                buttonTimer.Enabled = true;
            }
            if (tag == 10)
            {
                OnOff_Click(this, null); // still gives an error
            }

        }
        private void OnTimedButtonEvent(Object source, ElapsedEventArgs e)
        {
            makeChannelSetCommand();     
            buttonTimer.Enabled = false;
        }
        private void makeChannelSetCommand()
        {
            int channel = 0;
            switch (longchannel.Count)
            {
                case 1:
                    channel = longchannel[0];
                    break;
                case 2:
                    channel = longchannel[0] * 10 + longchannel[1];
                    break;
                case 3:
                    channel = longchannel[0]*100+ longchannel[1] * 10 + longchannel[2];
                    break;
                default:
                    break;
            }
            tv.SetChannel(channel);
            this.Dispatcher.Invoke(() =>
            {
                DispCh.Content = (Defaults.Channels)tv.Channel;
            });
            longchannel.Clear();
        }
    
        public void debuginfo (string text)
        {
            this.Dispatcher.Invoke(() =>
            {
                tbo_info.AppendText(text + "\r\n");
            });
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
        
    }
}
