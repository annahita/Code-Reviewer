using System.Collections.Generic;

namespace CleanCode.Utils.Name
{
    public interface INameSplitter
    {
        IEnumerable<string> Split(string objectName);
    }
}