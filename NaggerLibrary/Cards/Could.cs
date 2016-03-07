using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class Could: Card
    {
        public Could()
        {
            ColumnID = (int)ColumnType.colCould;
        }


        public override void ProcessTransition(string fromColumn, ICard penultimateAction)
        {
            ColumnID = (int)ColumnType.colCould;

            Undo(fromColumn, penultimateAction);
        }
    }
}
