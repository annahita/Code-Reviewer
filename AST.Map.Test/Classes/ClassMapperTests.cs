using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.AST.Map.Classes;
using Python.AST.Map.Test.Helpers;

namespace Python.AST.Map.Test.Classes
{
    [TestClass]
    public class ClassMapperTests
    {
        private const string NewClassName = "newClass";

        [TestMethod]
        public void ClassMapperTest()
        {
            const string superClass = "superClass";
            var elementFinder = new FakeClassElementFinder(NewClassName, new[] {superClass});
            var mapper = new ClassMapper(elementFinder);

            var mappedClass = mapper.GetMappedItem();
            var mappedName = mappedClass.Name;
            var mappedSupperClass = mappedClass.SuperClasses.FirstOrDefault();

            Assert.AreEqual(NewClassName, mappedName);
            Assert.AreEqual(superClass, mappedSupperClass);
        }

        [TestMethod]
        public void TestClass_StaticFieldsMap()
        {
            const string staticFieldId = "newStaticFieldId";
            var assignStatement = BuildAssignment(new[] {staticFieldId});
            var elementFinder = new FakeClassElementFinder(NewClassName, new[] {assignStatement});
            var mapper = new ClassMapper(elementFinder);

            var mappedClass = mapper.GetMappedItem();
            var mappedField = mappedClass.Fields.FirstOrDefault();
            var mappedFieldName = mappedField.Identifier.GetField();

            Assert.AreEqual(staticFieldId, mappedFieldName);
            Assert.IsTrue(mappedField.IsStatic);
        }

        [TestMethod]
        public void TestClass_InstanceFieldsMap()
        {
            const string instanceFieldId = "newInstanceFieldId";

            var constructor = BuildConstructor(instanceFieldId);
            var elementFinder = new FakeClassElementFinder(NewClassName, constructor);
            var mapper = new ClassMapper(elementFinder);

            var mappedClass = mapper.GetMappedItem();
            var mappedField = mappedClass.Fields.FirstOrDefault();
            var mappedFieldName = mappedField.Identifier.GetField();

            Assert.AreEqual(instanceFieldId, mappedFieldName);
            Assert.IsFalse(mappedField.IsStatic);
        }


        [TestMethod]
        public void TestClass_MethodMap()
        {
            const string newMethodName = "newMethod";
            const string instancePointer = "InstancePointer";

            var methodElementsFinder = new FakeInstanceMethodElementFinder(newMethodName, new[] {instancePointer});
            var elementFinder = new FakeClassElementFinder(NewClassName, new[] {methodElementsFinder});

            var mapper = new ClassMapper(elementFinder);
            var mappedClass = mapper.GetMappedItem();
            var mappedMethods = mappedClass.Methods.FirstOrDefault();
            var mappedMethodsName = mappedMethods.Name;

            Assert.AreEqual(newMethodName, mappedMethodsName);
        }


        private FakeInstanceMethodElementFinder BuildConstructor(string declaredInstanceField)
        {
            const string constructorName = "_init_";
            const string instancePointer = "self";
            var body = BuildMethodBody(declaredInstanceField, instancePointer);

            return new FakeInstanceMethodElementFinder(constructorName, new[] {instancePointer}, body);
        }

        private FakeIMethodBodyElementFinder BuildMethodBody(string declaredInstanceField, string instancePointer)
        {
            string[] instanceFieldId = {instancePointer, declaredInstanceField};
            var assignStatement = BuildAssignment(instanceFieldId);
            return new FakeIMethodBodyElementFinder(new[] {assignStatement});
        }

        private FakeAssignStatementElementFinder BuildAssignment(string[] identifier)
        {
            var leftPart = identifier;
            string[] rightPart = {"someValue"};

            return new FakeAssignStatementElementFinder(leftPart, rightPart);
        }
    }
}