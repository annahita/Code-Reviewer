using System.Collections.Generic;
using System.Linq;

namespace Analyzer
{
    public static class Filters
    {
        public static IEnumerable<T> Filter<T>(this IList<IAnalyzer> analyzers)
        {
            return analyzers.Select(a => new {success = a is T, val = a})
                .Where(a => a.success)
                .Select(v => (T) v.val);
        }
    }
}