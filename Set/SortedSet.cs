using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Set
{
    public class SortedSet<T> : IEnumerable<T>
    {
        LeftLeaningRedBlackTree<T> tree;
        IComparer<T> comparer;

        public int Count => tree.Count;

        public SortedSet(IComparer<T> comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            tree = new LeftLeaningRedBlackTree<T>(comparer);
        }

        public SortedSet() : this(null)
        {
        }

        public bool Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Called Add with null value.");
            }

            bool success = true;
            try
            {
                tree.Add(item);
            }
            catch (ArgumentException)
            {
                success = false;
            }

            return success;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Called Contains with null value.");
            }

            return tree.Contains(item);
        }

        public bool Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Called Remove with null value.");
            }

            return tree.Remove(item);
        }

        public T Max()
        {
            if (Count == 0)
            {
                throw new Exception("Set is empty.");
            }
            return tree.GetMaximum(tree.Root).Value;
        }

        public T Min()
        {
            if (Count == 0)
            {
                throw new Exception("Set is empty.");
            }
            return tree.GetMinimum(tree.Root).Value;
        }

        public T Ceiling(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Called Ceiling with null value.");
            }

            var ceil = Ceiling(tree.Root, item);
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
                throw new ArgumentNullException("Called Floor with null value.");
            }

            var floor = Floor(tree.Root, item);
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
            if (comparer.Compare(item, node.Value) == 0)
            {
                return node;
            }


            // if nodes key is larger, it must be in the left subtree
            if (comparer.Compare(node.Value, item) > 0)
            {
                return Floor(node.Left, item);
            }

            // otherwise, either right subtree or root has the floor value
            Node<T> floor = Floor(node.Right, item);
            if (floor == null)
            {
                return node;
            }
            else
                return (comparer.Compare(floor.Value, item) <= 0) ? floor : node;
        }

        private Node<T> Ceiling(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            // found equal key
            if (comparer.Compare(node.Value, item) == 0)
            {
                return node;
            }

            // if node's key is smaller, it must be in the right subtree
            if (comparer.Compare(node.Value, item) < 0)
            {
                return Ceiling(node.Right, item);
            }

            // otherwise, either left subtree or root has the ceil value
            Node<T> ceil = Ceiling(node.Left, item);
            if (ceil == null)
            {
                return node;
            }
            else
                return (comparer.Compare(ceil.Value, item) >= 0) ? ceil : node;

        }

        public SortedSet<T> Union(SortedSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("Other set cannot be null.");
            }

            SortedSet<T> newset = new SortedSet<T>(comparer);
            newset.AddRange(this);
            newset.AddRange(other);

            return newset;
        }

        public SortedSet<T> Intersection(SortedSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("Other set cannot be null.");
            }
            SortedSet<T> newset = new SortedSet<T>(comparer);

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
                    if (this.Contains(item))
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
                throw new ArgumentNullException("Enumerable cannot be null.");
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

            var node = tree.Root;

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
            if (other == null || this == null)
            {
                return false;
            }
            else if (other == this)
            {
                return true;
            }

            var that = other as SortedSet<T>;
            return Enumerable.SequenceEqual(this, that);
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
