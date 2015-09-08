using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumixGH4WIC
{
    public static class Log
    {
        static readonly string application = "GH4RW2";
        static Log()
        {
            if (!EventLog.SourceExists(application))
                EventLog.CreateEventSource(application, "Application");
        }
        public static void Trace(string log)
        {
#if TRACE
            EventLog.WriteEntry(application, log);
#endif
        }

        public static void Debug(string log)
        {
            EventLog.WriteEntry(application, log);
        }

        public static void Error(string log)
        {
            EventLog.WriteEntry(application, log, EventLogEntryType.Error);
        }
    }
}
