using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot
{
	public class Context
	{
		public Log Log = new Log();

		public ConversationState State;

		public void AnalyzeState()
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

			//State = ConversationState.SmallTalking;
		}

		public bool? BooleanAnswer()
		{
			var last = Log.LastReceived;
			
			if (!last.IsSourceSelf)
			{
				if (last.Message == "I.agree();")
				{
					return true;
				}

				if (last.Message == "I.agree not();")
				{
					return false;
				}
			}

			return null;
		}
	}
}
