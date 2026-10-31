using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    internal class CountReachedEventArgs(int count) : EventArgs
    {
        internal int Count { get; } = count;
    }
    internal class Counter(int _target = 5)
    {
        internal event EventHandler<CountReachedEventArgs>? CountReached;
        private int count;
        private readonly int target = _target;
        internal void Increment()
        {
            count++;
            if (count == target) OnCountReached(new(count));
        }
        private void OnCountReached(CountReachedEventArgs e)
        {
            CountReached?.Invoke(this, e);
        }
    }
}
