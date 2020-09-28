using System.Collections.Generic;
using System.Text;
using Python.Antlr.Extension;

namespace Python.Antlr.Path
{
    internal class ParserPath
    {
        protected ParserPath(IEnumerable<object> elements)
        {
            Path = ConvertToString(elements);
        }

        public string Path { get; }

        private string ConvertToString(IEnumerable<object> names)
        {
            var path = new StringBuilder();
            foreach (var item in names)
                if (ResolveElementType(item) == PathElements.ParserElement)
                    path.Append(PythonParser.ruleNames[(int) item]);
                else
                    path.Append(item);
            return path.ToString();
        }

        private PathElements ResolveElementType(object element)
        {
            return element is int ? PathElements.ParserElement : PathElements.Direction;
        }
    }
}