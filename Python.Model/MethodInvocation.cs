using System.Collections.Generic;
using System.Linq;
using OOP_Model;
using Python.Model.Extension;

namespace Python.Model
{
    public class MethodInvocation : EntityBase, IMethodInvocation
    {
        public MethodInvocation(int lineNumber) : base(lineNumber)
        {
        }

        public IEnumerable<IExpression> Arguments { get; private set; }

        public IIdentifier Identifier { get; private set; }
        public bool IsMemberOfParentClass { get; private set; }

        public void SetMethodIdentifier(Identifier identifier)
        {
            Identifier = identifier;
        }

        public void SetArguments(IEnumerable<IExpression> args)
        {
            Arguments = args;
        }

        public void SetMemberOfParentClassState(bool state)
        {
            IsMemberOfParentClass = state;
        }

        public IEnumerable<VariableAccess> GetArgumentsWithVariableType()
        {
            var nestedArguments = GetNestedArguments().FilterExpression<VariableAccess>();
            var directArguments = Arguments.FilterExpression<VariableAccess>();
            return nestedArguments.Concat(directArguments);
        }

        private IEnumerable<IExpression> GetNestedArguments()
        {
            var nestedArguments = new List<IExpression>();

            var argumentsWithMethodInvocationType = Arguments.FilterExpression<MethodInvocation>();
            foreach (var methodInvocation in argumentsWithMethodInvocationType)
                nestedArguments.AddRange(methodInvocation.GetNestedArguments());

            return nestedArguments;
        }
    }
}