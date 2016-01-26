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
           List<Card> cards =  NaggerProvider.FetchCards();

           foreach (var card in cards)
           {
               card.Update();
           }

            Console.WriteLine("Could Do: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colCould));
            Console.WriteLine("Should Do: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colShould));
            Console.WriteLine("Must Do: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colMust));
            Console.WriteLine("Done: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colDone));
            Console.WriteLine("Skipped: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colSkip));

        }

    }
}
