// <copyright file="PauseTokenSource.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    public class PauseTokenSource
    {
        internal static readonly Task CompletedTask = Task.FromResult(true);
        private volatile TaskCompletionSource<bool> paused;

        public bool IsPaused
        {
            get { return this.paused != null; }
            set
            {
                if (value)
                {
                    Interlocked.CompareExchange(
                        ref this.paused, new TaskCompletionSource<bool>(), null);
                }
                else
                {
                    while (true)
                    {
                        TaskCompletionSource<bool> tcs = this.paused;
                        if (tcs == null) return;
                        if (Interlocked.CompareExchange(ref this.paused, null, tcs) == tcs)
                        {
                            tcs.SetResult(true);
                            return;
                        }
                    }
                }
            }
        }

        public IPauseToken Token { get { return new PauseToken(this); } }

        internal Task WaitWhilePausedAsync()
        {
            TaskCompletionSource<bool> cur = this.paused;
            return cur != null ? cur.Task : CompletedTask;
        }
    }
}
