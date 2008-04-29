using System;
using System.Collections.Generic;
using System.Text;

using Babel;

namespace ChatterBot
{
    public class LogEntry
    {
        public DateTime Timestamp;
        public Source Source;
        public string Message;

        static Parser parser = new Babel.Parser();

        public LogEntry(Source source, string message)
        {
            Timestamp = DateTime.Now;
            Source = source;
            Message = message;
        }

        public bool IsSourceSelf
        {
            get { return Source == Source.Self; }
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