using System;

namespace Babel
{
	public abstract class Statement
	{
		public Statement(Verb verbPhrase)
		{
			Verb = verbPhrase;
		}

        private Verb verb;
        public Verb Verb
        {
            get { return verb; }
            set { verb = value; }
        }

        public abstract bool IsQuestion
        {
            get;
        }
	}
}