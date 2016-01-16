using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;
using NaggerLibrary;

namespace NaggerConsole
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main()
        {
            //Timer aTimer = new Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(ProcessCards);
            //aTimer.Interval = 5000;
            //aTimer.Enabled = true;

            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;

            ProcessCards();

            Console.ReadLine();
        }

      
        public static void ProcessCards(object source, ElapsedEventArgs e)
        {
            ProcessCards();
        }

        public static void ProcessCards()
        {
            NaggerProvider.FetchCards();
        }

    }
}
