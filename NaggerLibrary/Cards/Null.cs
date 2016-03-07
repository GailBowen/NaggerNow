using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary.Cards
{
    public class Null: Card
    {
        public override void ProcessTransition(string fromColumn, ICard penultimateAction)
        { }
    }
}
