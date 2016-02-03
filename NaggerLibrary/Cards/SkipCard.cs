using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class SkipCard: Card
    {
        public new void ProcessTransition()
        {
            LastSkip = SystemTime.Now.Invoke().Date;
            ColumnID = (int)ColumnType.colSkip;
            PreviousDueDate = DueDate;
            DueDate = DueDate.AddDays(Frequency);
        }
    }
}

