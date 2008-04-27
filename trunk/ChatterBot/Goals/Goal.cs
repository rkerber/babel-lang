using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot.Goals
{
    public abstract class Goal
    {
        public double Priority;

        public bool IsCompleted;
        public DateTime Started;
        public DateTime Finished;
        public DateTime LastActed;

        public Log Related;

        public abstract string Act(Context context);
    }
}