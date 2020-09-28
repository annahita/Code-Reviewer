using System.Collections.Generic;
using OOP_Model;

namespace Python.Model
{
    public class ClassDeclaration : EntityBase, IClassDeclaration
    {
        public ClassDeclaration(int lineNumber) : base(lineNumber)
        {
        }

        public string Name { get; private set; }
        public IEnumerable<string> SuperClasses { get; private set; } = new List<string>();
        public IEnumerable<IMethodBase> Methods { get; private set; } = new List<IMethodBase>();
        public IEnumerable<IMethodBase> Constructors { get; private set; } = new List<IMethodBase>();
        public IEnumerable<IFieldDeclaration> Fields { get; private set; } = new List<IFieldDeclaration>();

        public void SetSuperClasses(IEnumerable<string> superClassIdentifiers)
        {
            SuperClasses = superClassIdentifiers;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetMethods(IEnumerable<IMethodBase> methods)
        {
            Methods = methods;
        }

        public void SetConstructors(IEnumerable<IMethodBase> constructors)
        {
            Constructors = constructors;
        }

        public void SetFields(IEnumerable<IFieldDeclaration> fields)
        {
            Fields = fields;
        }
    }
}