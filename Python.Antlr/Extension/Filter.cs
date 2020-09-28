using System.Collections.Generic;
using Antlr4.Runtime.Tree;

namespace Python.Antlr.Extension
{
    public static class Filter
    {
        public static IEnumerable<IParseTree> FilterByAssignStatement(IEnumerable<IParseTree> trees)
        {
            return trees.FindByContextLabel<PythonParser.Assign_stmtContext>();
        }

        public static IEnumerable<IParseTree> FilterBySingleStatement(IEnumerable<IParseTree> trees)
        {
            return trees.FindByContextLabel<PythonParser.Single_stmtContext>();
        }

        public static IEnumerable<IParseTree> FilterByReturnStatement(IEnumerable<IParseTree> trees)
        {
            return trees.FindByContextLabel<PythonParser.Return_stmtContext>();
        }
    }
}