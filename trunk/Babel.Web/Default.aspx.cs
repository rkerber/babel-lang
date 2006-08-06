using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
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
        List<Statement> parse = parser.ParseSource(SourceTextBox.Text);

        ResultLabel.Text = String.Empty;

        foreach(Statement s in parse)
        {
            ResultLabel.Text += EnglishEmitter.ToEnglish(s) + "<br>\n";
        }

        foreach(Exception exception in parser.ErrorList)
        {
            ResultLabel.Text += exception.ToString() + "<br>\n";
        }
    }
}
