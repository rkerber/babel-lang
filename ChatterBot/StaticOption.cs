using System;
using System.Collections.Generic;
using System.Text;

namespace ChatterBot
{
    public class StaticOption : Option
    {
        public StaticOption(string response)
        {
            Response = response;
        }

        public string Response;

        public override string GetResponse()
        {
            return Response;
        }
    }
}