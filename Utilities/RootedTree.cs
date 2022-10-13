namespace Utilities
{
    public class RootedTree<T>
    {
        public T Value { get; set; }
        public RootedTree<T>? Parent;
        private readonly List<RootedTree<T>> children = new();
        public IReadOnlyList<RootedTree<T>> Children => children.AsReadOnly();
        public int TotalChildCount { get; private set; }
        public int Height { get; private set; }
        public bool IsLeaf
        {
            get
            {
                if (children.Count == 0) return true;
                return false;
            }
        }
        public bool IsRoot
        {
            get
            {
                if (Parent == null) return true;
                return false;
            }
        }
        public RootedTree(T value)
        {
            this.Value = value;
            Height = 0;
        }
        public RootedTree(T value, RootedTree<T> parent)
        {
            this.Value = value;
            Parent = parent;
            Height = 0;
        }
        public void AddChild(T value)
        {
            AddChildNoUpdate(value);
            UpdateChildCount(1);
            UpdateHeight();
        }
        public void AddChild(T value1, T value2)
        {
            AddChildNoUpdate(value1);
            AddChildNoUpdate(value2);
            UpdateChildCount(2);
            UpdateHeight();
        }
        public void AddChild(T value1, T value2, T value3)
        {
            AddChildNoUpdate(value1);
            AddChildNoUpdate(value2);
            AddChildNoUpdate(value3);
            UpdateChildCount(3);
            UpdateHeight();
        }
        public void AddChild(T[] values)
        {
            foreach (var value in values)
            {
                AddChildNoUpdate(value);
            }
            UpdateChildCount(values.Length);
        }
        public void Remove()
        {
            if (IsRoot)
            {
                Console.WriteLine("Cannot remove root node.");
                return;
            }
            Parent!.children.Remove(this);
            Parent.UpdateChildCount(-(TotalChildCount + 1));
            Parent.UpdateHeight();
        }
        private void AddChildNoUpdate(T value)
        {
            children.Add(new RootedTree<T>(value, this));
        }
        private void UpdateChildCount(int i)
        {
            TotalChildCount += i;
            Parent?.UpdateChildCount(i);
        }
        private void UpdateHeight()
        {
            int n = children.Count;
            if (n == 0)
            {
                Height = 0;
                return;
            }
            var heights = new int[n];
            for (int i = 0; i < n; i++)
            {
                heights[i] = children[i].Height;
            }
            Height = Enumerable.Max(heights) + 1;
            Parent?.UpdateHeight();
        }
    }
}