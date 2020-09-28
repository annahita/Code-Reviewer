using System.Collections.Generic;
using System.Linq;
using OOP_Model;

namespace Python.Model
{
    public class EmbeddedStatement : EntityBase, IEmbeddedStatement
    {
        private readonly int _countOfDirectChildren;

        public EmbeddedStatement(int startLineNumber, int countsOfDirectChildren) : base(
            startLineNumber)
        {
            _countOfDirectChildren = countsOfDirectChildren;
        }

        public IEnumerable<IEmbeddedStatement> InternalEmbeddedStatement { get; private set; } =
            new List<EmbeddedStatement>();

        public int GetLinesOfCodeCount()
        {
            return _countOfDirectChildren + InternalEmbeddedStatement.Sum(a => a.GetLinesOfCodeCount());
        }

        public int CountNestedStructures()
        {
            return SumOfNestedStructures();
        }

        public void SetEmbedded_structure(IEnumerable<IEmbeddedStatement> embeddedStatements)
        {
            InternalEmbeddedStatement = embeddedStatements;
        }

        private int SumOfNestedStructures()
        {
            var sum = 1;
            sum += InternalEmbeddedStatement.Sum(i => ((EmbeddedStatement) i).SumOfNestedStructures());
            return sum;
        }
    }
}