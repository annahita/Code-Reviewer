using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Python.Antlr.Extension
{
    public static class ParserTreeExtension
    {
        public static IEnumerable<T> FindByContextLabel<T>(this IEnumerable<IParseTree> tree)
        {
            return tree.Select(a => new {success = a is T, val = a})
                .Where(a => a.success)
                .Select(v => (T) v.val);
        }

        public static int LineNumber(this IParseTree tree)
        {
            return ((ParserRuleContext) tree).Start.Line;
        }

        public static int StopLineNumber(this IParseTree tree)
        {
            return ((ParserRuleContext) tree).Stop.Line;
        }
    }
}