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
        //public static random object for general usage
        public static Random rnd;
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        { 
           rnd = new Random(Guid.NewGuid().GetHashCode());

           new MainWindow().Run();
        }
    }
}
