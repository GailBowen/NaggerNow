using log4net;
using NaggerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using NaggerLibrary.Mock;



namespace NaggerConsole
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main()
        {
            ProcessCards();

            Console.ReadLine();
        }
        
        public static void ProcessCards()
        {
            var ndl = new NaggerDataLinker();

            List<Card> cards = ndl.GetCardCollection();

            foreach (var card in cards)
            {
                card.ColumnID = (int)card.AssignColumn();
                ndl.Update(card);
            }

            Console.WriteLine("Could Do: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colCould));
            Console.WriteLine("Should Do: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colShould));
            Console.WriteLine("Must Do: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colMust));
            Console.WriteLine("Done: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colDone));
            Console.WriteLine("Skipped: {0}", cards.Count(c => c.AssignColumn() == ColumnType.colSkip));

        }

    }
}
