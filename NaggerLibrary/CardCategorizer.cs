using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary
{
    abstract class CardCategorizer
    {
       
        public CardCategorizer RegisterNext(CardCategorizer next)
        {
            Next = next;
            return Next;
        }

        protected CardCategorizer Next { get; private set; }

        public abstract ColumnType Categorize(Card card);


        protected static bool DoneToday(Card card)
        {
            if (card.LastDone.Date == DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected static bool SkippedToday(Card card)
        {
            if (card.LastSkip.Date == DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected static bool MustBeDoneToday(Card card)
        {
            if (card.Mandated && card.LastDone < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        protected static bool ShouldDoToday(Card card)
        {
            if (card.DueDate.Date <= DateTime.Now.Date && card.SkipCount < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        protected static bool CouldDoToday(Card card)
        {
            if (card.DueDate.Date > DateTime.Now.Date && card.SkipCount < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
