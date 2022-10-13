namespace Utilities
{
    public static class Search
    {
        public static int BinarySearch(int[] array, int element)
        {
            int start = 0, end = array.Length - 1, mid = -1;
            bool found = false;
            while (found != true)
            {
                mid = start + (end - start) / 2;
                if (array[mid] == element)
                {
                    found = true;
                }
                else if (array[mid] > element)
                {
                    end = mid - 1;
                }
                else if (start >= end)
                {
                    return -1;
                }
                else
                {
                    start = mid + 1;
                }
            }
            return mid;
        }

        /// <summary>
        /// Find a path from Start to End given an adjacency matrix. Returns true if a path is found and out a list that describes the path.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FindPath(List<List<int>> list, int startNode, int endNode, out List<int> path)
        {
            int n = list.Count;
            var visited = new bool[n];
            bool foundPath = false;
            List<int> tempPath = new List<int>();

            DFS(startNode, new List<int>());
            path = tempPath;
            return foundPath;

            void DFS(int at, List<int> prev)
            {
                if (visited[at] || foundPath == true) return;
                if (at == endNode)
                {
                    foundPath = true;
                    prev.Add(at);
                    tempPath = prev;
                    return;
                }
                visited[at] = true;
                prev.Add(at);
                var neighbors = list[at];
                for (int i = 0; i < neighbors.Count; i++)
                {
                    DFS(neighbors[i], new List<int>(prev));
                }
            }
        }

        /// <summary>
        ///  Find the shortest path from Start to End given an adjacency matrix. Returns true if a path is found and out a list that describes the path.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FindShortestPath(List<List<int>> list, int startNode, int endNode, out List<int> path)
        {
            //  Find the total number of nodes
            var n = list.Count;
            //  Array to record which nodes have been visited before. Part of BFS
            var visited = new bool[n];
            //  Queue to hold to-be-explored nodes. Part of BFS
            var q = new Queue<int>(n);
            //  'Found' status for determining when to terminate early
            bool foundPath = false;
            //  'Previous' array to hold the previously visited node of each node
            var prev = new int?[n];

            //  Initiate BFS on the starting node
            BFS(startNode, endNode);
            path = ReconstructPath(prev, endNode);
            return foundPath;

            void BFS(int start, int end)
            {
                //  Add the starting node to the queue
                q.Enqueue(start);
                //  Add it to the visited list
                visited[start] = true;
                while (q.Count > 0)
                {
                    //  Remove and examine the oldest item in the queue
                    int node = q.Dequeue();
                    //  Find its neighbors and add them to the queue
                    var neighbors = list[node];
                    foreach (int neighbor in neighbors)
                    {
                        //  Ignore if the neighbor has already been visited
                        if (visited[neighbor]) continue;
                        //  Otherwise add it to the queue, the visited list, and the previous list
                        q.Enqueue(neighbor);
                        visited[neighbor] = true;
                        prev[neighbor] = node;
                        //  If the neighbor is the end node, terminate early
                        if (neighbor == end)
                        {
                            foundPath = true;
                            return;
                        }
                    }
                }
            }
            //  Using a previous array is a memory-efficient way to reconstruct a path. However, time complexity is high if the path is long O(n)
            List<int> ReconstructPath(int?[] p, int end)
            {
                var path = new List<int>();
                if (!foundPath) return path;
                //  Backtrack the path from the end node to the start node
                for (int? node = end; node != null; node = prev[(int)node])
                {
                    path.Add((int)node);
                }
                //  Reverse it to get the path from start to end
                path.Reverse();
                return path;
            }
        }
    }
}