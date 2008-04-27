using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    public class TellJoke: Goal
    {
		bool questionSent;
		bool punchlineSent;

		public override string Act(Context context)
        {
			if (!questionSent)
			{
				questionSent = true;

				// What do you call a dog with no legs?
				return "what(You.name(Dog with[Leg* zero]));";
			}

			if (!punchlineSent)
			{
				punchlineSent = true;
				// It doesn't matter.  He's not coming anyways.
				return "Name of[Dog].matter not();  Dog.is not(Come=);";
			}

			// TODO: Detect a positive response.
			IsCompleted = true;			
			return null;
        }
    }
}