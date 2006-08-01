using System;
using System.Collections.Specialized;

namespace Babel
{
	public class Question : Statement
	{
        public Question(Verb verbPhrase) : base(verbPhrase)
		{
            boolean = IsInterrogative(verbPhrase.Text);
		}

        private bool boolean;
        public bool Boolean
        {
            get { return boolean; }
            set { boolean = value; }
        }

#region Static Methods
        private static StringCollection interrogatives = new StringCollection();
        private static StringCollection booleanInterrogatives = new StringCollection();

        static Question()
        {
            interrogatives.AddRange
            (
                new String[]
                {
                    "who",
                    "which",
                    "what",
                    "when",
                    "where",
                    "why",
                    "whose"
                }
            );
            booleanInterrogatives.AddRange
            (
                new String[]
                {
                    "is",
                    "can",
                    "do",
                    "have",
                    "has",
                    "are",
                    "will",
                    "did",
                    "may"
                }
            );
        }

        public static bool IsInterrogative(string text)
        {
            return interrogatives.Contains(text) || booleanInterrogatives.Contains(text);
        }

        public static bool IsBooleanInterrogative(string text)
        {
            return booleanInterrogatives.Contains(text);
        }
#endregion
    }
}