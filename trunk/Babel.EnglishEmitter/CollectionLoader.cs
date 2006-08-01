using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Babel.EnglishEmitter
{
    public static class DictionaryLoader
    {
        public static StringDictionary LoadDictionary(string source)
        {
            StringDictionary result = new StringDictionary(); 
            foreach (string line in source.Split('\n'))
            {
                if (!line.StartsWith("//"))
                {
                    string[] list = line.TrimEnd('\n', '\r').Split(' ');
                    if (list.Length >= 2)
                        result.Add(list[0], list[1]);
                }
            }
            return result;
        }
    }
}