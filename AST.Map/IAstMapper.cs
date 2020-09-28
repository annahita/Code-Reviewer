namespace Python.AST.Map
{
    internal interface IAstMapper<out T>
    {
        T GetMappedItem();
    }
}