﻿using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Television
{
    class TV
    {

        public bool Active { get; private set; }        // TV is on??
        public int Source { get; private set; }         // Input source (Cable TV, VGA, HDMI,...)
        public int Channel { get; private set; }        // Actual TV Channel
        public int Volume { get; private set; }         // Actual Volume
        public int Communication { get; private set; }  // Art of communication
        private int MinVolume { get; set; }
        private int MaxVolume { get; set; }
        private int SourceCount { get; set; }
        private int ChannelCount { get; set; }

       
        private static Timer timer = new Timer();
        public TV()
        {
            MinVolume = Defaults.MinVolume;
            MaxVolume = Defaults.MaxVolume;
            this.GetSources();
            this.GetChannels();
        }
        public SqlRepository SqlRep = new SqlRepository();
        List<Command> commandsList = new List<Command>();

        public void CheckDatabaseOnchange()
        {
            string connectionString = Defaults.DbConnString;
            var changeListener = new DatabaseChangeListener(connectionString);
            changeListener.OnChange += () =>
            {
                changeListener.Start(@"SELECT [button] FROM [dbo].[Commands]");
                ReadRemoteDbCommands();
            };
            changeListener.Start(@"SELECT [button] FROM [dbo].[Commands]");
        }
        private void ReadRemoteDbCommands()
        {
            Debug.WriteLine("There was a change");
            Command test = SqlRep.GetFirst();
            Debug.WriteLine(test.command);
            Debug.WriteLine(test.DT);
            commandsList.Add(test);
            this.CommandTimer();

        }
        public void StartUp()
        {
            if (!Active)
            {
                Active = true;
                Volume = Defaults.DefaultVolume;
                Channel = Defaults.DefaultChannel;
                Source = Defaults.DefaultSource;
            }
        }
        public void ShutDown()
        {
            if (this.Active)
            {
                this.Active = false;
            }
        }
        public void VolumeUp()
        {
            if (this.Active)
            {
                if (this.Volume < MaxVolume)
                {
                    this.Volume++;
                }
            }
        }
        public void VolumeDown()
        {
            if (this.Active)
            {
                if (this.Volume > this.MinVolume)
                {
                    this.Volume--;
                }
            }

        }
        public void ChannelUp()
        {
            if (this.Active)
            {
                if (this.Channel == this.ChannelCount - 1)
                {
                    this.Channel = 0;
                }
                else if (this.Channel < this.ChannelCount - 1)
                {
                    this.Channel++;
                }
            }
        }
        public void ChannelDown()
        {
            if (this.Active)
            {
                if (this.Channel == 0)
                {
                    this.Channel = this.ChannelCount - 1;
                }
                else if (this.Channel > 0)
                {
                    this.Channel--;
                }
            }

        }
        public void SourceUp()
        {
            if (this.Active)
            {
                if (this.Source == this.SourceCount - 1)
                {
                    this.Source = 0;
                }
                else if (this.Source < this.SourceCount - 1)
                {
                    this.Source++;
                }
            }
        }
        public void SourceDown()
        {
            if (this.Active)
            {
                if (this.Source == 0)
                {
                    this.Source = this.SourceCount - 1;
                }
                else if (this.Source > 0)
                {
                    this.Source--;
                }
            }
        }
        private void GetSources()
        {
            this.SourceCount = Enum.GetValues(typeof(Defaults.Sources)).Length;
        }
        private void GetChannels()
        {
            this.ChannelCount = Enum.GetValues(typeof(Defaults.Channels)).Length;
        }
        public void CommandTimer()
        {

            timer.Interval = 3000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = false;
            timer.Enabled = true;

        }
        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Debug.WriteLine("timer");
            CommandFlush();
            timer.Elapsed -= OnTimedEvent;
        }
        private static void CommandFlush()
        {
            
        }
    }

}

