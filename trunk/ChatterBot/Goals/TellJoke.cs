using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    public class TellJoke: Goal
    {
        public override string Act()
        {
            Completed = true;
            return "I.know not();";
        }
    }
}