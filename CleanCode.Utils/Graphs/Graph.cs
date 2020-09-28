using System.Collections.Generic;
using System.Linq;

namespace CleanCode.Utils.Graphs
{
    public abstract class GraphBase
    {
        protected List<int>[] AdjacencyList { get; private set; }

        protected GraphBase(int vertices)
        {
            InitializeEmptyAdjacencyList(vertices);
        }

        protected void DeepFirstSearch(int vertex, bool[] visited)
        {
            visited[vertex] = true;
            var unVisitedVertex = AdjacencyList[vertex].Where(i => !visited[i]);
            foreach (var i in unVisitedVertex) DeepFirstSearch(i, visited);
        }

        public abstract int GetConnectedComponents();
        public abstract void AddEdge(int fromNode, int toNode);

        private void InitializeEmptyAdjacencyList(int vertices)
        {
            AdjacencyList = new List<int>[vertices];
            for (var i = 0; i < AdjacencyList.Length; i++) AdjacencyList[i] = new List<int>();
        }
    }
}