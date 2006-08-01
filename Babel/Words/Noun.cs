using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Babel
{
	public class Noun: Word
	{
		public Noun(string text) : base(text)
		{
		}

        private bool plural;
        public bool Plural
        {
            get { return plural; }
            set { plural = value; }
        }

        private bool gerund;
        public bool Gerund
        {
            get { return gerund; }
            set { gerund = value; }
        }

        private List<Adjective> adjectives;
        public List<Adjective> Adjectives
        {
            get { return adjectives; }
            set { adjectives = value; }
        }

        private List<Noun> list;
        public List<Noun> List
        {
            get { return list; }
            set { list = value; }
        }

        private ListMode listMode;
        public ListMode ListMode
        {
            get { return listMode; }
            set { listMode = value; }
        }

		public override void Write(StringBuilder AOutput)
		{
			base.Write(AOutput);
			foreach (Adjective LAdjective in adjectives)
				LAdjective.Write(AOutput);
		}

        public override void SetSuffix(string suffix)
        {
            if (suffix == "*")
                plural = true;
            if (suffix == "=")
                gerund = true;
        }
	}
}