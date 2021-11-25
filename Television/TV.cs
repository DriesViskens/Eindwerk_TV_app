using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Threading;

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
        //private static Timer timer = new Timer();
        public TV()
        {
            
            MinVolume = Defaults.MinVolume;
            MaxVolume = Defaults.MaxVolume;
            this.GetSources();
            this.GetChannels();
            
        }

        List<Command> commandsList = new List<Command>();

        ///// commandsList.Add(test)



        public void StartUp()
        {
            if (!Active)
            {
                Active = true;
                Volume = Defaults.DefaultVolume;
                Channel = Defaults.DefaultChannel;
                Source = Defaults.DefaultSource;
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Starting up TV.");
                SendBroadcast.sendb();
            }
        }
        public void ShutDown()
        {
            if (this.Active)
            {
                this.Active = false;
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Shutting down TV.");
                SendBroadcast.sendb();
            }
        }
        public void VolumeUp()
        {
            if (this.Active)
            {
                if (this.Volume < MaxVolume)
                {
                    this.Volume++;
                    SendBroadcast.data = Encoding.ASCII.GetBytes($"Volumed up to {this.Volume}.");
                    SendBroadcast.sendb();
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
                    SendBroadcast.data = Encoding.ASCII.GetBytes($"Volumed down to {this.Volume}.");
                    SendBroadcast.sendb();
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
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Changed channel to {(Defaults.Channels)this.Channel}.");
                SendBroadcast.sendb();
            }
        }
        public void SetChannel(int channel)
        {
            if (this.Active)
            {
                if (this.ChannelCount > channel)
                {
                    this.Channel = channel;
                }
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Setting channel to {(Defaults.Channels)this.Channel}");
                SendBroadcast.sendb();
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
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Changed channel to {(Defaults.Channels)this.Channel}.");
                SendBroadcast.sendb();
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
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Changed source to {(Defaults.Sources)this.Source}.");
                SendBroadcast.sendb();
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
                SendBroadcast.data = Encoding.ASCII.GetBytes($"Changed source to {(Defaults.Sources)this.Source}.");
                SendBroadcast.sendb();
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
       
      
 
    }

}

