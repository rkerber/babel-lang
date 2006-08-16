using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot
{
    public class LogEntry
    {
        public DateTime Timestamp;
        public string Source;
        public string Message;

        public LogEntry(string source, string message)
        {
            Timestamp = DateTime.Now;
            Source = source;
            Message = message;
        }
    }
}
