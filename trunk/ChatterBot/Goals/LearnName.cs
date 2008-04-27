using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    class LearnName: Goal
    {
        bool nameAsked;

		public override string Act(Context context)
        {
            if (!nameAsked)
            {
                nameAsked = true;
                return "what name of[You]();";
            }

			IsCompleted = true;
            return null;
        }
    }
}
