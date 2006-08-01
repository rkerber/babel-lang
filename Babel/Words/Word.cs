using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Babel
{
	public abstract class Word
	{
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
		public int SenseID = -1;

		public Word(string wordText)
		{
			text = wordText;
		}

		public virtual void Write(StringBuilder AOutput)
		{
			AOutput.Append(text);
			if (SenseID != -1)
				AOutput.AppendFormat("[{0}]", SenseID);
		}

		public override string ToString()
		{
			return text;
		}

        public virtual void SetSuffix(string suffix)
        {
        }
	}
}