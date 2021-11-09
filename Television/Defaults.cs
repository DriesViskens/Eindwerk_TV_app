using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Television
{
    class Defaults
    {
        public enum Sources
        {
            Cable_TV = 1,
            VGA = 2,
            HDMI_1 = 3,
            HDMI_2 = 4
        }

        public int MinVolume = 1;
        public int MaxVolume = 10;
        public int DefaultVolume = 3;

        public int MinChannel = 1;
        public int MaxChannel = 10;
        public int DefaultChannel = 1;

        public int DefaultSource = 1;


    }
}
