using System.Collections.Generic;

namespace Set
{
    public interface ISortedSet<T>
    {
        int Count { get; }
        void Clear();
        bool Add(T item);
        void AddRange(IEnumerable<T> items);
        bool Contains(T item);
        bool Remove(T item);
        T Max();
        T Min();
        T Ceiling(T item);
        T Floor(T item);
        SortedSet<T> Union(SortedSet<T> other);
        SortedSet<T> Intersection(SortedSet<T> other);
    }
}