using System;
using System.Collections.Generic;
using OOP_Model;
using Python.Antlr.ElementFinders;
using Python.AST.Map.Expressions.Variables.Mappers;
using Python.AST.Map.RelationResolver;
using Python.Model;

namespace Python.AST.Map.Expressions.Variables.Strategies
{
    internal class AlteredVariableStrategy : IVariableMappingStrategy
    {
        private Dictionary<ExpressionType, Func<IIdentifierElementFinder, Identifier, IAstMapper<IExpression>>>
            _mapperFactory;

        internal AlteredVariableStrategy()

        {
            FillMapperFactory();
        }

        private IParentReferenceResolver Parent { get; set; }

        public IAstMapper<IExpression> GetMapper(IIdentifierElementFinder elementFinder, Identifier identifier)
        {
            var expressionType = Parent.ResolveType(identifier);
            return _mapperFactory[expressionType].Invoke(elementFinder, identifier);
        }

        public void SetParent(IParentReferenceResolver parent)
        {
            Parent = parent;
        }

        private void FillMapperFactory()
        {
            _mapperFactory =
                new Dictionary<ExpressionType, Func<IIdentifierElementFinder, Identifier, IAstMapper<IExpression>>>
                {
                    {ExpressionType.UndefinedClassInstanceField, ClassInstanceFieldMapper},
                    {ExpressionType.UndefinedClassStaticField, ClassStaticFieldMapper},
                    {ExpressionType.UndefinedLocalVariable, LocalVariableMapper},
                    {ExpressionType.AccessedMember, ModifiedVariableMapper},
                    {ExpressionType.UndefinedField, ModifiedVariableMapper}
                };
        }

        private IAstMapper<IExpression> ModifiedVariableMapper(IIdentifierElementFinder elementFinder,
            Identifier identifier)
        {
            var expression = Parent.FindMember(identifier);
            return new ModifiedVariableMapper(elementFinder, identifier, expression);
        }

        private IAstMapper<IExpression> ClassInstanceFieldMapper(IIdentifierElementFinder elementFinder,
            Identifier identifier)
        {
            return new ClassInstanceFieldMapper(elementFinder, identifier);
        }

        private IAstMapper<IExpression> ClassStaticFieldMapper(IIdentifierElementFinder elementFinder,
            Identifier identifier)
        {
            return new ClassStaticFieldMapper(elementFinder, identifier);
        }

        private IAstMapper<IExpression> LocalVariableMapper(IIdentifierElementFinder elementFinder,
            Identifier identifier)
        {
            return new LocalVariableMapper(elementFinder, identifier);
        }
    }
}