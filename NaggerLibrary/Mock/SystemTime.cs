using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary.Mock
{
    /// <summary>
    /// This is an Function which always returns SystemTime.Now.Invoke() except....!
    /// when you override it with a new Function as follows:
    /// SystemTime.Now = () => DateTime.Parse("My Date and Time string, e.g. 19/12/1953 09:00");
    /// You can also access this using a configuration setting in the following way:
    /// SystemTime.Now = () => DateTime.Parse(ConfigurationManager.AppSettings["DebugSystemTimeOverride"]);
    /// 
    /// </summary>
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
