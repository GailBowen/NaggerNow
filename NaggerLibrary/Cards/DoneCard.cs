using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class DoneCard: Card
    {
        public new void ProcessTransition()
        {
            LastDone = SystemTime.Now.Invoke().Date;
            ColumnID = (int)ColumnType.colDone;
            PreviousDueDate = DueDate;
            DueDate = DueDate.AddDays(FrequencyID);
        }
    }
}
