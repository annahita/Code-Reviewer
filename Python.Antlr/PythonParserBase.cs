using System.IO;
using Antlr4.Runtime;

namespace Python.Antlr
{
    public enum PythonVersion
    {
        Autodetect,
        Python2 = 2,
        Python3 = 3
    }

    public abstract class PythonParserBase : Parser
    {
        protected PythonParserBase(ITokenStream input, TextWriter output, TextWriter errorOutput) : base(input)
        {
        }

        protected PythonParserBase(ITokenStream input) : base(input)
        {
        }

        public PythonVersion Version { get; set; }

        protected bool CheckVersion(int version)
        {
            return Version == PythonVersion.Autodetect || version == (int) Version;
        }

        protected void SetVersion(int requiredVersion)
        {
            Version = (PythonVersion) requiredVersion;
        }
    }
}