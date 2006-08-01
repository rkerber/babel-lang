using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Babel.EnglishEmitter
{
    public static class CollectionLoader
    {
        public static StringCollection LoadCollection(string source)
        {
            StringCollection result = new StringCollection(); 
            foreach (string line in source.Split('\n'))
            {
                string trimmedLine = line.Trim();
                if (!trimmedLine.StartsWith("//") && trimmedLine != String.Empty)
                    result.Add(trimmedLine);
            }
            return result;
        }
    }
}