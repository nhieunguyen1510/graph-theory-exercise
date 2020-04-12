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
            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                int size = Int32.Parse(sr.ReadLine());
                List<List<int>> graph = new List<List<int>>();
                for (int i = 0; i < size; i++)
                {
                    List<int> row = new List<int>();
                    string line = sr.ReadLine();
                    string[] values = line.Split(' ');
                    Array.ForEach(values, v => row.Add(Int32.Parse(v)));
                    graph.Add(row);
                }
            }

        }


        static int checkEulerGraph(List<List<int>> graph)
        {
            int size = graph.Count;
            List<List<int>> graphAfterVisit = new List<List<int>>();
            int startNode = -1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (startNode == -1 && graph[i][j] == 1)
                    {
                        startNode = i;
                    }
                    graphAfterVisit[i][j] = graph[i][j];
                }
            }
            return Program.EULER_GRAPH_TYPE.COMPLETE_EULER;
        }

        static bool canGoNext(List<List<int>> graph, int currentNode)
        {

            return false;
        }

        static public class EULER_GRAPH_TYPE
        {
            static public int COMPLETE_EULER = 1;
            static public int HALF_EULER = 0;
            static public int NOT_EULER = -1;
        }
    }
}
