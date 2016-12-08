using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading;

namespace Shu.Utility.Queue
{
    /// <summary>Represents a first-in, first-out collection of objects.</summary>
    /// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
    [DebuggerDisplay("Count = {Count}"), DebuggerTypeProxy(typeof(Common_LinkedQueueDebugView<>)), ComVisible(false)]
    [Serializable]
    public class LinkedQueue<T> : IEnumerable<T>, ICollection, IEnumerable
    {
        /// <summary>Enumerates the elements of a <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
        [Serializable]
        public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private LinkedQueue<T> _q;
            private byte _status;
            private int _version;
            private LinkedQueueNode<T> _currentElement;
            /// <summary>Gets the element at the current position of the enumerator.</summary>
            /// <returns>The element in the <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> at the current position of the enumerator.</returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection, after the last element or it has been disposed. </exception>
            public T Current
            {
                get
                {
                    if (this._status == 3)
                    {
                        throw new InvalidOperationException("It has been disposed");
                    }

                    if (this._status == 0)
                    {
                        throw new InvalidOperationException("The enumerator has not been started");
                    }

                    if (this._status == 2)
                    {
                        throw new InvalidOperationException("The enumerator has been ended");
                    }

                    return this._currentElement.Value;
                }
            }
            /// <summary>Gets the element at the current position of the enumerator.</summary>
            /// <returns>The element in the collection at the current position of the enumerator.</returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
            object IEnumerator.Current
            {
                get
                {
                    if (this._status == 3)
                    {
                        throw new InvalidOperationException("It has been disposed");
                    }

                    if (this._status == 0)
                    {
                        throw new InvalidOperationException("The enumerator has not been started");
                    }

                    if (this._status == 2)
                    {
                        throw new InvalidOperationException("The enumerator has been ended");
                    }

                    return this._currentElement.Value;
                }
            }
            internal Enumerator(LinkedQueue<T> q)
            {
                this._q = q;
                this._version = q._version;
                this._status = 0;
                this._currentElement = null;
            }
            /// <summary>Releases all resources used by the <see cref="T:Common.Collections.Generic.LinkedQueue`1.Enumerator" />.</summary>
            public void Dispose()
            {
                this._status = 3;
                this._q = null;
                this._currentElement = null;
            }
            /// <summary>Advances the enumerator to the next element of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified or disposed after the enumerator was created. </exception>
            public bool MoveNext()
            {
                if (this._status == 3)
                {
                    throw new InvalidOperationException("It has been disposed");
                }

                if (this._version != this._q._version)
                {
                    throw new InvalidOperationException("The queue has been changed");
                }

                if (this._status == 2)
                {
                    return false;
                }

                if (this._q.Count == 0)
                {
                    this._status = 2;
                    return false;
                }

                if (this._status == 0)
                {
                    this._currentElement = _q._head;
                    this._status = 1;
                    return true;
                }

                if (this._currentElement._next == null)
                {
                    this._status = 2;
                    this._currentElement = null;
                    return false;
                }

                this._currentElement = _currentElement._next;

                return true;
            }
            /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified or disposed after the enumerator was created. </exception>
            void IEnumerator.Reset()
            {
                if (this._status == 3)
                {
                    throw new InvalidOperationException("It has been disposed");
                }

                if (this._version != this._q._version)
                {
                    throw new InvalidOperationException("The queue has been changed");
                }

                this._status = 0;
                this._currentElement = null;
            }
        }

        private LinkedQueueNode<T> _head;
        private LinkedQueueNode<T> _tail;
        private int _size;
        private int _version;
        [NonSerialized]
        private object _syncRoot;

        /// <summary>Gets the number of elements contained in the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
        /// <returns>The number of elements contained in the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</returns>
        public int Count
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> class that is empty.
        /// </summary>
        public LinkedQueue()
        {
            _head = null;
            _tail = null;
        }

        /// <summary>Initializes a new instance of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="collection" /> is null.</exception>
        public LinkedQueue(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (T t in collection)
            {
                Enqueue(t);
            }
        }

        /// <summary>Adds an object to the end of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
        /// <param name="item">The object to add to the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />. The value can be null for reference types.</param>
        public void Enqueue(T item)
        {
            LinkedQueueNode<T> newNode = new LinkedQueueNode<T>(item);

            if (_tail == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail._next = newNode;
                _tail = newNode;
            }

            _size++;
            _version++;
        }

        /// <summary>Removes and returns the object at the beginning of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
        /// <returns>The object that is removed from the beginning of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> is empty.</exception>
        public LinkedQueueNode<T> Dequeue()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            LinkedQueueNode<T> node = _head;
            _head = _head._next;

            if (_head == null)
            {
                _tail = null;
            }

            _size--;
            _version++;

            return node;
        }

        /// <summary>Returns the object at the beginning of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> without removing it.</summary>
        /// <returns>The object at the beginning of the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> is empty.</exception>
        public LinkedQueueNode<T> Peek()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _head;
        }

        /// <summary>Determines whether an element is in the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />; otherwise, false.</returns>
        /// <param name="item">The object to locate in the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />. The value can be null for reference types.</param>
        public bool Contains(T item)
        {
            bool find = false;
            LinkedQueueNode<T> temp = _head;

            while (temp != null && !find)
            {
                find = temp.Value.Equals(item);
                temp = temp._next;
            }

            return find;
        }

        /// <summary>
        /// Removes all objects from the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.
        /// </summary>
        public void Clear()
        {
            if (_head == null)
            {
                return;
            }

            LinkedQueueNode<T> temp = _head;

            do
            {
                temp.Invalid();
                temp = temp._next;
            }
            while (temp != null);

            _size = 0;
            _version++;
        }

        /// <summary>Copies the <see cref="T:Common.Collections.Generic.LinkedQueue`1" /> elements to a new array.</summary>
        /// <returns>A new array containing elements copied from the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</returns>
        public T[] ToArray()
        {
            T[] array = new T[_size];
            LinkedQueueNode<T> temp = _head;
            int i = 0;

            while (temp != null)
            {
                array[i++] = temp.Value;
                temp = temp._next;
            }

            return array;
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</summary>
        /// <returns>An <see cref="T:Common.Collections.Generic.LinkedQueue`1.Enumerator" /> for the <see cref="T:Common.Collections.Generic.LinkedQueue`1" />.</returns>
        public LinkedQueue<T>.Enumerator GetEnumerator()
        {
            return new LinkedQueue<T>.Enumerator(this);
        }

        #region ICollection implementations.
        /// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns false.</returns>
        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }
        /// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
        /// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns the current instance.</returns>
        object ICollection.SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }
        /// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> is less than zero.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("Multi-Dimension is not supported");
            }
            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("Non zero lower bound is not supported");
            }
            int length = array.Length;
            if (index < 0 || index > length)
            {
                throw new ArgumentOutOfRangeException("index", index, "The index is less than zero or greater than the length of the array");
            }
            if (length - index < this._size)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from index to the end of the destination array");
            }

            T[] temp = ToArray();
            try
            {
                Array.Copy(temp, 0, array, index, Count);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("The type of the source collection cannot be cast automatically to the type of the destination array");
            }
        }
        #endregion

        #region IEnumerable<T> implementation
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new LinkedQueue<T>.Enumerator(this);
        }
        #endregion

        #region IEnumerable implementation
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedQueue<T>.Enumerator(this);
        }
        #endregion
    }
}