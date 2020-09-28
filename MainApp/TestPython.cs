using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MainApp
{
    public partial class TestPython : Form
    {
        private const string Ok = "No Issue found";
        public TestPython()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            ResultGridView.DataSource = null;
            if (openPythonFile.ShowDialog() != DialogResult.OK) return;
            try
            {
                var codeFile = File.ReadAllText(openPythonFile.FileName);
                var result = CleanCodeAnalyzer.Analyze(codeFile);
                if (result.Any())
                    ResultGridView.DataSource = result;
                else
                    MessageBox.Show(Ok);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}