using System;
using System.Collections.Generic;
using System.Text;

using Babel;

namespace ChatterBot
{
    public class LogEntry
    {
        public DateTime Timestamp;
        public string Source;
        public string Message;

        static Parser parser = new Babel.Parser();

        public LogEntry(string source, string message)
        {
            Timestamp = DateTime.Now;
            Source = source;
            Message = message;
        }

        public bool SourceSelf
        {
            get { return Source == "Self"; }
        }

        Statement parsed;
        public Statement Parsed
        {
            get
            {
                if (parsed == null)
                    parsed = parser.ParseSource(Message)[0];
                return parsed;
            }
        }

        public bool IsQuestion
        {
            get
            {
                return Parsed.IsQuestion;
            }
        }
    }
}