using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary.Cards
{
    public class Should : Card
    {
        public Should()
        {
            ColumnID = (int)ColumnType.colShould;
        }

        public override void ProcessTransition(string fromColumn, ICard penultimateAction)
        {
            ColumnID = (int)ColumnType.colShould;

            Undo(fromColumn, penultimateAction);
        }
    }
}
