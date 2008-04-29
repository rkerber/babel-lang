using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    public abstract class Goal
    {
        public double Priority;

		bool isCompleted;
        public bool IsCompleted
		{
			get { return isCompleted; }
			set
			{
				isCompleted = value;
				if (isCompleted)
					Completed = DateTime.Now;
			}
		}

		public DateTime Started;
        public DateTime Completed;
        public DateTime LastActed;

        public Log Related;

		public Goal()
		{
			Started = DateTime.Now;
		}

        public virtual string Act(Context context)
		{
			LastActed = DateTime.Now;
			return null;
		}
    }
}