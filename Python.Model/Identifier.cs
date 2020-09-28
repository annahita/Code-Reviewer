using System.Collections.Generic;
using System.Linq;
using OOP_Model;

namespace Python.Model
{
    public class Identifier : IIdentifier
    {
        private readonly string _nullInstancePointer = string.Empty;

        public string InstancePointer { get; private set; }

        public IEnumerable<string> Id { get; private set; } = new List<string>();

        public string GetTarget()
        {
            return Id.FirstOrDefault();
        }

        public string GetField()
        {
            return Id.LastOrDefault();
        }

        public string GetIdentifiersAsString()
        {
            return string.Join(".", Id);
        }

        public void SetInstancePointer(string instancePointer)
        {
            InstancePointer = instancePointer;
        }

        public void SetId(IEnumerable<string> identifier)
        {
            Id = identifier;
        }

        public bool HasInstancePointer()
        {
            return !InstancePointer.Equals(_nullInstancePointer);
        }
    }
}