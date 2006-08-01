using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Babel
{
	public class Adverb: Word
	{
		public Adverb(string text) : base(text)
		{
		}

        private Word prepositionSubject;
        public Word PrepositionSubject
        {
            get { return prepositionSubject; }
            set { prepositionSubject = value; }
        }

        public override void Write(StringBuilder AOutput)
		{
			AOutput.Append(" ");
			base.Write(AOutput);
		}
	}
}