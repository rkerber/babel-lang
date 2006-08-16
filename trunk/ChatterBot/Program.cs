using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Babel;
using Babel.EnglishEmitter;

namespace ChatterBot
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ChatEngine engine = new ChatEngine();
            engine.Initialize();

            Babel.Parser parser = new Babel.Parser();

            while (true)
            {
                string input = Console.ReadLine().Trim();

                if (!input.EndsWith(";"))
                    input = input + ";";

                ParseResult parse = parser.ParseSource(input);
                Console.Write(parse.ToString());

                if (parse.Successfull)
                {
                    Console.Write("({0}) sent to bot.\n", Babel.EnglishEmitter.EnglishEmitter.ToEnglish(parse).Trim());
                    engine.Sense(input, "Chat Client");

                    string response = engine.Act();
                    if (response.Length > 0)
                    {
                        Console.WriteLine("{0} ({1})", response, EnglishEmitter.ToEnglish(response).Trim());
                    }

                    if (response == "quit();")
                    {
                        Console.ReadLine();
                        return;
                    }
                }
            }
        }
    }
}