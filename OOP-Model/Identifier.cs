using System.Collections.Generic;

namespace OOP_Model
{
    public interface IIdentifier
    {
        IEnumerable<string> Id { get; }
        string GetTarget();
        string GetField();
        string GetIdentifiersAsString();
    }
}