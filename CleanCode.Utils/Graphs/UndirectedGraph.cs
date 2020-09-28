namespace CleanCode.Utils.Graphs
{
    public class UndirectedGraph : GraphBase
    {
        private readonly int _vertices;

        public UndirectedGraph(int vertices) : base(vertices)
        {
            _vertices = vertices;
        }

        public override void AddEdge(int fromNode, int toNode)
        {
            AdjacencyList[fromNode].Add(toNode);
            AdjacencyList[toNode].Add(fromNode);
        }

        public override int GetConnectedComponents()
        {
            var visited = new bool[_vertices];

            var counter = 0;
            for (var i = 0; i < _vertices; i++)
                if (!visited[i])
                {
                    DeepFirstSearch(i, visited);
                    counter++;
                }

            return counter;
        }
    }
}