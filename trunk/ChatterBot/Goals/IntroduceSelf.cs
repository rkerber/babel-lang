using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    class IntroduceSelf: Goal
    {
		public override string Act(Context context)
        {
            IsCompleted = true;
            return "Name of[I].Computer";
        }
    }
}
