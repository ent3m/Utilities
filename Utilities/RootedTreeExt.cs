namespace Utilities
{
    public static partial class RoootedTrees
    {
        /// <summary>
        /// Return the sum of all leaves' values.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static int LeafSum(RootedTree<int> tree)
        {
            if (tree.IsLeaf) return tree.Value;
            int sum = 0;
            foreach (var child in tree.Children)
            {
                sum += LeafSum(child);
            }
            return sum;
        }
        /// <summary>
        /// Calculate the maximum depth of a rooted tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static int GetTreeHeight<T>(RootedTree<T> tree)
        {
            if (tree.IsLeaf) return 0;
            int n = tree.Children.Count;
            var heights = new int[n];
            for (int i = 0; i < n; i++)
            {
                heights[i] = GetTreeHeight(tree.Children[i]);
            }
            return Enumerable.Max(heights) + 1;
        }
        /// <summary>
        /// Return the number of children under a node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static int GetChildCount<T>(RootedTree<T> tree)
        {
            int total = 0;
            Count(tree);
            void Count(RootedTree<T> node)
            {
                total += node.Children.Count;
                foreach (var child in node.Children)
                {
                    Count(child);
                }
            }
            return total;
        }
    }
}