using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    class UnderstandWords: Goal
    {
        public override string Act()
        {
//            Completed = true;
            return "I.do not(Understand);"; // I do not understand
        }
    }
}
