using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class Skip: Card
    {
        public Skip()
        {
            ColumnID = (int)ColumnType.colSkip;
        }

        public override void ProcessTransition(string fromColumn, ICard penultimateAction)
        {
            LastSkip = SystemTime.Now.Invoke().Date;
            ColumnID = (int)ColumnType.colSkip;
            DueDate = DueDate.AddDays(FrequencyID);
            SkipCount += 1;
        }
    }
}

