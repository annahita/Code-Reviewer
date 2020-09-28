using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CleanCode.Utils.Name
{
    public class Splitter : INameSplitter
    {
        private const string Separator = " ";

        public IEnumerable<string> Split(string objectName)
        {
            var separatedByCamel = SeparateCamelCaseBySpace(objectName);
            var replacedUnderLines = ReplaceUnderLineWithSpace(separatedByCamel);
            return replacedUnderLines.Split(Separator.ToCharArray());
        }

        private string SeparateCamelCaseBySpace(string input)
        {
            return Regex.Replace(input, SplittingPatterns.CamelCasePattern, Separator + "$1", RegexOptions.Compiled)
                .Trim();
        }

        private string ReplaceUnderLineWithSpace(string input)
        {
            return Regex.Replace(input, SplittingPatterns.UnderLinePattern, Separator, RegexOptions.Compiled).Trim();
        }
    }
}