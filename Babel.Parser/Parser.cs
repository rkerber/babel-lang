using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using com.calitha.goldparser;

namespace Babel
{
    public class Parser
    {
        public List<Exception> ErrorList = new List<Exception>();

		private List<Statement> results;

        private LALRParser parser;

        public Parser()
        {
            long t1 = DateTime.Now.Ticks;

            using (Stream cgtStream = new MemoryStream(ParserResources.BabelCGT))
            {
                CGTReader reader = new CGTReader(cgtStream);
                parser = reader.CreateNewParser();
            }
            parser.TrimReductions = false;
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
            parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);

            long t2 = DateTime.Now.Ticks;
            Console.WriteLine(String.Format("Reading the Compiled Grammar Table File took {0}ms", (t2 - t1) / 10000));
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            ErrorList.Add(new Exception(String.Format("{0}: Token error. Cannot recognize token {1}.", args.Token.Location, args.Token.Text)));
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            ErrorList.Add(new Exception(String.Format("{0}: Syntax error. Expecting the following tokens {1}.  Found {2}.", args.UnexpectedToken.Location, args.ExpectedTokens, args.UnexpectedToken.Text)));
        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            args.Token.UserObject = CreateObject(args.Token);
        }

        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)ParserSymbols.Declaration :
                return null;
            }
            return null;
        }

        private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
        {
			args.Token.UserObject = CreateObject(args.Token);
			if (args.Token.UserObject is Statement)
				results.Insert(0, (Statement)args.Token.UserObject);
        }

        public Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)ParserRules.Sentanceset_Semi:
                case (int)ParserRules.Sentanceset_Semi2:
                    {
                        Verb verb = (Verb)token.Tokens[0].UserObject;
                        if (verb.Subject.Text == "You" && Question.IsInterrogative(verb.Text))
                            return new Question(verb);
                        else
                            return new Sentence(verb);
                    }

				case (int)ParserRules.Sentance:
                case (int)ParserRules.Sentance2:
                case (int)ParserRules.Sentance3:
                    return token.Tokens[0].UserObject;

                case (int)ParserRules.Declaration_Dot:
				case (int)ParserRules.Classdeclaration_Dot:
                {
                    Verb verb = new Verb("is");
                    verb.Subject = (Noun)token.Tokens[0].UserObject;
                    verb.Object = (Word)token.Tokens[2].UserObject;
					return verb;
                }

				case (int)ParserRules.Noun_Capitalizedword:
					return new Noun(token.Tokens[0].ToString());
                case (int)ParserRules.Noun_Capitalizedword2:
                    {
                        Noun noun = new Noun(token.Tokens[0].ToString());
                        List<string> sufixSet = (List<string>)token.Tokens[1].UserObject;
                        foreach (string sufix in sufixSet)
                            noun.SetSuffix(sufix);
                        return noun;
                    }

				case (int)ParserRules.Adjective_Word:
					return new Adjective(token.Tokens[0].ToString());

                case (int)ParserRules.Verb_Word:
                    {
                        Verb verb = new Verb(token.Tokens[0].ToString());
                        return verb;
                    }
                case (int)ParserRules.Verb_Word2:
                    {
                        Verb verb = new Verb(token.Tokens[0].ToString());
                        verb.SetSuffix((string)token.Tokens[1].UserObject);
                        return verb;
                    }
                case (int)ParserRules.Verb_Word3:
                    {
                        Verb verb = new Verb(token.Tokens[0].ToString());
                        verb.Adverbs = (List<Adverb>)token.Tokens[1].UserObject;
                        return verb;
                    }
                case (int)ParserRules.Verb_Word4:
                    {
                        Verb verb = new Verb(token.Tokens[0].ToString());
                        verb.SetSuffix((string)token.Tokens[1].UserObject);
                        verb.Adverbs = (List<Adverb>)token.Tokens[2].UserObject;
                        return verb;
                    }

				case (int)ParserRules.Adverb_Word:
					return new Adverb(token.Tokens[0].ToString());

				case (int)ParserRules.Adverbset:
                case (int)ParserRules.Adverbset3:
					List<Adverb> adverbSet = new List<Adverb>();
					adverbSet.Add((Adverb)token.Tokens[0].UserObject);
					return adverbSet;

                case (int)ParserRules.Adverbset2:
                case (int)ParserRules.Adverbset4:
					((List<Adverb>)token.Tokens[1].UserObject).Insert(0, (Adverb)token.Tokens[0].UserObject);
					return token.Tokens[1].UserObject;

				case (int)ParserRules.Adjectiveset:
				case (int)ParserRules.Adjectiveset3:
					List<Adjective> set = new List<Adjective>();
					set.Add((Adjective)token.Tokens[0].UserObject);
					return set;

                case (int)ParserRules.Adjectiveset2:
				case (int)ParserRules.Adjectiveset4:
					((List<Adjective>)token.Tokens[1].UserObject).Insert(0, (Adjective)token.Tokens[0].UserObject);
					return token.Tokens[1].UserObject;

				case (int)ParserRules.Phrase:
                case (int)ParserRules.Phrase2:
                case (int)ParserRules.Phrase3:
				case (int)ParserRules.Nounphrase:
					return token.Tokens[0].UserObject;


				case (int)ParserRules.Nounphrase2:
					((Noun)token.Tokens[0].UserObject).Adjectives = (List<Adjective>)token.Tokens[1].UserObject;
					return token.Tokens[0].UserObject;

				case (int)ParserRules.Nounset:
				case (int)ParserRules.Nounset2:
				case (int)ParserRules.Nounset3:
					return token.Tokens[0].UserObject;

                case (int)ParserRules.Nounandset_Amp:
                case (int)ParserRules.Nounandset_Amp2:
                case (int)ParserRules.Nounorset_Pipe:
                case (int)ParserRules.Nounorset_Pipe2:
                {
                    Noun noun = (Noun)token.Tokens[0].UserObject;
                    Noun secondNoun = (Noun)token.Tokens[2].UserObject;
                    if (noun.List == null)
                    {
                        if (secondNoun.List == null)
                            noun.List = new List<Noun>();
                        else
                        {
                            noun.List = secondNoun.List;
                            secondNoun.List = null;
                        }

                        noun.ListMode = ((TerminalToken)token.Tokens[1]).Text == "&" ? ListMode.And : ListMode.Or;
                    }
                    noun.List.Insert(0, secondNoun);
					return noun;
                }

				case (int)ParserRules.Verbobject:
                    return token.Tokens[0].UserObject;
                case (int)ParserRules.Verbobject2:
                    return null;

                case (int)ParserRules.Verbphrase_Dot_Lparan_Rparan:
                    {
                        Verb verb = (Verb)token.Tokens[2].UserObject;
                        verb.Subject = (Noun)token.Tokens[0].UserObject;
                        verb.Object = (Word)token.Tokens[4].UserObject;
                        return verb;
                    }
                case (int)ParserRules.Verbphrase_Lparan_Rparan:
                    {
                        Verb verb = (Verb)token.Tokens[0].UserObject;
                        verb.Subject = new Noun("You");
                        verb.Object = (Word)token.Tokens[2].UserObject;
                        return verb;
                    }

                case (int)ParserRules.Nounsuffix_Eq:
                case (int)ParserRules.Nounsuffix_Times:
                case (int)ParserRules.Verbsuffix_Minus:
                case (int)ParserRules.Verbsuffix_Plus:
                    return token.Tokens[0].ToString();

                case (int)ParserRules.Nounsuffixset:
                {
                    List<string> result = new List<string>();
                    result.Add((string)token.Tokens[0].UserObject);
                    return result;
                }
                case (int)ParserRules.Nounsuffixset2:
                {
                    List<string> result = (List<string>)token.Tokens[1].UserObject;
                    result.Add((string)token.Tokens[0].UserObject);
                    return result;
                }

                case (int)ParserRules.Preposition_Lbracket_Rbracket:
                    ((Adverb)token.Tokens[0].UserObject).PrepositionSubject = (Word)token.Tokens[2].UserObject;
                    return token.Tokens[0].UserObject;

                case (int)ParserRules.Restrictiveclause_Lbracket_Rbracket:
                    ((Adjective)token.Tokens[0].UserObject).RestictiveClauseSubject = (Word)token.Tokens[2].UserObject;
                    return token.Tokens[0].UserObject;

                default:
                    throw new NotImplementedException(String.Format("Unimplemented Token Rule ({0}) for token {1}", token.Rule, token.ToString()));
			}
        }


        public List<Statement> ParseSource(string source)
        {
			long t1 = DateTime.Now.Ticks;

            ErrorList.Clear();
			results = new List<Statement>();

            NonterminalToken parseTree = parser.Parse(source);

            long t2 = DateTime.Now.Ticks;
            Console.WriteLine(String.Format("Parsing the source took {0}ms", (t2 - t1) / 10000));

            return results;
        }
    }
}