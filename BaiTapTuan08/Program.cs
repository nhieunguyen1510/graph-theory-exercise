using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapTuan08
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> graph = new List<List<int>>();
            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                int size = Int32.Parse(sr.ReadLine());
                for (int i = 0; i < size; i++)
                {
                    List<int> row = new List<int>();
                    string line = sr.ReadLine();
                    string[] values = line.Split(' ');
                    Array.ForEach(values, v => row.Add(Int32.Parse(v)));
                    graph.Add(row);
                }
            }
            int eulerType = Program.checkEulerGraph(graph);
            string statement = "";
            string path = "";
            if (eulerType == Program.EULER_GRAPH_TYPE.NOT_EULER)
            {
                statement = "Do thi khong co tinh chat Euler";
            }
            else if (eulerType == Program.EULER_GRAPH_TYPE.HALF_EULER)
            {
                statement = "Day la do thi nua Euler";
                path = "Duong di Euler: ";
                List<int> oddNodes = new List<int>();
                int size = graph.Count;
                for (int i = 0; i < size; i++)
                {
                    int deg = graph[i].Count(isEdge => isEdge == 1);
                    if (deg % 2 != 0)
                    {
                        oddNodes.Add(i);
                    }
                    if (oddNodes.Count == 2)
                    {
                        break;
                    }
                }
                List<int> eulerCycle = Program.findEulerCycle(graph, oddNodes[0]);
                path += String.Join(" ", eulerCycle.ToArray());
            }
            else
            {
                statement = "Day la do thi Euler.";
                path = "Chu trinh Euler: ";
                List<int> eulerCycle = Program.findEulerCycle(graph);
                path += String.Join(" ", eulerCycle.ToArray());
            }
            Console.WriteLine(statement);
            Console.WriteLine("{0}", path);
        }

        static List<int> findEulerCycle(List<List<int>> graph, int from = -1)
        {
            int size = graph.Count;
            List<List<int>> graphAfterVisit = Program.cloneGraph(graph);
            int startNode = from;
            for (int i = 0; i < size; i++)
            {
                if (startNode != -1)
                {
                    break;
                }
                for (int j = 0; j < size; j++)
                {
                    if (graph[i][j] == 1)
                    {
                        startNode = i;
                        break;
                    }
                }
            }
            List<int> eulerCycle = new List<int>();
            int currentNode = startNode;
            while (currentNode != -1)
            {
                eulerCycle.Add(currentNode);
                int nextNode = -1;
                for (int i = 0; i < size; i++)
                {
                    if (graphAfterVisit[currentNode][i] == 1)
                    {
                        nextNode = i;
                        List<List<int>> cloned = Program.cloneGraph(graphAfterVisit);
                        cloned[currentNode][i] = 0;
                        cloned[i][currentNode] = 0;
                        BFS bfsIns = new BFS(cloned);
                        if (bfsIns.checkNodeConnection(currentNode, i))
                        {
                            break;
                        }
                    }
                }
                if (nextNode != -1)
                {
                    graphAfterVisit[currentNode][nextNode] = 0;
                    graphAfterVisit[nextNode][currentNode] = 0;
                }
                currentNode = nextNode;
            }
            return eulerCycle;
        }


        static int checkEulerGraph(List<List<int>> graph)
        {
            int size = graph.Count;
            int oddVertexCount = 0;
            for (int i = 0; i < size; i++)
            {
                int deg = graph[i].Count(isEdge => isEdge == 1);
                if (deg % 2 != 0)
                {
                    oddVertexCount++;
                }
                if (oddVertexCount > 2)
                {
                    break;
                }
            }
            if (oddVertexCount == 0)
            {
                return Program.EULER_GRAPH_TYPE.COMPLETE_EULER;
            }

            if (oddVertexCount <= 2)
            {
                return Program.EULER_GRAPH_TYPE.HALF_EULER;
            }
            return Program.EULER_GRAPH_TYPE.NOT_EULER;
        }

        static public List<List<int>> cloneGraph(List<List<int>> graph)
        {
            int size = graph.Count;
            List<List<int>> cloned = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                cloned.Add(new List<int>());
                for (int j = 0; j < size; j++)
                {
                    cloned[i].Add(graph[i][j]);
                }
            }
            return cloned;
        }
        public class BFS
        {
            public List<List<int>> graph;
            public int size;
            public BFS(List<List<int>> graph)
            {
                size = graph.Count;
                this.graph = graph;
            }
            public bool checkNodeConnection(int startNode, int endNode)
            {
                List<int> visitedNodes = new List<int>();
                Queue<int> bfsQueue = new Queue<int>();
                bfsQueue.Enqueue(startNode);
                visitedNodes.Add(startNode);
                bool isConnected = false;
                while(bfsQueue.Count != 0)
                {
                    int currentNode = bfsQueue.Dequeue();
                    for (int i = 0; i < size; i++)
                    {
                        if (i == endNode && graph[currentNode][i] == 1)
                        {
                            isConnected = true;
                            break;
                        }
                        if (graph[currentNode][i] == 1 && !visitedNodes.Any(node => node == i))
                        {
                            visitedNodes.Add(i);
                            bfsQueue.Enqueue(i);
                        }
                    }
                    if (isConnected)
                    {
                        break;
                    }
                }
                return isConnected;
            }
        }

        static public class EULER_GRAPH_TYPE
        {
            static public int COMPLETE_EULER = 1;
            static public int HALF_EULER = 0;
            static public int NOT_EULER = -1;
        }
    }
}
