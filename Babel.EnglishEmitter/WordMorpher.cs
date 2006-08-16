using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Babel.EnglishEmitter
{
	public static class WordMorpher
	{
        private static bool EndsWithAny(string word, string[] values)
        {
            foreach (string value in values)
                if (word.EndsWith(value))
                    return true;
            return false;
        }

        private static List<char> consonants = new List<char>(new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' });

        private static bool EndsWithSingleConsonant(string word)
        {
            return word.Length >= 2 && consonants.Contains(word[word.Length - 1]) && !consonants.Contains(word[word.Length - 2]);
        }

        private static List<char> vowels = new List<char>(new char[] { 'a', 'e', 'i', 'o', 'u' });

        public static bool ContainsSingleVowel(string word)
        {
            int count = 0;
            foreach (char c in word)
                if (vowels.Contains(c))
                    count++;
            return count == 1;
        }

        // http://www.resourceroom.net/readspell/doubling.asp
        // I've actually glossed over the single syllable requirement.
        public static string DoubleLetterRule(string word)
        {
            if (EndsWithSingleConsonant(word) && ContainsSingleVowel(word))
                word = word + word[word.Length - 1];
            return word;
        }

        public static StringDictionary IrregularNouns = DictionaryLoader.LoadDictionary(Resources.Irregular_Nouns);

        public static string PluralNoun(string noun)
		{
			StringBuilder result = new StringBuilder();
            
            if (IrregularNouns.ContainsKey(noun.ToLower()))
                result.Append(IrregularNouns[noun.ToLower()]);
            else
            {
                if (EndsWithAny(noun, new string[] {"s", "sh", "ch", "x", "z"}))
                    result.Append(noun.ToLower() + "es");
                else
                {
                    if (noun.EndsWith("y"))
                        result.Append(noun.TrimEnd('y') + "ies");
                    else
                        result.Append(noun.ToLower() + "s");
                }
            }

            return result.ToString();
		}

        public static string GerundNoun(string noun)
		{
            noun = DoubleLetterRule(noun);
            noun = noun.TrimEnd('e');
            return noun + "ing";
		}

        public static StringCollection Adjectives = CollectionLoader.LoadCollection(Resources.Adjectives);

        public static string AddSufixToAdverbsDerivedFromAdjectives(string adverb)
        {
            if (Adjectives.Contains(adverb))
                return adverb + "ly";
            else
                return adverb;
        }


        public static StringDictionary IrregularVerbs = DictionaryLoader.LoadDictionary(Resources.Irregular_Verbs);

        public static string FutureTenseVerb(string verb)
        {
            return "will " + verb;
        }

        public static string PastTenseVerb(string verb)
        {
			StringBuilder result = new StringBuilder();
            if (IrregularVerbs.ContainsKey(verb))
                result.Append(IrregularVerbs[verb]);
            else
            {
                if (verb.EndsWith("e"))
                    result.Append(verb + "d");
                else
                {
                    if (verb.EndsWith("y"))
                        result.Append(verb.TrimEnd('y') + "ied");
                    else
                        result.Append(DoubleLetterRule(verb) + "ed");
                }
            }
            return result.ToString();
        }

        public static string PluralVerb(string verb)
        {
            if (verb == "is")
                return "are";
            else
                return verb;
        }        
    }
}