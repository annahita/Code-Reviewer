using CleanCode.Utils.Search;

namespace CleanCode.Utils.WordNet.searchableFile
{
    public interface ISearchableFile : ISearchableArray
    {
        void SetFilePath(string filePath);
    }
}