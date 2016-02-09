using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary.Cards
{
    public class Must : Card
    {

        public Must()
        {
            ColumnID = (int)ColumnType.colMust;
        }

        public override void ProcessTransition()
        {
          
        }
    }
}
