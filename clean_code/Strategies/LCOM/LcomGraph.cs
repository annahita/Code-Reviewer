using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CleanCode.Utils.Graphs;
using OOP_Model;

[assembly: InternalsVisibleTo("CleanCode.Test")]

namespace CleanCode.Strategies.LCOM
{
    internal sealed class Lcom4Graph
    {
        private readonly IEnumerable<IMethodBase> _classMethods;
        private UndirectedGraph _classGraph;

        internal Lcom4Graph(IEnumerable<IMethodBase> classMethods)
        {
            _classMethods = classMethods;
            CreateGraph();
        }

        internal int CountConnectedComponents()
        {
            return _classGraph.GetConnectedComponents();
        }

        private void CreateGraph()
        {
            _classGraph = new UndirectedGraph(_classMethods.Count());

            for (var i = 0; i < _classMethods.Count(); i++)
                 for (var j = i + 1; j < _classMethods.Count(); j++)
                      AddEdgeForConnectedMethods(i, j);
        }

        private void AddEdgeForConnectedMethods(int vertexI, int vertexJ)
        {
            if (AreConnected(_classMethods.ElementAt(vertexI), _classMethods.ElementAt(vertexJ)))
                _classGraph.AddEdge(vertexI, vertexJ);
        }

        private bool AreConnected(IMethodBase method1, IMethodBase method2)
        {
            var intersectOfUsedFields = GetUsedClassFieldsIn(method1.MethodBody)
                .Intersect(GetUsedClassFieldsIn(method2.MethodBody));
            var connectedBySharedClassField = intersectOfUsedFields.Any();

            var connectedByCallMethod = IsCalledByMethod(method1.MethodBody, method2) ||
                                        IsCalledByMethod(method2.MethodBody, method1);

            return connectedBySharedClassField || connectedByCallMethod;
        }

        private bool IsCalledByMethod(IMethodBody callerMethodBody, IMethodBase subjectMethod)
        {
            var localInvokedMethods = GetLocalInvokedMethods(callerMethodBody);
            var localInvokedMethodsName = localInvokedMethods.Select(a => a.Identifier.GetField());
            return localInvokedMethodsName.Any(a => a.Equals(subjectMethod.Name));
        }

        private IEnumerable<string> GetUsedClassFieldsIn(IMethodBody methodBlock)
        {
            var classModifiedFields = GetParentClassModifiedFields(methodBlock).Select(a => a.GetTarget());
            var classAccessedFields = GetParentClassAccessedFields(methodBlock).Select(a => a.GetTarget());
            return classModifiedFields.Concat(classAccessedFields);
        }

        private IEnumerable<IMethodInvocation> GetLocalInvokedMethods(IMethodBody callerMethodBody)
        {
            return callerMethodBody.GetInvokedMethods().Where(a => a.IsMemberOfParentClass);
        }

        private IEnumerable<IIdentifier> GetParentClassAccessedFields(IMethodBody methodBlock)
        {
            return methodBlock.GetAccessedFields().Where(a => a.IsMemberOfParentClass()).Select(a => a.Identifier);
        }

        private IEnumerable<IIdentifier> GetParentClassModifiedFields(IMethodBody methodBlock)
        {
            return methodBlock.GetModifiedFields().Where(a => a.IsMemberOfParentClass()).Select(a => a.Identifier);
        }
    }
}
