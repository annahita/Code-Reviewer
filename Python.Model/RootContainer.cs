using System.Collections.Generic;
using OOP_Model;

namespace Python.Model
{
    public class RootContainer : EntityBase, IRootContainer
    {
        public RootContainer(int lineNumber) : base(lineNumber)
        {
        }

        public IEnumerable<IClassDeclaration> ClassDeclarations { get; private set; } = new List<ClassDeclaration>();


        public void SetClasses(IEnumerable<ClassDeclaration> classStructures)
        {
            ClassDeclarations = classStructures;
        }
    }
}