using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    class LearnName: Goal
    {
        bool nameAsked;

        public override string Act()
        {
            if (!nameAsked)
            {
                nameAsked = true;
                return "what name of[You]();";
            }
            return null;
        }
    }
}
