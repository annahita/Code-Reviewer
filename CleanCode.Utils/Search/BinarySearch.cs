namespace CleanCode.Utils.Search
{
    public class WordNetBinarySearch
    {
        private ISearchableArray _array;
        private long _bot;
        private long _mid;
        private long _top;

        public bool Search(string searchKey, ISearchableArray array)
        {
            _array = array;
            _bot = array.GetLengthOfArray();
            return BinarySearch(searchKey);
        }

        private bool BinarySearch(string searchKey)
        {
            string key;

            do
            {
                CalculateMiddlePointOfFile();
                key = GetKeyOfMiddleElement();
                CalculateTopAndBottom(searchKey, key);
            } while (key != searchKey && IsSearchCompleted());

            return key == searchKey;
        }

        private bool IsSearchCompleted()
        {
            return _bot - _top > 1;
        }

        private void CalculateMiddlePointOfFile()
        {
            var diff = (_bot - _top) / 2;
            _mid = _top + diff;
        }

        private void CalculateTopAndBottom(string searchKey, string key)
        {
            if (string.CompareOrdinal(key, searchKey) > 0)
                _bot = _mid;
            else
                _top = _mid;
        }

        private string GetKeyOfMiddleElement()
        {
            return _array.GetElement(_mid - 1);
        }
    }
}