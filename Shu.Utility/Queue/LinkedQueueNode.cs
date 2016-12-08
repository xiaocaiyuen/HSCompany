using System.Runtime;
using System.Runtime.InteropServices;

namespace Shu.Utility.Queue
{
    /// <summary>
    /// Represents a node in a <see cref="T:Common.Collections.Generic.LinkedQueue`1" />. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
    [ComVisible(false)]
    public sealed class LinkedQueueNode<T>
    {
        private T _item;
        internal LinkedQueueNode<T> _next;

        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public T Value 
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get 
            { 
                return _item; 
            } 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Common.Collections.Generic.LinkedQueueNode`1" /> class, containing the specified value.
        /// </summary>
        /// <param name="item">The specified value.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        internal LinkedQueueNode(T item)
        {
            _item = item;
        }

        internal void Invalid()
        {
            _item = default(T);
            _next = null;
        }
    }
}