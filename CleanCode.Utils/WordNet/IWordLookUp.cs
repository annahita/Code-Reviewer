using System.Collections.Generic;

namespace CleanCode.Utils.WordNet
{
    public interface IWordLookUp
    {
        IEnumerable<PartsOfSpeech> FindWordPartOfSpeeches(string word);
    }
}