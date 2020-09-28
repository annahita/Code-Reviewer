using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Antlr.ElementFinders;
using Python.Antlr.Test.PythonCodeTemplates;

namespace Python.Antlr.Test.ElementFinders
{
    [TestClass]
    public class ClassElementFinderTests
    {
        private const string ClassName = "NewClass";

        private readonly List<(string, string)> _assignmentStatements = new List<(string, string)>
            {("target1", "value1"), ("target2", "value2")};

        private readonly string[] _methodNames = {"NewMethod", "NewMethod2"};
        private readonly string[] _superClassesName = {"superClass1", "superClass2"};
        private ClassElementFinder _elementFinder;


        [TestInitialize]
        public void TestInit()
        {
            var classBody = PythonCodeGenerator.GenerateClassBody(_assignmentStatements, _methodNames);
            var classDef = PythonCodeGenerator.GenerateClass(ClassName, _superClassesName, classBody);
            var parser = TestParser.GetParser(classDef);

            _elementFinder = new ClassElementFinder(parser.classdef(), parser);
        }

        [TestMethod]
        public void FindClassNameTest()
        {
            var actualClassName = _elementFinder.FindName();

            Assert.AreEqual(ClassName, actualClassName);
        }

        [TestMethod]
        public void FindSuperClassesTest()
        {
            var actualSuperClasses = _elementFinder.FindSuperClasses().ToList();

            CollectionAssert.Contains(actualSuperClasses, _superClassesName[0]);
            CollectionAssert.Contains(actualSuperClasses, _superClassesName[1]);
        }


        [TestMethod]
        public void FindAssignStatementsTest()
        {
            var actualAssignStatements = _elementFinder.FindAssignStatements();
            Assert.AreEqual(_assignmentStatements.Count, actualAssignStatements.Count());
        }

        [TestMethod]
        public void FindConstructorsTest()
        {
            var constructors = _elementFinder.FindConstructors();
            var hasConstructors = constructors.Any();

            Assert.IsTrue(hasConstructors);
        }

        [TestMethod]
        public void FindMethodsTest()
        {
            var actualMethods = _elementFinder.FindInstanceMethods();

            Assert.AreEqual(_methodNames.Length, actualMethods.Count());
        }

        [TestMethod]
        public void ConstructorShouldNotBeInMethodList()
        {
            var actualMethods = _elementFinder.FindInstanceMethods();
            var actualConstructors = _elementFinder.FindConstructors();
            var hasConstructors = actualConstructors.Any();

            Assert.IsTrue(hasConstructors);
            Assert.AreEqual(_methodNames.Length, actualMethods.Count());
        }
    }
}