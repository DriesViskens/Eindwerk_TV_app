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
            Cable_TV,
            VGA,
            HDMI_1,
            HDMI_2
        }
        public enum Channels
        {
            vtmHD,
            één,
            VIER,
            Canvas,
            Q2,
            VIJF,
            Vitaya,
            Regionaal,
            CAZ,
            KanaalZ,
            PlayTime,
            Ketnet,
            Fox,
            ZES,
            Discovery,
            NatGeo,
            ComedyCentral,
            MTV,
            Njam,
            Viceland,
            PlattelandsTV,
            BBC,
            Cadet
        }



        public static int MinVolume = 1;
        public static int MaxVolume = 10;
        public static int DefaultVolume = 3;

        public static int DefaultChannel = 1;

        public static int DefaultSource = 0;


    }
}
