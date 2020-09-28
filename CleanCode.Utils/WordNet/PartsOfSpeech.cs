using System.ComponentModel;

namespace CleanCode.Utils.WordNet
{
    public enum PartsOfSpeech
    {
        [Description("noun")] Noun = 1,
        [Description("verb")] Verb = 2,
        [Description("adj")] Adj = 3,
        [Description("adv")] Adv = 4,
        [Description("stopWord")] StopWord = 5
    }

    public static class MyEnumExtensions
    {
        public static string ToDescriptionString(this PartsOfSpeech val)
        {
            var attributes = (DescriptionAttribute[]) val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}