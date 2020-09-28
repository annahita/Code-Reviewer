using CleanCode.Strategies.SideEffects;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Strategies.SideEffects
{
    [TestClass]
    public class ModifyStateSideEffectTests
    {
        [TestMethod]
        public void Method_ModifiedParameters_HasSideEffect()
        {
            const string methodParameter = "param1";
            var body = MockBlock.GetMethodBodyThatModifiedMethodParameters(methodParameter);
            var method = MockMethod.DefaultMethodWithBody(body);

            var sideEffectChecker = new ModifiedStateSideEffect();
            sideEffectChecker.CheckForSideEffects(method);
            var hasSideEffect = sideEffectChecker.HasSideEffects();

            Assert.IsTrue(hasSideEffect);
        }

        [TestMethod]
        public void Method_ModifiedStaticFields_HasSideEffect()
        {
            const string classStaticField = "staticFieldOfClass";
            var body = MockBlock.GetMethodBodyThatModifiedStaticFields(classStaticField);
            var method = MockMethod.DefaultMethodWithBody(body);

            var sideEffectChecker = new ModifiedStateSideEffect();
            sideEffectChecker.CheckForSideEffects(method);
            var hasSideEffect = sideEffectChecker.HasSideEffects();

            Assert.IsTrue(hasSideEffect);
        }
    }
}