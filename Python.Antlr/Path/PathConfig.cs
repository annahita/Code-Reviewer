using System.Collections.Generic;
using p = PythonParser;

namespace Python.Antlr.Path
{
    internal class PathConfig : ParserPath
    {
        private const string AnyWhere = "//";
        private const string WildCard = "*";
        private const string InPath = "/";

        private PathConfig(IEnumerable<object> elements) : base(elements)
        {
        }

        #region Root Path

        internal static readonly PathConfig DecoratorName
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_decorator, AnyWhere, p.RULE_name});

        internal static readonly PathConfig Name
            = new PathConfig(new object[] {AnyWhere, p.RULE_name});

        #endregion

        #region Class Path

        internal static readonly PathConfig ClassDef
            = new PathConfig(new object[]
                {InPath, WildCard, InPath, p.RULE_stmt, InPath, p.RULE_compound_stmt, InPath, p.RULE_classdef});

        internal static readonly PathConfig ClassName
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_name});

        internal static readonly PathConfig ClassArgs
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_arglist, InPath, p.RULE_argument});

        internal static readonly PathConfig ClassBodySmallStatements
            = new PathConfig(new object[]
            {
                InPath, WildCard, InPath, p.RULE_suite, InPath, p.RULE_stmt, InPath, p.RULE_simple_stmt, InPath,
                p.RULE_small_stmt
            });

        #endregion

        #region FunctionDef Path

        internal static readonly PathConfig FunctionName
            = new PathConfig(new object[] {AnyWhere, p.RULE_funcdef, InPath, p.RULE_name});

        internal static readonly PathConfig FunctionParameters
            = new PathConfig(new object[]
            {
                InPath, p.RULE_funcdef, InPath, p.RULE_typedargslist, InPath, p.RULE_def_parameters, InPath,
                p.RULE_def_parameter
            });

        internal static readonly PathConfig ParameterName
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_named_parameter, InPath, p.RULE_name});

        internal static readonly PathConfig FunctionBlock
            = new PathConfig(new object[] {InPath, p.RULE_funcdef, InPath, p.RULE_suite});

        internal static readonly PathConfig SmallStatesmenAnywhere
            = new PathConfig(new object[] {AnyWhere, p.RULE_simple_stmt, InPath, p.RULE_small_stmt});

        internal static readonly PathConfig FirstLevelStatementsInBlock
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_stmt});

        #endregion


        #region EmbededStructures Path

        internal static readonly PathConfig CompoundStatementInFunctionSuite
            = new PathConfig(new object[]
                {InPath, WildCard, InPath, p.RULE_stmt, InPath, p.RULE_compound_stmt});

        internal static readonly PathConfig FirstLevelStatementsInCompoundStatement
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_suite, InPath, p.RULE_stmt});

        internal static readonly PathConfig NestedCompoundStatement
            = new PathConfig(new object[]
                {InPath, WildCard, InPath, p.RULE_suite, InPath, p.RULE_stmt, InPath, p.RULE_compound_stmt});

        #endregion

        #region ExpressionStatement Path

        internal static readonly PathConfig StatementLeftPart
            = new PathConfig(new object[]
                {InPath, WildCard, InPath, p.RULE_testlist_star_expr, AnyWhere, p.RULE_expr_element});

        internal static readonly PathConfig StatementRightPart
            = new PathConfig(new object[]
                {InPath, WildCard, InPath, p.RULE_assign_part, AnyWhere, p.RULE_expr_element});

        internal static readonly PathConfig ExprInStatement
            = new PathConfig(new object[] {InPath, WildCard, InPath, p.RULE_testlist, AnyWhere, p.RULE_expr_element});

        internal static readonly PathConfig ExprElementAnywhere
            = new PathConfig(new object[] {AnyWhere, p.RULE_expr_element});

        internal static readonly PathConfig InvokedMethodArgumentCollection
            = new PathConfig(new object[]
            {
                InPath, WildCard, InPath, p.RULE_arguments
            });

        internal static readonly PathConfig Arguments
            = new PathConfig(new object[]
            {
                InPath, WildCard, InPath, p.RULE_arglist, InPath, p.RULE_argument
            });


        internal static readonly PathConfig ExprElementIdentifier
            = new PathConfig(new object[]
                {InPath, p.RULE_expr_element, InPath, p.RULE_identifier, AnyWhere, p.RULE_name});

        #endregion
    }
}