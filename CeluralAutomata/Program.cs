using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;

namespace OpenTk
{
    static class Program
    {
        public static Random rnd = new Random();
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new MainWindow().Run(60,0);
        }
    }
}
