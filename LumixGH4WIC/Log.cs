using System.Diagnostics;

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

        [Conditional("TRACE")]
        public static void Trace(string log)
        {
            EventLog.WriteEntry(application, log);
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
