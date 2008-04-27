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
			//engine.Initialize();

			Babel.Parser parser = new Babel.Parser();

			Console.WriteLine("Babel Chatterbot Running");

			engine.Act();

			while (true)
			{
				Console.Write("> ");
				string input = Console.ReadLine().Trim();

				ParseResult parse = null;
				if (input.Length > 0)
				{
					if (!input.EndsWith(";"))
						input = input + ";";

					parse = parser.ParseSource(input);
					Console.Write(parse.ToString());

					if (parse.Successfull)
					{
						Console.WriteLine(">>> {0} ({1})", input, Babel.EnglishEmitter.EnglishEmitter.ToEnglish(parse).Trim());
						engine.Sense(input, "Chat Client");
					}
				}

				if (parse == null || parse.Successfull)
				{
					string response = engine.Act();
					if (response != null && response.Length > 0)
						Console.WriteLine("<<< {0} ({1})", response, EnglishEmitter.ToEnglish(response).Trim());
				}

				//if (response == "quit();")
				//{
				//    Console.ReadLine();
				//    return;
				//}
			}
		}
	}
}