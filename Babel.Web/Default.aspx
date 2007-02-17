<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Babel To English Translator</title>
</head>
<body style="font-family: Times New Roman">
    <form id="form1" runat="server">
    <div>
        <h2>
            Enter the Babel text to be translated to English (<a href="http://babel-lang.googlecode.com/svn/trunk/Babel%20Editor/bin/Debug/TestCases.babel">examples</a>)<br />
            <asp:TextBox ID="SourceTextBox" runat="server" Height="266px" Rows="100" Width="576px" TextMode="MultiLine">// The quick brown fox jumped over the lazy dog.
Fox quick brown.jump- over[Dog lazy]();</asp:TextBox>
            <br />
            <asp:Button ID="TranslateButton" runat="server" Text="Translate" OnClick="TranslateButton_Click" />
        </h2>
        <p>&nbsp;</p>
        <h2>Results:</h2>
        <p><asp:Label ID="ResultLabel" runat="server" Height="306px" Width="585px"></asp:Label>&nbsp;</p>
    </div>
    </form>
</body>
</html>
