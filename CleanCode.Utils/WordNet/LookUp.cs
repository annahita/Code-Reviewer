using System;
using System.Collections.Generic;
using System.Linq;
using CleanCode.Utils.Search;
using CleanCode.Utils.WordNet.searchableFile;

namespace CleanCode.Utils.WordNet
{
    public class LookUp : IWordLookUp
    {
        private readonly string _index;
        private readonly string _path;
        private readonly ISearchableFile _searchableFile;

        public LookUp(ISearchableFile searchableFile)
        {
            _path = Setting.DirectoryPath;
            _index = Setting.IndexPrefix;
            _searchableFile = searchableFile;
        }

        public IEnumerable<PartsOfSpeech> FindWordPartOfSpeeches(string word)
        {
            return GetAllPartOfSpeeches().Where(pos => CheckWordPartOfSpeech(word, pos));
        }

        private bool CheckWordPartOfSpeech(string word, PartsOfSpeech pos)
        {
            _searchableFile.SetFilePath(GetIndexFilePath(pos));
            var searchMethod = new WordNetBinarySearch();
            return searchMethod.Search(word, _searchableFile);
        }

        private IEnumerable<PartsOfSpeech> GetAllPartOfSpeeches()
        {
            return Enum.GetValues(typeof(PartsOfSpeech)).Cast<PartsOfSpeech>();
        }

        private string GetIndexFilePath(PartsOfSpeech n)
        {
            return _path + _index + n.ToDescriptionString();
        }
    }
}