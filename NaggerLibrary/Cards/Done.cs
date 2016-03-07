using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class Done: Card
    {

        public Done()
        {
            ColumnID = (int)ColumnType.colDone;
        }

        public override void ProcessTransition(string fromColumn, ICard penultimateAction)
        {
            ColumnID = (int)ColumnType.colDone;
            LastDone = SystemTime.Now.Invoke().Date;
            DueDate = LastDone.AddDays(FrequencyID);
        }
    }
}
