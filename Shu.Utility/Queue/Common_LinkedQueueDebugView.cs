using System;
using System.Diagnostics;

namespace Shu.Utility.Queue
{
    internal sealed class Common_LinkedQueueDebugView<T>
    {
        private LinkedQueue<T> queue;
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.queue.ToArray();
			}
		}
        public Common_LinkedQueueDebugView(LinkedQueue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this.queue = queue;
		}
    }
}
