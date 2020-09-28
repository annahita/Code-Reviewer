using System.Collections.Generic;
using System.Linq;
using OOP_Model;

namespace Python.Model.Extension
{
    public static class ExpressionsExtension
    {
        public static IEnumerable<T> FilterExpression<T>(this IEnumerable<IExpression> expressions)
        {
            return expressions.Select(a => new {success = a is T, val = a})
                    .Where(a => a.success).Select(v => (T) v.val.GetThis())
                ;
        }
    }
}