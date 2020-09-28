using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Metrics;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Analyzers.Method
{
    [TestClass]
    public class MethodNameTests
    {
        [TestMethod]
        public void MethodName_StartsByNoun_HasAnalyzerError()
        {
            var partOfPeachRule = QualityRules.VerbForMethodName;
            var nounName = "validation";

            var method = MockMethod.EmptyMethod(nounName);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var hasRightPartOfPeach = !analyzeResult.Any(a => a.ViolatedRule.Equals(partOfPeachRule));

            Assert.IsFalse(hasRightPartOfPeach);
        }

        [TestMethod]
        public void MethodName_StartsByGerunds_HasAnalyzerError()
        {
            var partOfPeachRule = QualityRules.VerbForMethodName;
            var gerundName = "validating";

            var method = MockMethod.EmptyMethod(gerundName);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var hasRightPartOfPeach = !analyzeResult.Any(a => a.ViolatedRule.Equals(partOfPeachRule));

            Assert.IsFalse(hasRightPartOfPeach);
        }

        [TestMethod]
        public void MethodName_StartsByGerunds_HasNotPosError()
        {
            var partOfSpeachRule = QualityRules.VerbForMethodName;
            var verbName = "validate";

            var method = MockMethod.EmptyMethod(verbName);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var hasRightPartOfSpeech = !analyzeResult.Any(a => a.ViolatedRule.Equals(partOfSpeachRule));

            Assert.IsTrue(hasRightPartOfSpeech);
        }

        [TestMethod]
        public void MethodName_HasLenght_SmallerThanLimitSize_ISNotSearchable()
        {
            var searchableRule = QualityRules.SearchableName;
            var minimLength = new Metric().MinimumLengthOfName;
            var name = NameHelper.RandomString(minimLength - 1);

            var method = MockMethod.EmptyMethod(name);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var isSearchable = !analyzeResult.Any(a => a.ViolatedRule.Equals(searchableRule));

            Assert.IsFalse(isSearchable);
        }

        [TestMethod]
        public void MethodName_HasLenght_BiggerThanLimitSize_ISSearchable()
        {
            var pronounceableRule = QualityRules.PronounceableName;
            var name = "ValidatePKDMN";

            var method = MockMethod.EmptyMethod(name);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var isPronounceable = !analyzeResult.Any(a => a.ViolatedRule.Equals(pronounceableRule));

            Assert.IsFalse(isPronounceable);
        }

        [TestMethod]
        public void MethodName_HasAPartThat_NotExistInDictionary_IsNotPronounceable()
        {
            var pronounceableRule = QualityRules.PronounceableName;
            var name = "ValidateKMLS";

            var method = MockMethod.EmptyMethod(name);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var isPronounceable = !analyzeResult.Any(a => a.ViolatedRule.Equals(pronounceableRule));

            Assert.IsFalse(isPronounceable);
        }

        [TestMethod]
        public void MethodName_WithPartsThat_ExistInDictionary_IsPronounceable()
        {
            var pronounceableRule = QualityRules.PronounceableName;
            var name = "ValidateSomeFieldsWithSmallName";

            var method = MockMethod.EmptyMethod(name);
            var methodNameAnalyzer = new MethodName(TestBootStrapped.GetInstanceOfNamingConvention());
            methodNameAnalyzer.Analyze(method);
            var analyzeResult = methodNameAnalyzer.GetResult();

            var isPronounceable = !analyzeResult.Any(a => a.ViolatedRule.Equals(pronounceableRule));

            Assert.IsTrue(isPronounceable);
        }
    }
}