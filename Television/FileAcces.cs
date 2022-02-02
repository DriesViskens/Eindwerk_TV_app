using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Television
{
    class FileAcces
    {
        public int defVol = -1;
        public int defCh = -1;
        public int defSrc = -1;

        public void ReadFile()
        {
            if (File.Exists(@"C:\temp\defaults.txt"))
            {
                string[] data = File.ReadAllLines(@"C:\temp\defaults.txt");
                defVol = GetNumber(data[0]);
                defCh = GetNumber(data[1]);
                defSrc = GetNumber(data[2]);
            }
        }

        public void WriteFile(int Vol, int Ch, int Src)
        {
            File.Create(@"C:\temp\defaults.txt").Close();

            File.AppendAllText(@"C:\temp\defaults.txt",
                Vol + Environment.NewLine +
                Ch + Environment.NewLine +
                Src
                );
        }

        static int GetNumber(string numberstring)
        {
            bool succes = false;
            int getal;

            succes = int.TryParse(numberstring, out getal);
           if (!succes)
            {
                getal = -1;
            }

            return getal;
        }
    }
}
