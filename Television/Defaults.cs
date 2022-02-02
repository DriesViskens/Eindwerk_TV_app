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
            Cadet,p23,p24,p25,p26,p27,p28,p29,p30,p31,p32,p33,p34,p35,p36,p37,p38,p39,p40,p41,p42,p43,p44,p45,p46,p47,p48,p49,p50,p51,p52,p53,p54,p55,p56,p57,p58,p59,p60,
            p61,p62,p63,p64,p65,p66,p67,p68,p69,p70,p71,p72,p73,p74,p75,p76,p77,p78,p79,p80,p81,p82,p83,p84,p85,p86,p87,p88,p89,p90,p91,p92,p93,p94,p95,p96,p97,p98,p99,p100,p101
        }

        public static int MinVolume = 1;
        public static int MaxVolume = 10;
        public static int DefaultVolume = 3;
        public static int DefaultChannel = 1;
        public static int DefaultSource = 0;

        public static string DbConnString = "Data Source=localhost; Initial Catalog=Television; User ID=sa; Password=1234";
       // public static string DbConnString = "Data Source=localhost; Initial Catalog=Television; User ID=sa; Password=syntrawest1234A";

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
