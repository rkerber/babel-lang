using System;
using System.Collections.Generic;
using System.Text;

namespace Babel
{
    public class ParseResult: List<Statement>
    {
        public List<Exception> ErrorList = new List<Exception>();

        public float ParseTime;

        public bool Successfull
        {
            get { return ErrorList.Count == 0; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach(Exception exception in ErrorList)
            {
                result.Append(exception.ToString() + "\n");
            }

//            result.AppendFormat("Parsing the source took {0}ms", ParseTime);
            
            return result.ToString();
        }
    }
}
