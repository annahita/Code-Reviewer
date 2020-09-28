using System.IO;
using System.Linq;

namespace CleanCode.Utils.WordNet.searchableFile
{
    public class SearchableFile : ISearchableFile
    {
        private readonly char _splitter = ' ';
        private string _filePath;

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        public long GetLengthOfArray()
        {
            var fi = new FileInfo(_filePath);
            return fi.Length;
        }

        public string GetElement(long position)
        {
            string line;
            using (Stream stream = File.Open(_filePath, FileMode.Open))
            {
                stream.Seek(position, SeekOrigin.Begin);
                line = GetFirstCompleteLineAtPosition(stream);
            }

            var firstWord = line.Split(_splitter).First();
            return firstWord;
        }

        private string GetFirstCompleteLineAtPosition(Stream stream)
        {
            string firstCompleteLineAtPosition;
            using (var reader = new StreamReader(stream))
            {
                var goToPosition = reader.ReadLine();
                firstCompleteLineAtPosition = reader.ReadLine();
            }

            return firstCompleteLineAtPosition;
        }
    }
}