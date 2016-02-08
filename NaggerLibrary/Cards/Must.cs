using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary.Cards
{
    public class Must : Card
    {

        public override void ProcessTransition()
        {
            Description = "Must Do Card";
        }
    }
}
