using System;
using System.Diagnostics;

namespace Set
{
    /// <summary>
    /// Represents a node of the tree.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Node<T>
    {
        /// <summary>
        /// Gets or sets the node's value
        /// </summary>
        public T Value;

        /// <summary>
        /// Gets or sets the left node.
        /// </summary>
        public Node<T> Left;

        /// <summary>
        /// Gets or sets the right node.
        /// </summary>
        public Node<T> Right;

        /// <summary>
        /// Gets or sets the color of the node.
        /// </summary>
        public bool IsBlack;

        /// <summary>
        /// Initializes a new instance of the Node class.
        /// </summary>
        /// <remarks>
        /// IsBlack is defaulted to false as new nodes added to a red black tree must be red.
        /// </remarks>
        /// <param name="value">The value of the node.</param>
        public Node(T value)
        {
            this.Value = value;
            IsBlack = false;
            Left = null;
            Right = null;
        }

        private string GetDebuggerDisplay()
        {
            return $"Val: {Value}, Left: {Left?.Value.ToString() ?? "Null"}, Right: {Right?.Value.ToString() ?? "Null"}";
        }
    }

}