using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Television
{
    public sealed class Worker
    {
        private static readonly Lazy<Worker> lazy = new Lazy<Worker>(() => new Worker());
        // zorgt ervoor dat slechts 1 object kan gemaakt worden
        // lazy zorgt controleert dat op geen enkele thread een worker bestaat
        // nieuwe worker wordt in de lazy aangemaakt
        public static Worker Instance { get { return lazy.Value; } }
        public bool TvIsOn { get; set; }
        BackgroundWorker worker = new BackgroundWorker();
        private Worker()
        {
            Console.WriteLine("test");
            worker.DoWork += Worker_DoWork;
        }

        /// <summary>
        /// Execute this on when u set TvIsOn to true.
        /// </summary>
        public void StartWorking()
        {
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (TvIsOn)
            {
                Console.WriteLine("test");
                // Code To get the required action for your tv.
                // Code To get the required action for your tv.
                // Code To get the required action for your tv.
            }
        }
    }
}
