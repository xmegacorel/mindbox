using System;

namespace TripCards
{
    /// <summary>
    /// гибридное суффиксное деревео только для русских символов
    /// без учета регистра поиск
    /// </summary>
    public class Tries<T>
    {
        private NodeFirstLevelTop<T> _root;
        private char _startSymbol = 'а'; // русская буква
        private static readonly int _lengthAlfabet = 32; // для русского языка

        public Tries()
        {
            
        }

        public Tries(Item[] dictionary)
        {
            foreach (var s in dictionary)
            {
                if (s.Word.Length > 2)
                    Put(s.Word.ToLower(), s.Index);
            }
        }

        public void Put(Item item)
        {
            if (item.Word.Length > 2)
                Put(item.Word.ToLower(), item.Index);
        }

        private void Put(string s, T value)
        {
            _root = PutInnerFirst(_root, s, 0, value);
        }

        private NodeFirstLevelTop<T> PutInnerFirst(NodeFirstLevelTop<T> node, string s, int d, T value)
        {
            if (node == null)
                node = new NodeFirstLevelTop<T>();

            int index = (s[d] - _startSymbol);
            NodeFirstLevelBottom<T> node1 = node.Next[index];
            if (node1 == null)
                node1 = new NodeFirstLevelBottom<T>();
            d++;
            int index1 = (s[d] - _startSymbol);
            node1.Next[index1] = PutInnerOther(node1.Next[index1], s, d + 1, value);

            node.Next[index] = node1;

            return node;
        }

        private Node<T> PutInnerOther(Node<T> node, string s, int d, T value)
        {
            char c = s[d];
            if (node == null)
                node = new Node<T>() { C = c, Value = value };
            if (c < node.C)
                node.Left = PutInnerOther(node.Left, s, d, value);
            else if (c > node.C)
                node.Right = PutInnerOther(node.Right, s, d, value);
            else if (d < s.Length - 1)
                node.Middle = PutInnerOther(node.Middle, s, d + 1, value);
            else
                node.HasValue = true;

            return node;
        }

        public SearchResult Get(String key1)
        {
            var key = key1.ToLower();
            int d = 0;
            int index = (key[d] - _startSymbol);
            if (_root == null)
                return new SearchResult() {Result = SearchResultTstEnum.NotFound};
            NodeFirstLevelBottom<T> node = _root.Next[index];
            if (node == null)
                return new SearchResult() { Result = SearchResultTstEnum.NotFound };

            if (d == 0)
            {
                d++;
                index = (key[d] - _startSymbol);
                Node<T> nd = node.Next[index];
                if (nd == null)
                    return new SearchResult() { Result = SearchResultTstEnum.NotFound };
                nd = GetInnerOther(nd, key, d + 1);
                if (nd == null)
                    return new SearchResult() { Result = SearchResultTstEnum.NotFound };
                if (nd.HasValue)
                    return new SearchResult() { Result = SearchResultTstEnum.Found, Value = nd.Value};
                return new SearchResult() { Result = SearchResultTstEnum.Contain };
            }
            return new SearchResult() { Result = SearchResultTstEnum.NotFound };
        }

        private Node<T> GetInnerOther(Node<T> node, String key, int d)
        {
            if (node == null)
                return null;
            char c = key[d];
            if (c < node.C)
                return GetInnerOther(node.Left, key, d);
            if (c > node.C)
                return GetInnerOther(node.Right, key, d);
            if (d < key.Length - 1)
                return GetInnerOther(node.Middle, key, d + 1);

            return node;
        }

        private class NodeFirstLevelTop<T>
        {
            public NodeFirstLevelBottom<T>[] Next { get; set; }

            public NodeFirstLevelTop()
            {
                Next = new NodeFirstLevelBottom<T>[26];
            }
        }

        public class NodeFirstLevelBottom<T>
        {
            public Node<T>[] Next { get; set; }
        
            public NodeFirstLevelBottom()
            {
                Next = new Node<T>[_lengthAlfabet];
            }

        }

        public class Node<T>
        {
            public char C { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Middle { get; set; }
            public Node<T> Right { get; set; }

            public bool HasValue { get; set; }

            public T Value { get; set; }
            
        }

        public class SearchResult
        {
            public SearchResultTstEnum Result { get; set; }
            public T Value { get; set; }
        }

        public class Item
        {
            public string Word { get; set; }
            public T Index { get; set; }
        }
    }
}