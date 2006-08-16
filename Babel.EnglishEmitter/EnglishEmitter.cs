using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Babel.EnglishEmitter
{
	public static class EnglishEmitter
	{
		public static string ToEnglish(Statement statement)
		{
            string result = ToEnglish(statement.Verb);

			if (statement is Question)
            {
                if (result.StartsWith("you "))
                    result = result.Substring("you ".Length);
                result += "?";
            }
            else
                result += ".";

            // capitalize first letter of sentances.
            if (result.Length > 0)
            {
                char[] temp = result.ToCharArray();
                temp[0] = temp[0].ToString().ToUpper()[0];
                result = new String(temp);
            }

			return result;
		}

        public static string ToEnglish(Adjective adjective)
        {
            if (adjective.RestictiveClauseSubject == null)
                return adjective.Text;
            else
                return adjective.Text + " " + ToEnglish(adjective.RestictiveClauseSubject);
        }

        public static string ToEnglish(Noun noun)
		{
			StringBuilder result = new StringBuilder();

            string word = noun.Text;

            if (noun.Gerund)
                word = WordMorpher.GerundNoun(word);
            else
            {
                if (noun.Adjectives != null)
			        foreach (Adjective adjective in noun.Adjectives)
                        if (adjective.RestictiveClauseSubject == null)
				            result.AppendFormat("{0} ", ToEnglish(adjective));
            }
                        
            if (noun.Plural)
                result.Append(WordMorpher.PluralNoun(word).ToLower());
            else
            {
                if (noun.Text == "I")
                    result.Append(word);
                else                
                    result.Append(word.ToLower());
            }

            if (noun.List != null)
            {
                for (int count = 0; count < noun.List.Count; count++)
                {
                    if (count != noun.List.Count - 1)
                        result.Append(", " + ToEnglish(noun.List[count]));
                    else
                        result.Append((noun.ListMode == ListMode.And ? " and " : " or ") + ToEnglish(noun.List[count]));
                }
            }

            if (noun.Adjectives != null)
            {
                if (noun.Gerund)
                {
		            foreach (Adjective adjective in noun.Adjectives)
		                result.AppendFormat(" {0}", ToEnglish(adjective));
                }
                else
                {
		            foreach (Adjective adjective in noun.Adjectives)
                        if (adjective.RestictiveClauseSubject != null)
			                result.AppendFormat(" {0}", ToEnglish(adjective));
                }
            }

            return result.ToString();
		}

        public static string ToEnglish(Adverb adverb)
        {
            // TODO: only add the -ly for adjectives that have been used as adverbs
            // for example "quickly", but not "well" or "wrong"
            if (adverb.PrepositionSubject == null)
            {
                return WordMorpher.AddSufixToAdverbsDerivedFromAdjectives(adverb.Text);
            }
            else
                return adverb.Text + " " + ToEnglish(adverb.PrepositionSubject);
        }

        public static string ToEnglish(Word word)
        {
            Verb verb = word as Verb;
            if (verb != null)
                return ToEnglish(verb);
            Noun noun = word as Noun;
            if (noun != null)
                return ToEnglish(noun);
            return word.Text;
        }

        public static string ToEnglish(Verb verb)
        {
			StringBuilder result = new StringBuilder();
            if (verb.Subject != null)
                result.Append(ToEnglish(verb.Subject) + " ");

            switch (verb.Tense)
            {
                case Tense.Past:
                    {
                        if (verb.Text == "is" && verb.SubjectPlural)
                            result.Append("were");
                        else
                            result.Append(WordMorpher.PastTenseVerb(verb.Text));
                    }
                    break;
                case Tense.Present:
                    {
                        if (verb.SubjectPlural)
                            result.Append(WordMorpher.PluralVerb(verb.Text));
                        else
                        {
                            if (verb.Text == "is" && verb.Subject.Text == "I")
                                result.Append("am");
                            else
                                result.Append(verb.Text);
                        }
                    }
                    break;
                case Tense.Future:
                    result.Append(WordMorpher.FutureTenseVerb(verb.Text));
                    break;
            }

            if (verb.Adverbs != null)
			    foreach (Adverb adverb in verb.Adverbs)
                    if (adverb.PrepositionSubject == null)
    				    result.AppendFormat(" {0}", ToEnglish(adverb));

            if (verb.Object != null)
            {
                if (verb.Object is Noun)
                    result.AppendFormat(" {0}", ToEnglish((Noun)verb.Object));
                if (verb.Object is Verb)
                    result.AppendFormat(" {0}", ToEnglish((Verb)verb.Object));
                if (verb.Object is Adjective)
                    result.AppendFormat(" {0}", ToEnglish((Adjective)verb.Object));
            }

            if (verb.Adverbs != null)
			    foreach (Adverb adverb in verb.Adverbs)
                    if (adverb.PrepositionSubject != null)
    				    result.AppendFormat(" {0}", ToEnglish(adverb));

            return result.ToString();
        }
	}
}