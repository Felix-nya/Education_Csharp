using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class Node
    {
        internal NodeList Children = new NodeList();
        internal string Text = string.Empty;

        internal Node() { }
        internal Node(string text) { Text = text; }

        public override string ToString()
        {
            var sb = new StringBuilder(Text);
            Print(sb, "");
            return sb.ToString();
        }

        private void Print(StringBuilder sb, string prefix)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                bool last = i == Children.Count - 1;
                Node child = Children[i];
                sb.Append('\n').Append(prefix).Append(last ? "└─" : "├─").Append(child.Text);
                child.Print(sb, prefix + (last ? "  " : "│ "));
            }
        }
    }
    internal class NodeList
    {
        private Node[] _items = new Node[0];

        internal int Count => _items.Length;
        internal Node this[int index] => _items[index];

        internal void Add(params Node[] nodes)
        {
            int oldLength = _items.Length;
            Array.Resize(ref _items, oldLength + nodes.Length);
            nodes.CopyTo(_items, oldLength);
        }

        internal void Add(in Node node)
        {
            Array.Resize(ref _items, _items.Length + 1);
            _items[_items.Length - 1] = node;
        }

        internal void AddDefault() => Add(new Node());

        internal Node Get(int index) => _items[index];

        internal bool TryGet(int index, out Node node)
        {
            if (index < 0 || index >= _items.Length) { node = null; return false; }
            node = _items[index];
            return true;
        }

        internal bool TryGetRef(int index, ref Node node)
        {
            if (index < 0 || index >= _items.Length) return false;
            node = _items[index];
            return true;
        }
    }
}
