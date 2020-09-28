using System.Collections.Generic;
using OOP_Model;

namespace Python.Model
{
    public class MethodDeclaration : EntityBase, IMethodBase
    {
        public MethodDeclaration(int lineNumber) : base(lineNumber)
        {
        }

        public ClassDeclaration ParentClass { get; private set; }
        public ParameterDeclaration InstancePointer { get; private set; }
        public bool IsStatic { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<IParameterDeclaration> Parameters { get; private set; } = new List<ParameterDeclaration>();
        public IMethodBody MethodBody { get; private set; }


        public void SetParameters(IEnumerable<ParameterDeclaration> parameters)
        {
            Parameters = parameters;
        }

        public void SetInstancePointer(ParameterDeclaration instancePointer)
        {
            InstancePointer = instancePointer;
        }


        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAsStaticMethod()
        {
            IsStatic = true;
        }

        public void SetMethodBody(IMethodBody body)
        {
            MethodBody = body;
        }

        public void SetParentClass(ClassDeclaration parent)
        {
            ParentClass = parent;
        }

        public bool HasInstancePointer()
        {
            return !IsStatic;
        }
    }
}