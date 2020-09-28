using System.Linq;
using CleanCode.Metrics;
using CleanCode.Utils.Name;
using CleanCode.Utils.WordNet;

namespace CleanCode.Strategies.Name
{
    public class NamingConvention : INamingConvention
    {
        private readonly int _nameMinLength;
        private readonly INameSplitter _nameSplitter;
        private readonly IWordLookUp _wordLookUp;

        public NamingConvention(IWordLookUp wordLookUp, INameSplitter nameSplitter, IMetric metric)
        {
            _nameSplitter = nameSplitter;
            _wordLookUp = wordLookUp;
            _nameMinLength = metric.MinimumLengthOfName;
        }

        public bool IsPronounceable(string name)
        {
            var nameParts = _nameSplitter.Split(name);
            return nameParts.All(IsNamePartPronounceable);
        }

        public bool IsSearchable(string name)
        {
            var hasMinimumLength = name.Length >= _nameMinLength;
            return hasMinimumLength;
        }

        public bool HasWritePartOfSpeech(string name, PartsOfSpeech partsOfSpeech)
        {
            var firstPartOfName = _nameSplitter.Split(name).FirstOrDefault();
            var partOfSpeeches = _wordLookUp.FindWordPartOfSpeeches(firstPartOfName.ToLower());
            return partOfSpeeches.Contains(partsOfSpeech);
        }

        private bool IsNamePartPronounceable(string name)
        {
            return _wordLookUp.FindWordPartOfSpeeches(name.ToLower()).Any();
        }
    }
}