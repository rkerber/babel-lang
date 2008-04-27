using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    class AnswerQuestion: Goal
    {
		public override string Act(Context context)
        {
            IsCompleted = true;
            return "I.know not();";
        }

    
            //option = new StaticOption("I.agree();");
            //option.Name = "Yes";
            //OptionList.Add(option);

            //option = new StaticOption("I.agree not();");
            //option.Name = "No";
            //OptionList.Add(option);

            //option = new StaticOption("I.do not(Know);");
            //option.Name = "Do not Know";
            //OptionList.Add(option);

            //option = new StaticOption("I.agree maybe();");
            //option.Name = "Maybe";
            //OptionList.Add(option);
    }
}
