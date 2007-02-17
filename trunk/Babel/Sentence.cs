using System;

namespace Babel
{
	public class Sentence : Statement
	{
		public Sentence(Verb verbPhrase) : base(verbPhrase)
		{
		}

        public override bool IsQuestion
        {
            get { return false; }
        }
    }
}