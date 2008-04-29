using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot
{
    public class Log : List<LogEntry>
    {
        public LogEntry LastReceived
        {
            get
            {
                for (int count = Count - 1; count >= 0; count--)
                {
                    LogEntry entry = this[count];
                    if (!entry.IsSourceSelf)
                        return entry;
                }
                return null;
            }            
        }
    }
}
