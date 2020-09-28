using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Model.Extension;

namespace Python.Model
{
    public class MethodBody : EntityBase, IMethodBody
    {
        private readonly int _countsOfDirectChildren;
        private readonly int _endLineNumber;
        private readonly int _startLineNumber;

        public MethodBody(int startLineNumber, int endLineNumber, int countsOfDirectChildren) : base(startLineNumber)
        {
            _countsOfDirectChildren = countsOfDirectChildren;
            _startLineNumber = startLineNumber;
            _endLineNumber = endLineNumber;
        }

        public IMethodBase Parent { get; private set; }


        public IEnumerable<ISimpleStatement> SimpleStatements { get; private set; } = new List<ISimpleStatement>();

        public IEnumerable<IEmbeddedStatement> EmbeddedStatements { get; private set; } =
            new List<IEmbeddedStatement>();

        public IEnumerable<ILocalVariableDeclaration> GetDeclaredLocalVariables()
        {
            return SimpleStatements.SelectMany(a => a.Expressions).FilterExpression<LocalVariableDeclaration>();
        }

        public IEnumerable<IVariableAccess> GetAccessedFields()
        {
            var arguments = GetInvokedMethods().SelectMany(a => ((MethodInvocation) a).GetArgumentsWithVariableType());
            return GetStatementsAccessedVariables().Concat(arguments);
        }

        public IEnumerable<IVariableModification> GetModifiedFields()
        {
            return SimpleStatements.SelectMany(a => a.Expressions).FilterExpression<VariableModification>();
        }

        public IEnumerable<IMethodInvocation> GetInvokedMethods()
        {
            return SimpleStatements.SelectMany(a => a.Expressions).FilterExpression<MethodInvocation>();
        }

        public int GetEmptyLinesCount()
        {
            var totalLinesCount = _startLineNumber - _endLineNumber;
            return totalLinesCount - GetLinesOfCodeCount();
        }

        public int GetLinesOfCodeCount()
        {
            return _countsOfDirectChildren + EmbeddedStatements.Sum(a => a.GetLinesOfCodeCount());
        }

        public IEnumerable<FieldDeclaration> GetDeclaredFields()
        {
            return SimpleStatements.SelectMany(a => a.Expressions).FilterExpression<FieldDeclaration>();
        }

        public void SetParent(IMethodBase parent)
        {
            Parent = parent;
        }

        public void SetEmbeddedStatements(IEnumerable<IEmbeddedStatement> statements)
        {
            EmbeddedStatements = statements.ToList();
        }

        public void SetSimpleStatements(IEnumerable<ISimpleStatement> statements)
        {
            SimpleStatements = statements.ToList();
        }

        private IEnumerable<VariableAccess> GetStatementsAccessedVariables()
        {
            return SimpleStatements.SelectMany(a => a.Expressions).FilterExpression<VariableAccess>();
        }
    }
}