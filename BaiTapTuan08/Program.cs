using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapTuan10
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> graph = new List<List<int>>();
            int size;
            int start;
            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                string[] firstLine = sr.ReadLine().Split();
                size = Int32.Parse(firstLine[0]);
                start = Int32.Parse(firstLine[1]);
                Int32.Parse("-4");
                for (int i = 0; i < size; i++)
                {
                    List<int> row = new List<int>();
                    string line = sr.ReadLine();
                    string[] values = line.Split(' ');
                    Array.ForEach(values, v =>
                    {
                        row.Add(Int32.Parse(v));
                    });
                    graph.Add(row);
                }
            }

            int[,] costs = new int[size + 1, size];
            int[,] from = new int[size, size];
            for (int i = 0; i < size + 1; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    costs[i, j] = Int32.MaxValue;
                }
            }
            costs[0, start] = 0;
            Queue<List<int>> queue = new Queue<List<int>>();
            queue.Enqueue(new List<int>() { start });
            for (int i = 0; i < size; i++)
            {
                List<int> nodes = queue.Dequeue();
                for (int j = 0; j < size; j++)
                {
                    costs[i + 1, j] = costs[i, j];
                }
                nodes.ForEach(node =>
                {
                    var paths = graph[node].Select((value, index) => new { value, index })
                             .Where(z => z.value != 0);
                    bool[] changed = new bool[size];
                    //List<int> changed = new List<int>();
                    foreach(var path in paths)
                    {
                        int value = path.value;
                        int index = path.index;
                        if (costs[i, index] == Int32.MaxValue || value + costs[i, node] < costs[i, index])
                        {
                            costs[i + 1, index] = value + costs[i, node];
                            from[i, index] = node;
                            changed[index] = true;
                        }
                        //else if ()
                        //{
                        //    costs[i + 1, index] = value + costs[i, node];
                        //    from[i, index] = node;
                        //    changed[index] = true;
                        //}
                    }
                    List<int> changedList = changed.Select((value, index) => new { value, index })
                             .Where(z => z.value == true)
                             .Select(z => z.index).ToList<int>();
                    queue.Enqueue(changedList);
                });
                for (int j = 0; j < size; j++)
                {
                    if (i < size - 1 && i > 0)
                    {
                        from[i + 1, j] = from[i, j];
                    }
                }
            }
            bool hasNegativeCycle = true;
            string output = "";
            for (int i = 0; i < size; i++)
            {
                if (costs[size, i] == Int32.MaxValue)
                {
                    output += $"Duong di ngan nhat tu {start} den {i}: Khong co duong di\n";
                }
                else if (i != start)
                {
                    hasNegativeCycle = false;
                    List<int> shortestPath = new List<int>() { i };
                    int prevNode = from[size - 1, i];
                    shortestPath.Add(prevNode);
                    while(prevNode != start)
                    {
                        prevNode = from[size - 1, prevNode];
                        shortestPath.Add(prevNode);
                    }
                    output += $"Duong di ngan nhat tu {start} den {i}: {String.Join(" <- ", shortestPath.ToArray())}\n";
                    output += $"Chi phi duong di ngan nhat: {costs[size, i]}\n";
                }
            }
            if (hasNegativeCycle)
            {
                Console.WriteLine("Do thi co mach am");
            }
            else
            {
                Console.WriteLine(output);
            }
        }
    }
}
