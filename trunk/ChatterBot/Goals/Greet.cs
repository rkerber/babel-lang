using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    public class Greet: Goal
    {
        public override string Act()
        {
            Completed = true;
            return "I.greet(You);";
        }
    }
}
