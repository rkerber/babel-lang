using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    public class Greet: Goal
    {
        public override string Act(Context context)
        {
            IsCompleted = true;
            return "I.greet(You);";
        }
    }
}
