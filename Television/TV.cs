using System;
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
        private int MinChannel { get; set; }
        private int MaxChannel { get; set; }
        
        public TV()
        {
            Defaults def = new Defaults();
            MinVolume = def.MinVolume;
            MaxVolume = def.MaxVolume;
            Volume = def.DefaultVolume;
            MinChannel = def.MinChannel;
            MaxChannel = def.MaxChannel;
            Channel = def.DefaultChannel;
            Source = def.DefaultSource;
        }

        public void StartUp()
        {
            if (!Active)
            {
                Active = true;
            }
        }
        public void ShutDown()
        {
            if(this.Active)
            {
                this.Active = false;
            }
        }
        private void SetDefaultValues()
        {
            this.Volume = 10;
            this.Channel = 1;
        }
        public void VolumeUp()
        {
            if (this.Active)
            {
                if (this.Volume<MaxVolume)
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
                if (this.Channel == this.MaxChannel)
                {
                    this.Channel = this.MinChannel;
                }
                else if (this.Channel < this.MaxChannel)
                {
                    this.Channel++;
                }
            }
        }
        public void ChannelDown()
        {
            if (this.Active)
            {
                if (this.Channel == this.MinChannel)
                {
                    this.Channel = this.MaxChannel;
                }
                else if (this.Channel > this.MinChannel)
                {
                    this.Channel--;
                }
            }

        }
        public void ChangeSource()
        {
            Defaults.Sources def = Defaults.Sources.Cable_TV;
            Debug.WriteLine(def);
        }


    }

}
