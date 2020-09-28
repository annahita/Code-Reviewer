using CleanCode.Utils.WordNet;

namespace CleanCode.Strategies.Name
{
    public interface INamingConvention
    {
        bool IsPronounceable(string name);
        bool IsSearchable(string name);
        bool HasWritePartOfSpeech(string name, PartsOfSpeech partsOfSpeech);
    }
}