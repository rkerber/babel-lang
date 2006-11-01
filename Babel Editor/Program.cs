using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Babel;
using Babel.EnglishEmitter;

namespace Babel.Editor
{
	class Program
	{
        [STAThread]
		static void Main(string[] args)
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
		}
	}
}
