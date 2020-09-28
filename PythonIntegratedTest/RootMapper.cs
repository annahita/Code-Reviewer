using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr;
using Python.AST.Map;
using Python.Model;
using PythonIntegratedTest.Templates;

namespace PythonIntegratedTest
{
    [TestClass]
    public class UnitTest1
    {
        private ClassDeclaration _mainClass;
        private RootContainer _mappedRoot;
        private PythonCodeTemplate _template;

        [TestInitialize]
        public void TestInit()
        {
            _template = new PythonCodeTemplate();
            var code = _template.TransformText();
            var rootMapper = new RootMapper(new ParserBuilder());
            rootMapper.MapElements(code);
            _mappedRoot = rootMapper.GetMappedItem();
            _mainClass = (ClassDeclaration) _mappedRoot.ClassDeclarations.FirstOrDefault();
        }

        [TestMethod]
        public void MappedAllClassesInRootFile()
        {
            var expectedClassCount = 2;

            var classes = _mappedRoot.ClassDeclarations;

            Assert.AreEqual(expectedClassCount, classes.Count());
        }

        [TestMethod]
        public void MappedClassStaticFields()
        {
            var mappedStaticField = _mainClass.Fields.FirstOrDefault(a => a.IsStatic);

            var mappedStaticFieldName = mappedStaticField.Identifier.GetField();

            Assert.AreEqual(_template.StaticField1, mappedStaticFieldName);
        }

        [TestMethod]
        public void MappedStaticMethods()
        {
            var mappedStaticMethod = _mainClass.Methods.FirstOrDefault(a => ((MethodDeclaration) a).IsStatic);

            var parametersNames = mappedStaticMethod.Parameters.Select(a => a.Name).ToList();

            Assert.AreEqual(_template.StaticMethod, mappedStaticMethod.Name);
            CollectionAssert.Contains(parametersNames, _template.Param1);
            CollectionAssert.Contains(parametersNames, _template.Param2);
        }

        [TestMethod]
        public void FirstParameter_InInstanceMethod_IsInstancePointer()
        {
            var mappedMethod =
                (MethodDeclaration) _mainClass.Methods.FirstOrDefault(a => !((MethodDeclaration) a).IsStatic);
            var parametersNames = mappedMethod.Parameters.Select(a => a.Name).ToList();

            Assert.AreEqual(_template.Param1, mappedMethod.InstancePointer.Name);
            CollectionAssert.DoesNotContain(parametersNames, _template.Param1);

            CollectionAssert.Contains(parametersNames, _template.Param2);
        }

        [TestMethod]
        public void MappedFieldModificationsInMethodBody()
        {
            const int expectedModificationCount = 1;
            var mappedMethod =
                (MethodDeclaration) _mainClass.Methods.FirstOrDefault(a => !((MethodDeclaration) a).IsStatic);

            var methodBody = (MethodBody) mappedMethod.MethodBody;
            var modifiedFields = methodBody.GetModifiedFields().ToList();
            var modifiedLocalVariable = modifiedFields.Where(a => a.IsLocalVariable());
            var modifiedMethodParam = modifiedFields.Where(a => a.IsMethodParameter());
            var modifiedClassStaticFields = modifiedFields.Where(a => a.IsMemberOfParentClass() && a.IsStatic());
            var modifiedClassInstanceFields = modifiedFields.Where(a => a.IsMemberOfParentClass() && !a.IsStatic());

            Assert.AreEqual(expectedModificationCount, modifiedLocalVariable.Count());
            Assert.AreEqual(expectedModificationCount, modifiedMethodParam.Count());
            Assert.AreEqual(expectedModificationCount, modifiedClassStaticFields.Count());
            Assert.AreEqual(expectedModificationCount, modifiedClassInstanceFields.Count());
        }

        [TestMethod]
        public void MappedFieldAccesseInMethodBody()
        {
            const int expectedModificationCount = 1;
            var mappedInstanceMethods = _mainClass.Methods.Where(a => !((MethodDeclaration) a).IsStatic).ToList();
            var mappedMethod = (MethodDeclaration) mappedInstanceMethods[1];

            var methodBody = (MethodBody) mappedMethod.MethodBody;
            var accessedFields = methodBody.GetAccessedFields().ToList();
            var accessedLocalVariable = accessedFields.Where(a => a.IsLocalVariable());
            var accessedMethodParam = accessedFields.Where(a => a.IsMethodParameter());
            var accessedClassStaticFields = accessedFields.Where(a => a.IsMemberOfParentClass() && a.IsStatic());
            var accessedClassInstanceFields = accessedFields.Where(a => a.IsMemberOfParentClass() && !a.IsStatic());
            var accessedUndignifiedFields = accessedFields.Where(a => a.IsUndefinedAccess());

            Assert.AreEqual(expectedModificationCount, accessedLocalVariable.Count());
            Assert.AreEqual(expectedModificationCount, accessedMethodParam.Count());
            Assert.AreEqual(expectedModificationCount, accessedClassStaticFields.Count());
            Assert.AreEqual(expectedModificationCount, accessedClassInstanceFields.Count());
            Assert.AreEqual(expectedModificationCount, accessedUndignifiedFields.Count());
        }
    }
}