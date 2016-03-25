using UnityEngine;
using System.Collections.Generic;

namespace CyberCommon
{
    /// <summary>
    /// Creates queue that has a limit on size.
    /// It will enqueue until the limit is reached
    /// then it dequeues the first item.
    /// Useful for graphing a history of plots.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedQueue<T> : Queue<T>
    {

        // limit of the queue
        private int _limit = -1;
        public int limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        // Constructor sets the limit for this queue and initializes the queue to be the limit
        public LimitedQueue(int limit) : base(limit)
        {
            this.limit = limit;
        }

        // hide the base enqueue method with new
        public new void Enqueue(T element)
        {
            if (this.Count >= limit)
            {
                this.Dequeue();
            }
            base.Enqueue(element);
        }
    }
}
