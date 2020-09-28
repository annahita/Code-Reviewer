using System.Linq;
using CleanCode.Analyzers;
using CleanCode.Metrics;
using CleanCode.Test.TestHelpers;
using CleanCode.Test.TestHelpers.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCode.Test.Analyzers.Class
{
    [TestClass]
    public class ClassNameTests
    {
        [TestMethod]
        public void ClassName_StartsByVerb_HasAnalyzerError()
        {
            var partOfPeachRule = QualityRules.NounForClassName;
            var gerundName = "validate";

            var classDef = MockClass.EmptyClass(gerundName);
            var classNameAnalyzer = new ClassName(TestBootStrapped.GetInstanceOfNamingConvention());
            classNameAnalyzer.Analyze(classDef);
            var analyzeResult = classNameAnalyzer.GetResult();

            var hasRightPartOfPeach = !analyzeResult.Any(a => a.ViolatedRule.Equals(partOfPeachRule));

            Assert.IsFalse(hasRightPartOfPeach);
        }

        [TestMethod]
        public void ClassName_StartsByNoun_HasHasNotPosError()
        {
            var partOfPeachRule = QualityRules.NounForClassName;
            var nounName = "validation";

            var classDef = MockClass.EmptyClass(nounName);
            var classNameAnalyzer = new ClassName(TestBootStrapped.GetInstanceOfNamingConvention());
            classNameAnalyzer.Analyze(classDef);
            var analyzeResult = classNameAnalyzer.GetResult();

            var hasRightPartOfPeach = !analyzeResult.Any(a => a.ViolatedRule.Equals(partOfPeachRule));

            Assert.IsTrue(hasRightPartOfPeach);
        }

        [TestMethod]
        public void ClassName_HasLength_SmallerThanLimitSize_IsNotSearchable()
        {
            var searchableRule = QualityRules.SearchableName;
            var minimLength = new Metric().MinimumLengthOfName;
            var name = NameHelper.RandomString(minimLength - 1);

            var classDef = MockClass.EmptyClass(name);
            var classNameAnalyzer = new ClassName(TestBootStrapped.GetInstanceOfNamingConvention());
            classNameAnalyzer.Analyze(classDef);
            var analyzeResult = classNameAnalyzer.GetResult();

            var isSearchable = !analyzeResult.Any(a => a.ViolatedRule.Equals(searchableRule));

            Assert.IsFalse(isSearchable);
        }

        [TestMethod]
        public void ClassName_HasLength_BiggerThanLimitSize_IsSearchable()
        {
            var pronounceableRule = QualityRules.PronounceableName;
            var name = "ValidatePKDMN";

            var classDef = MockClass.EmptyClass(name);
            var classNameAnalyzer = new ClassName(TestBootStrapped.GetInstanceOfNamingConvention());
            classNameAnalyzer.Analyze(classDef);
            var analyzeResult = classNameAnalyzer.GetResult();

            var isPronounceable = !analyzeResult.Any(a => a.ViolatedRule.Equals(pronounceableRule));

            Assert.IsFalse(isPronounceable);
        }

        [TestMethod]
        public void ClassName_HasAPartThat_NotExistInDictionary_IsNotPronounceable()
        {
            var pronounceableRule = QualityRules.PronounceableName;
            var name = "ValidationKLMN";

            var classDef = MockClass.EmptyClass(name);
            var classNameAnalyzer = new ClassName(TestBootStrapped.GetInstanceOfNamingConvention());
            classNameAnalyzer.Analyze(classDef);
            var analyzeResult = classNameAnalyzer.GetResult();

            var isPronounceable = !analyzeResult.Any(a => a.ViolatedRule.Equals(pronounceableRule));

            Assert.IsFalse(isPronounceable);
        }

        [TestMethod]
        public void ClassName_WithPartsThat_ExistInDictionary_IsPronounceable()
        {
            var pronounceableRule = QualityRules.PronounceableName;
            var name = "CustomerValidation";

            var classDef = MockClass.EmptyClass(name);
            var classNameAnalyzer = new ClassName(TestBootStrapped.GetInstanceOfNamingConvention());
            classNameAnalyzer.Analyze(classDef);
            var analyzeResult = classNameAnalyzer.GetResult();

            var isPronounceable = !analyzeResult.Any(a => a.ViolatedRule.Equals(pronounceableRule));

            Assert.IsTrue(isPronounceable);
        }
    }
}