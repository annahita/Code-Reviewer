using Python.Model;

namespace Python.AST.Map
{
    public interface IRootMapper
    {
        void MapElements(string code);
        RootContainer GetMappedItem();
    }
}