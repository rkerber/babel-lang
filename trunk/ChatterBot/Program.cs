using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChatterBot
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ChatEngine engine = new ChatEngine();
            engine.Initialize();

            while (true)
            {
                string input = Console.ReadLine();
                engine.Sense(input, "Chat Client");

                string response = engine.Act();
                if (response.Length > 0)
                    Console.WriteLine(response);

                if (response == "quit();")
                {
                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}