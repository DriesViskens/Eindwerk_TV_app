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
            Een,
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

       // public static string DbConnString = "Data Source=localhost; Initial Catalog=Television; User ID=sa; Password=1234";
        public static string DbConnString = "Data Source=localhost; Initial Catalog=Television; User ID=sa; Password=syntrawest1234A";

        public enum CommandsList
        {
            Button0 = 0,
            Button1 = 1,
            Button2 = 2,
            Button3 = 3,
            Button4 = 4,
            Button5 = 5,
            Button6 = 6,
            Button7 = 7,
            Button8 = 8,
            Button9 = 9,
            ButtonOnOff = 1010,
            ButtonVolUp =1011,
            ButtonVolDo = 1012,
            ButtonChaUp = 1013,
            ButtonChaDo = 1014,
            ButtonSrcUp = 1015,
            ButtonSrcDo = 1016


        }
    }
}
