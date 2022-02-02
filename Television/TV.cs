using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Television
{
    public class TV : INotifyPropertyChanged
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

        private List<string> _Logfile = new List<string>();
        public List<String> Logfile
        {
            get { return _Logfile; }
            set
            {
                _Logfile = value;
                RoepPropertyChangedOp();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RoepPropertyChangedOp([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TV()
        {

            MinVolume = Defaults.MinVolume;
            MaxVolume = Defaults.MaxVolume;
            this.GetSources();
            this.GetChannels();

        }

        List<Command> commandsList = new List<Command>();

        public void StartUp()
        {
            if (!Active)
            {
                Active = true;
                Volume = Defaults.DefaultVolume;
                Channel = Defaults.DefaultChannel;
                Source = Defaults.DefaultSource;
                AddToLogfile($"Starting up TV.");
            }
        }
        public void ShutDown()
        {
            if (this.Active)
            {
                this.Active = false;
                AddToLogfile($"Shutting down TV.");
            }
        }
        public void VolumeUp()
        {
            if (this.Active)
            {
                if (this.Volume < MaxVolume)
                {
                    this.Volume++;
                    AddToLogfile($"Volumed up to {this.Volume}.");
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
                    AddToLogfile($"Volumed down to {this.Volume}.");
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
                AddToLogfile($"Changed channel to {(Defaults.Channels)this.Channel}.");
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
                AddToLogfile($"Setting channel to {(Defaults.Channels)this.Channel}");
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
                AddToLogfile($"Changed channel to {(Defaults.Channels)this.Channel}.");
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
                AddToLogfile($"Changed source to {(Defaults.Sources)this.Source}.");
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
                AddToLogfile($"Changed source to {(Defaults.Sources)this.Source}.");
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
        private void AddToLogfile(string text)
        {
            Logfile.Add(text);
            SendBroadcast.data = Encoding.ASCII.GetBytes(text);
            SendBroadcast.sendb();
            RoepPropertyChangedOp("Logfile");
            RoepPropertyChangedOp();
        }



    }

}

