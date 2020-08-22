using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Set
{
    public class SortedSet<T> : IEnumerable<T>, ISortedSet<T>
    {
        private LeftLeaningRedBlackTree<T> _tree;
        public IComparer<T> Comparer { get; }
        public int Count => _tree.Count;

        public SortedSet(IComparer<T> comparer)
        {
            Comparer = comparer ?? Comparer<T>.Default;
            _tree = new LeftLeaningRedBlackTree<T>(Comparer);
        }

        public SortedSet() : this(null)
        {
        }

        public void Clear()
        {
            _tree = new LeftLeaningRedBlackTree<T>(Comparer);
        }

        public bool Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (Contains(item))
            {
                return false;
            }

            _tree.Add(item);
            return true;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return _tree.Contains(item);
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return _tree.Remove(item);
        }

        public T Max()
        {
            if (Count == 0)
            {
                throw new Exception("SortedSet is empty.");
            }

            return _tree.GetMaximum(_tree.Root).Value;
        }

        public T Min()
        {
            if (Count == 0)
            {
                throw new Exception("SortedSet is empty.");
            }

            return _tree.GetMinimum(_tree.Root).Value;
        }

        public T Ceiling(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var ceil = Ceiling(_tree.Root, item);
            if (ceil == null)
            {
                throw new Exception($"All keys are less than {item}");
            }

            return ceil.Value;
        }

        public T Floor(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var floor = Floor(_tree.Root, item);
            if (floor == null)
            {
                throw new Exception($"All keys are greater than {item}");
            }

            return floor.Value;
        }

        private Node<T> Floor(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            // equal key
            if (Comparer.Compare(item, node.Value) == 0)
            {
                return node;
            }


            // if nodes key is larger, it must be in the left subtree
            if (Comparer.Compare(node.Value, item) > 0)
            {
                return Floor(node.Left, item);
            }

            // otherwise, either right subtree or root has the floor value
            Node<T> floor = Floor(node.Right, item);
            if (floor == null)
            {
                return node;
            }

            return (Comparer.Compare(floor.Value, item) <= 0) ? floor : node;
        }

        private Node<T> Ceiling(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            // found equal key
            if (Comparer.Compare(node.Value, item) == 0)
            {
                return node;
            }

            // if node's key is smaller, it must be in the right subtree
            if (Comparer.Compare(node.Value, item) < 0)
            {
                return Ceiling(node.Right, item);
            }

            // otherwise, either left subtree or root has the ceil value
            Node<T> ceil = Ceiling(node.Left, item);
            if (ceil == null)
            {
                return node;
            }

            return (Comparer.Compare(ceil.Value, item) >= 0) ? ceil : node;
        }

        public SortedSet<T> Union(SortedSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            SortedSet<T> newset = new SortedSet<T>(Comparer);
            newset.AddRange(this);
            newset.AddRange(other);

            return newset;
        }

        public SortedSet<T> Intersection(SortedSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            SortedSet<T> newset = new SortedSet<T>(Comparer);

            // pick and loop over the smaller of two sets
            if (Count < other.Count)
            {
                foreach (var item in this)
                {
                    if (other.Contains(item))
                    {
                        newset.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in other)
                {
                    if (Contains(item))
                    {
                        newset.Add(item);
                    }
                }
            }

            return newset;
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (var item in items)
            {
                Add(item);
            }
        }

        // iterative inorder traversal
        public IEnumerator<T> GetEnumerator()
        {
            var stack = new Stack<Node<T>>();

            var node = _tree.Root;

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                else
                {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (other == this)
            {
                return true;
            }

            var that = other as SortedSet<T>;
            return this.SequenceEqual(that!);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            foreach (var item in this)
            {
                hashCode.Add(item);
            }

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            return string.Join(',', this);
        }
    }
}