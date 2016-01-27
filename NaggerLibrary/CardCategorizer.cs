using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Mock;
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
            if (card.LastDone.Date == SystemTime.Now.Invoke().Date)
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
            if (card.LastSkip.Date == SystemTime.Now.Invoke().Date)
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
            if (card.Mandated && card.LastDone < SystemTime.Now.Invoke())
            {
                return true;
            }

            if (card.DueDate.Date == SystemTime.Now.Invoke().Date && card.SkipCount >= 2)
            {
                return true;
            }
            
            return false;
        }


        protected static bool ShouldDoToday(Card card)
        {
            if (card.DueDate.Date <= SystemTime.Now.Invoke().Date && card.SkipCount < 2)
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
            if (card.DueDate.Date > SystemTime.Now.Invoke().Date && card.SkipCount < 2)
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
