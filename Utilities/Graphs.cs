namespace Utilities
{
    public static class Graphs
    {
        /// <summary>
        ///  Find all groups of connected nodes from adjacency lists. Returns the number of groups and out an array that describes which group each node belongs to.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="componentMap"></param>
        /// <returns></returns>
        public static int FindConnectedComponents(List<List<int>> list, out int[] componentMap)
        {
            int n = list.Count;
            var visited = new bool[n];
            int groupCount = 0;
            var tempMap = new int[n];

            for (int i = 0; i < n; i++)
            {
                if (!visited[i]) groupCount++;
                DFS(i);
            }
            componentMap = tempMap;
            return groupCount;

            void DFS(int at)
            {
                if (visited[at]) return;
                visited[at] = true;
                tempMap[at] = groupCount;
                var neighbors = list[at];
                for (int i = 0; i < neighbors.Count; i++)
                {
                    DFS(neighbors[i]);
                }
            }
        }
        /// <summary>
        /// Find the shortest path from the start to the end given a grid represented as a 2-dimensional char array. Return true if a path is found 
        /// and out a list representing the shortest path.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool NavigateMazeGrid(char[,] grid, out List<(int x, int y)> path)
        {
            var m = grid.GetLength(0);
            var n = grid.GetLength(1);
            var visited = new bool[m, n];
            var qX = new Queue<int>(m * n);
            var qY = new Queue<int>(m * n);
            var dx = new int[4] { -1, 1, 0, 0 };
            var dy = new int[4] { 0, 0, 1, -1 };
            (int x, int y) start = new();
            (int x, int y) end = new();
            bool foundPath = false;
            var prev = new (int? x, int? y)[m, n];

            MapOutGrid();
            BFS(start, end);
            path = ReconstructPath(prev, end);
            return foundPath;

            void MapOutGrid()
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        var node = grid[i, j];
                        if (node == 'S')
                        {
                            start = (i, j);
                        }
                        if (node == 'E')
                        {
                            end = (i, j);
                        }
                        if (node == '#')
                        {
                            visited[i, j] = true;
                        }
                    }
                }
            }

            void BFS((int x, int y) start, (int x, int y) end)
            {
                qX.Enqueue(start.x);
                qY.Enqueue(start.y);
                visited[start.x, start.y] = true;
                while (qX.Count > 0)
                {
                    int nodeX = qX.Dequeue();
                    int nodeY = qY.Dequeue();
                    for (int i = 0; i < 4; i++)
                    {
                        var x = nodeX + dx[i];
                        var y = nodeY + dy[i];
                        if (x < 0 || y < 0) continue;
                        if (x >= m || y >= n) continue;
                        if (visited[x, y]) continue;
                        qX.Enqueue(x);
                        qY.Enqueue(y);
                        visited[x, y] = true;
                        prev[x, y] = (nodeX, nodeY);
                        if ((x, y) == end)
                        {
                            foundPath = true;
                            return;
                        }
                    }
                }
            }

            List<(int, int)> ReconstructPath((int? x, int? y)[,] p, (int x, int y) end)
            {
                var path = new List<(int, int)>();
                if (!foundPath) return path;
                for ((int? x, int? y) node = end; node.x != null; node = prev[(int)node.x, (int)node.y])
                {
                    path.Add(((int)node.x, (int)node.y));
                }
                path.Reverse();
                return path;
            }
        }
    }
}