using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot
{
    public abstract class Option
    {
        public string Name;
        public string Description;

        public abstract string GetResponse();
    }
}
