using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Babel.EnglishEmitter;

namespace Babel.Test
{
    public partial class TestForm : Form
    {
        private static Parser parser = new Parser();

        public TestForm()
        {
            InitializeComponent();
            try
            {
                load(@"TestCases.babel");
            }
            catch
            {
                // Throw away if can't load
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                saveFileDialog.FileName = openFileDialog.FileName;
                Text = "Babeler - " + saveFileDialog.FileName;
            }
        }

        private void load(string filename)
        {
            sourceTextBox.Text = File.ReadAllText(filename);
            parse();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.FileName.Length == 0)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, sourceTextBox.Text);
                    Text = "Babeler - " + saveFileDialog.FileName;
                }
            }
            else
            {
                File.WriteAllText(saveFileDialog.FileName, sourceTextBox.Text);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, sourceTextBox.Text);
                Text = "Babeler - " + saveFileDialog.FileName;
            }
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parse();
        }

        private void resultTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = resultTreeView.SelectedNode.Tag;
        }

        private void sourceTextBox_SelectionChanged(object sender, EventArgs e)
        {
            int line = sourceTextBox.GetLineFromCharIndex(sourceTextBox.SelectionStart);
            lineLabel.Text = "Line: " + (line + 1);
            columnLabel.Text = "Col: " + ((sourceTextBox.SelectionStart - sourceTextBox.GetFirstCharIndexFromLine(line)) + 1);
        }

        private void parse()
        {
            resultTextBox.Clear();
            resultTreeView.Nodes.Clear();

            List<Statement> parse = parser.ParseSource(sourceTextBox.Text);

            foreach(Statement s in parse)
            {
                TreeNode result = new TreeNode(s.ToString());
                result.Tag = s;
                result.Nodes.Add(WordToTreeNode(s.Verb));
                resultTreeView.Nodes.Add(result);

                resultTextBox.AppendText(EnglishEmitter.EnglishEmitter.ToEnglish(s) + "\n");
            }

            int temp = resultTextBox.TextLength;
            foreach(Exception exception in parser.ErrorList)
            {
                resultTextBox.AppendText(exception.ToString() + "\n");
            }

            // highlight the errors in red
            resultTextBox.SelectionStart = temp;
            resultTextBox.SelectionLength = resultTextBox.TextLength - temp;
            resultTextBox.SelectionColor = Color.Red;
            resultTextBox.SelectionLength = 0;
            
            //resultTextBox.ScrollToCaret();

            resultTreeView.ExpandAll();
        }

        private TreeNode WordToTreeNode(Word word)
        {
            TreeNode result = new TreeNode(word.ToString());
            result.Tag = word;

            Verb verb = word as Verb;
            if (verb != null)
            {
                if (verb.Subject != null)
                {
                    TreeNode node = WordToTreeNode(verb.Subject);
                    node.ForeColor = Color.Red;
                    result.Nodes.Add(node);
                }
                if (verb.Object != null)
                    result.Nodes.Add(WordToTreeNode(verb.Object));
                if (verb.Adverbs != null)
                {
                    foreach (Adverb verbAdverb in verb.Adverbs)
                        result.Nodes.Add(WordToTreeNode(verbAdverb));
                }
            }

            Noun noun = word as Noun;
            if (noun != null)
            {
                if (noun.List != null)
                    foreach (Noun listNoun in noun.List)
                        result.Nodes.Add(WordToTreeNode(listNoun));

                if (noun.Adjectives != null)
                {
                    foreach (Adjective adjective in noun.Adjectives)
                        result.Nodes.Add(WordToTreeNode(adjective));
                }
            }

            Adverb adverb = word as Adverb;
            if (adverb != null && adverb.PrepositionSubject != null)
                result.Nodes.Add(WordToTreeNode(adverb.PrepositionSubject));

            return result;
        }
    }
}