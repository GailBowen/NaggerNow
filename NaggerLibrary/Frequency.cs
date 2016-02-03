using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary
{
    public enum Frequency
    {
        Specific = 0,
        Daily = 1,
        EOD = 2,
        Weekly = 7,
        EOW = 14,
        Monthly = 30,
        EOM = 60,
        NowAndThen = 180,
        Yearly = 365
    }
}
