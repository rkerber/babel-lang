using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Babel
{
	public class Verb: Word
	{
		public Verb(string text) : base(text)
		{
		}

        private Tense tense;
        public Tense Tense
        {
            get { return tense; }
            set { tense = value; }
        }
        
        private Noun subject;
        public Noun Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public bool SubjectPlural
        {
            get { return (Subject != null && (Subject.Plural || (Subject.List != null && Subject.List.Count > 0))); }
        }

        private List<Adverb> adverbs;
        public List<Adverb> Adverbs
        {
            get { return adverbs; }
            set { adverbs = value; }
        }

        private Word verbObject;
        public Word Object
        {
            get { return verbObject; }
            set { verbObject = value; }
        }

		public override void Write(StringBuilder AOutput)
		{
			base.Write(AOutput);
			foreach (Adverb LAdverb in adverbs)
				LAdverb.Write(AOutput);
			AOutput.Append("(");

            if (verbObject != null)
            {
				AOutput.Append(", ");
				verbObject.Write(AOutput);
            }

			AOutput.Append(")");
		}

        public override void SetSuffix(string suffix)
        {
            if (suffix == "-")
                Tense = Tense.Past;
            else if (suffix == "+")
                Tense = Tense.Future;
        }
	}

    //public enum VerbType
    //{
    //    Question,
    //    BooleanQuestion,
    //    NoSubject,
    //    NounSubject,
    //    VerbSubject,
    //    AdjectiveSubject
    //}
}