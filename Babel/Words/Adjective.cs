using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Babel
{
	public class Adjective: Word
	{
		public Adjective(string text) : base(text)
		{
		}

        private Word restictiveClauseSubject;
        public Word RestictiveClauseSubject
        {
            get { return restictiveClauseSubject; }
            set { restictiveClauseSubject = value; }
        }

		public override void Write(StringBuilder AOutput)
		{
			AOutput.Append(" ");
			base.Write(AOutput);
		}
	}
}