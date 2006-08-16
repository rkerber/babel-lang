using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot
{
    class ChatEngine
    {
        public Log Log = new Log();
        public OptionList OptionList = new OptionList();
        public DateTime LastActed;

        public ConversationState State;

        public string Act()
        {
            LastActed = DateTime.Now;
            //return String.Empty;

            // if nothing has changed in log then do nothing

            // if question then try to answer

            // if statement was made then acknowlege or ask a question

            // if given order then acknowledge and try to execute (add to goals?)

            // if greet and not already greeted then greet

            // if name unknown ask name

            // ask about something interesting

            // tell a story

            // do nothing

            string result = "I.agree();";
            Log.Add(new LogEntry("Self", result));
            return result;
        }

        public void Sense(string message, string source)
        {
            Log.Add(new LogEntry(source, message));
        }

        public System.Collections.Generic.List<ConversationState> EvaluateStates(string source)
        {
            /* 
             * expire/age old state settings (ie, offended, insulted, entertained)
             * 
             * keep track of last time state was updated
             * get message received since last update
             * 
             * search messages for indicators
             * 
             * attach/update state settings with new information
             * 
             * attach messages as reasons for indicators (for diagnostics and future state decisions evaluation)
             */

            return null;
        }

        public void Initialize()
        {
            Option option = new StaticOption(String.Empty);
            option.Name = "Do nothing.";
            OptionList.Add(option);

            option = new StaticOption("what.Name of[You]();");
            option.Name = "Ask for name";
            OptionList.Add(option);

            option = new StaticOption("I.do not(Understand);");
            option.Name = "Do not understand";
            OptionList.Add(option);

            option = new StaticOption("I.greet(You);");
            option.Name = "Greet";
            OptionList.Add(option);

            option = new StaticOption("I.agree();");
            option.Name = "Yes";
            OptionList.Add(option);

            option = new StaticOption("I.agree not();");
            option.Name = "No";
            OptionList.Add(option);

            option = new StaticOption("I.do not(Know);");
            option.Name = "Do not Know";
            OptionList.Add(option);

            option = new StaticOption("I.agree maybe();");
            option.Name = "Maybe";
            OptionList.Add(option);
        }
    }
}