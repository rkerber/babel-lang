using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    class IntroduceSelf: Goal
    {
		public static double Evaluate(Context context)
		{
			if (context.Log.Count == 0)
				return 1;
			
			return 0;
		}

		public override string Act(Context context)
        {
            IsCompleted = true;
            return "Name of[I].Computer;";
        }
    }
}
