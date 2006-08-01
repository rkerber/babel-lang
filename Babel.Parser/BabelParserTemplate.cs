
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace Babel
{
    public enum ParserSymbols : int
    {
        /// <summary>
        /// EOF
        /// </summary>
        Eof = 0  , // (EOF)
        /// <summary>
        /// Error
        /// </summary>
        Error = 1  , // (Error)
        /// <summary>
        /// Whitespace
        /// </summary>
        Whitespace = 2  , // (Whitespace)
        /// <summary>
        /// Comment End
        /// </summary>
        Commentend = 3  , // (Comment End)
        /// <summary>
        /// Comment Line
        /// </summary>
        Commentline = 4  , // (Comment Line)
        /// <summary>
        /// Comment Start
        /// </summary>
        Commentstart = 5  , // (Comment Start)
        /// <summary>
        /// -
        /// </summary>
        Minus = 6  , // '-'
        /// <summary>
        /// &#38;
        /// </summary>
        Amp = 7  , // '&'
        /// <summary>
        /// (
        /// </summary>
        Lparan = 8  , // '('
        /// <summary>
        /// )
        /// </summary>
        Rparan = 9  , // ')'
        /// <summary>
        /// *
        /// </summary>
        Times = 10 , // '*'
        /// <summary>
        /// .
        /// </summary>
        Dot = 11 , // '.'
        /// <summary>
        /// ;
        /// </summary>
        Semi = 12 , // ';'
        /// <summary>
        /// [
        /// </summary>
        Lbracket = 13 , // '['
        /// <summary>
        /// ]
        /// </summary>
        Rbracket = 14 , // ']'
        /// <summary>
        /// |
        /// </summary>
        Pipe = 15 , // '|'
        /// <summary>
        /// +
        /// </summary>
        Plus = 16 , // '+'
        /// <summary>
        /// =
        /// </summary>
        Eq = 17 , // '='
        /// <summary>
        /// CapitalizedWord
        /// </summary>
        Capitalizedword = 18 , // CapitalizedWord
        /// <summary>
        /// Word
        /// </summary>
        Word = 19 , // Word
        /// <summary>
        /// Adjective
        /// </summary>
        Adjective = 20 , // <Adjective>
        /// <summary>
        /// Adjective Set
        /// </summary>
        Adjectiveset = 21 , // <Adjective Set>
        /// <summary>
        /// Adverb
        /// </summary>
        Adverb = 22 , // <Adverb>
        /// <summary>
        /// Adverb Set
        /// </summary>
        Adverbset = 23 , // <Adverb Set>
        /// <summary>
        /// Class Declaration
        /// </summary>
        Classdeclaration = 24 , // <Class Declaration>
        /// <summary>
        /// Declaration
        /// </summary>
        Declaration = 25 , // <Declaration>
        /// <summary>
        /// Noun
        /// </summary>
        Noun = 26 , // <Noun>
        /// <summary>
        /// Noun And Set
        /// </summary>
        Nounandset = 27 , // <Noun And Set>
        /// <summary>
        /// Noun Or Set
        /// </summary>
        Nounorset = 28 , // <Noun Or Set>
        /// <summary>
        /// Noun Phrase
        /// </summary>
        Nounphrase = 29 , // <Noun Phrase>
        /// <summary>
        /// Noun Set
        /// </summary>
        Nounset = 30 , // <Noun Set>
        /// <summary>
        /// Noun Suffix
        /// </summary>
        Nounsuffix = 31 , // <Noun Suffix>
        /// <summary>
        /// Noun Suffix Set
        /// </summary>
        Nounsuffixset = 32 , // <Noun Suffix Set>
        /// <summary>
        /// Phrase
        /// </summary>
        Phrase = 33 , // <Phrase>
        /// <summary>
        /// Preposition
        /// </summary>
        Preposition = 34 , // <Preposition>
        /// <summary>
        /// Restrictive Clause
        /// </summary>
        Restrictiveclause = 35 , // <Restrictive Clause>
        /// <summary>
        /// Sentance
        /// </summary>
        Sentance = 36 , // <Sentance>
        /// <summary>
        /// Sentance Set
        /// </summary>
        Sentanceset = 37 , // <Sentance Set>
        /// <summary>
        /// Verb
        /// </summary>
        Verb = 38 , // <Verb>
        /// <summary>
        /// Verb Phrase
        /// </summary>
        Verbphrase = 39 , // <Verb Phrase>
        /// <summary>
        /// Verb Suffix
        /// </summary>
        Verbsuffix = 40 , // <Verb Suffix>
        /// <summary>
        /// VerbObject
        /// </summary>
        Verbobject = 41   // <VerbObject>
    };

    public enum ParserRules : int
    {
        /// <summary>
        /// <Sentance Set> ::= <Sentance> &#39;;&#39; <Sentance Set>
        /// </summary>
        Sentanceset_Semi = 0  , // <Sentance Set> ::= <Sentance> ';' <Sentance Set>
        /// <summary>
        /// <Sentance Set> ::= <Sentance> &#39;;&#39;
        /// </summary>
        Sentanceset_Semi2 = 1  , // <Sentance Set> ::= <Sentance> ';'
        /// <summary>
        /// <Sentance> ::= <Verb Phrase>
        /// </summary>
        Sentance = 2  , // <Sentance> ::= <Verb Phrase>
        /// <summary>
        /// <Sentance> ::= <Declaration>
        /// </summary>
        Sentance2 = 3  , // <Sentance> ::= <Declaration>
        /// <summary>
        /// <Sentance> ::= <Class Declaration>
        /// </summary>
        Sentance3 = 4  , // <Sentance> ::= <Class Declaration>
        /// <summary>
        /// <Declaration> ::= <Noun Set> &#39;.&#39; <Adjective>
        /// </summary>
        Declaration_Dot = 5  , // <Declaration> ::= <Noun Set> '.' <Adjective>
        /// <summary>
        /// <Class Declaration> ::= <Noun Set> &#39;.&#39; <Noun Set>
        /// </summary>
        Classdeclaration_Dot = 6  , // <Class Declaration> ::= <Noun Set> '.' <Noun Set>
        /// <summary>
        /// <Phrase> ::= <Sentance>
        /// </summary>
        Phrase = 7  , // <Phrase> ::= <Sentance>
        /// <summary>
        /// <Phrase> ::= <Noun Set>
        /// </summary>
        Phrase2 = 8  , // <Phrase> ::= <Noun Set>
        /// <summary>
        /// <Phrase> ::= <Adjective>
        /// </summary>
        Phrase3 = 9  , // <Phrase> ::= <Adjective>
        /// <summary>
        /// <Noun Set> ::= <Noun Phrase>
        /// </summary>
        Nounset = 10 , // <Noun Set> ::= <Noun Phrase>
        /// <summary>
        /// <Noun Set> ::= <Noun And Set>
        /// </summary>
        Nounset2 = 11 , // <Noun Set> ::= <Noun And Set>
        /// <summary>
        /// <Noun Set> ::= <Noun Or Set>
        /// </summary>
        Nounset3 = 12 , // <Noun Set> ::= <Noun Or Set>
        /// <summary>
        /// <Noun And Set> ::= <Noun Phrase> &#39;&#38;&#39; <Noun Phrase>
        /// </summary>
        Nounandset_Amp = 13 , // <Noun And Set> ::= <Noun Phrase> '&' <Noun Phrase>
        /// <summary>
        /// <Noun And Set> ::= <Noun Phrase> &#39;&#38;&#39; <Noun And Set>
        /// </summary>
        Nounandset_Amp2 = 14 , // <Noun And Set> ::= <Noun Phrase> '&' <Noun And Set>
        /// <summary>
        /// <Noun Or Set> ::= <Noun Phrase> &#39;|&#39; <Noun Phrase>
        /// </summary>
        Nounorset_Pipe = 15 , // <Noun Or Set> ::= <Noun Phrase> '|' <Noun Phrase>
        /// <summary>
        /// <Noun Or Set> ::= <Noun Phrase> &#39;|&#39; <Noun Or Set>
        /// </summary>
        Nounorset_Pipe2 = 16 , // <Noun Or Set> ::= <Noun Phrase> '|' <Noun Or Set>
        /// <summary>
        /// <Noun> ::= CapitalizedWord
        /// </summary>
        Noun_Capitalizedword = 17 , // <Noun> ::= CapitalizedWord
        /// <summary>
        /// <Noun> ::= CapitalizedWord <Noun Suffix Set>
        /// </summary>
        Noun_Capitalizedword2 = 18 , // <Noun> ::= CapitalizedWord <Noun Suffix Set>
        /// <summary>
        /// <Noun Suffix> ::= &#39;*&#39;
        /// </summary>
        Nounsuffix_Times = 19 , // <Noun Suffix> ::= '*'
        /// <summary>
        /// <Noun Suffix> ::= &#39;=&#39;
        /// </summary>
        Nounsuffix_Eq = 20 , // <Noun Suffix> ::= '='
        /// <summary>
        /// <Noun Suffix Set> ::= <Noun Suffix>
        /// </summary>
        Nounsuffixset = 21 , // <Noun Suffix Set> ::= <Noun Suffix>
        /// <summary>
        /// <Noun Suffix Set> ::= <Noun Suffix> <Noun Suffix Set>
        /// </summary>
        Nounsuffixset2 = 22 , // <Noun Suffix Set> ::= <Noun Suffix> <Noun Suffix Set>
        /// <summary>
        /// <Noun Phrase> ::= <Noun>
        /// </summary>
        Nounphrase = 23 , // <Noun Phrase> ::= <Noun>
        /// <summary>
        /// <Noun Phrase> ::= <Noun> <Adjective Set>
        /// </summary>
        Nounphrase2 = 24 , // <Noun Phrase> ::= <Noun> <Adjective Set>
        /// <summary>
        /// <Adjective Set> ::= <Adjective>
        /// </summary>
        Adjectiveset = 25 , // <Adjective Set> ::= <Adjective>
        /// <summary>
        /// <Adjective Set> ::= <Adjective> <Adjective Set>
        /// </summary>
        Adjectiveset2 = 26 , // <Adjective Set> ::= <Adjective> <Adjective Set>
        /// <summary>
        /// <Adjective Set> ::= <Restrictive Clause>
        /// </summary>
        Adjectiveset3 = 27 , // <Adjective Set> ::= <Restrictive Clause>
        /// <summary>
        /// <Adjective Set> ::= <Restrictive Clause> <Adjective Set>
        /// </summary>
        Adjectiveset4 = 28 , // <Adjective Set> ::= <Restrictive Clause> <Adjective Set>
        /// <summary>
        /// <Adjective> ::= Word
        /// </summary>
        Adjective_Word = 29 , // <Adjective> ::= Word
        /// <summary>
        /// <Restrictive Clause> ::= <Adjective> &#39;[&#39; <Phrase> &#39;]&#39;
        /// </summary>
        Restrictiveclause_Lbracket_Rbracket = 30 , // <Restrictive Clause> ::= <Adjective> '[' <Phrase> ']'
        /// <summary>
        /// <Verb Phrase> ::= <Noun Set> &#39;.&#39; <Verb> &#39;(&#39; <VerbObject> &#39;)&#39;
        /// </summary>
        Verbphrase_Dot_Lparan_Rparan = 31 , // <Verb Phrase> ::= <Noun Set> '.' <Verb> '(' <VerbObject> ')'
        /// <summary>
        /// <Verb Phrase> ::= <Verb> &#39;(&#39; <VerbObject> &#39;)&#39;
        /// </summary>
        Verbphrase_Lparan_Rparan = 32 , // <Verb Phrase> ::= <Verb> '(' <VerbObject> ')'
        /// <summary>
        /// <Verb> ::= Word
        /// </summary>
        Verb_Word = 33 , // <Verb> ::= Word
        /// <summary>
        /// <Verb> ::= Word <Verb Suffix>
        /// </summary>
        Verb_Word2 = 34 , // <Verb> ::= Word <Verb Suffix>
        /// <summary>
        /// <Verb> ::= Word <Adverb Set>
        /// </summary>
        Verb_Word3 = 35 , // <Verb> ::= Word <Adverb Set>
        /// <summary>
        /// <Verb> ::= Word <Verb Suffix> <Adverb Set>
        /// </summary>
        Verb_Word4 = 36 , // <Verb> ::= Word <Verb Suffix> <Adverb Set>
        /// <summary>
        /// <Verb Suffix> ::= &#39;-&#39;
        /// </summary>
        Verbsuffix_Minus = 37 , // <Verb Suffix> ::= '-'
        /// <summary>
        /// <Verb Suffix> ::= &#39;+&#39;
        /// </summary>
        Verbsuffix_Plus = 38 , // <Verb Suffix> ::= '+'
        /// <summary>
        /// <Adverb Set> ::= <Adverb>
        /// </summary>
        Adverbset = 39 , // <Adverb Set> ::= <Adverb>
        /// <summary>
        /// <Adverb Set> ::= <Adverb> <Adverb Set>
        /// </summary>
        Adverbset2 = 40 , // <Adverb Set> ::= <Adverb> <Adverb Set>
        /// <summary>
        /// <Adverb Set> ::= <Preposition>
        /// </summary>
        Adverbset3 = 41 , // <Adverb Set> ::= <Preposition>
        /// <summary>
        /// <Adverb Set> ::= <Preposition> <Adverb Set>
        /// </summary>
        Adverbset4 = 42 , // <Adverb Set> ::= <Preposition> <Adverb Set>
        /// <summary>
        /// <Adverb> ::= Word
        /// </summary>
        Adverb_Word = 43 , // <Adverb> ::= Word
        /// <summary>
        /// <Preposition> ::= <Adverb> &#39;[&#39; <Phrase> &#39;]&#39;
        /// </summary>
        Preposition_Lbracket_Rbracket = 44 , // <Preposition> ::= <Adverb> '[' <Phrase> ']'
        /// <summary>
        /// <VerbObject> ::= <Phrase>
        /// </summary>
        Verbobject = 45 , // <VerbObject> ::= <Phrase>
        /// <summary>
        /// <VerbObject> ::= 
        /// </summary>
        Verbobject2 = 46   // <VerbObject> ::= 
    };
}
