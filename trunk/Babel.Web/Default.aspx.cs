using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Babel;
using Babel.EnglishEmitter;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TranslateButton_Click(object sender, EventArgs e)
    {
        parse();
    }

    private static Parser parser = new Parser();

    private void parse()
    {
        ParseResult parse = parser.ParseSource(SourceTextBox.Text);

        ResultLabel.Text = String.Empty;

        foreach(Statement s in parse)
            ResultLabel.Text += EnglishEmitter.ToEnglish(s) + "<br>\r\n";

        foreach(Exception exception in parse.ErrorList)
            ResultLabel.Text += exception.ToString() + "<br>\r\n";

        Log(String.Format("\r\n\r\n\t\t{0} - {1} - {2}\r\n\r\nSource:\r\n{3}\r\n\r\nResult:\r\n{4}", DateTime.Now.ToString(), Request.UserHostAddress, Request.UserAgent, SourceTextBox.Text, ResultLabel.Text.Replace("<br>", String.Empty)));
    }

    public void Log(string message)
    {
        if (message.Length < 10000)
        {
            DateTime now = DateTime.Now;
            File.AppendAllText(Request.PhysicalApplicationPath + "\\logs\\" + now.Year + "-" + now.Month + "-" + now.Day + ".log", message);
        }
    }
}