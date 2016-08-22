// <copyright file="PauseToken.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Threading.Tasks;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    public class PauseToken : IPauseToken
    {
        private readonly PauseTokenSource source;

        internal PauseToken(PauseTokenSource source) { this.source = source; }

        public bool IsPaused { get { return this.source != null && this.source.IsPaused; } }

        public Task WaitWhilePausedAsync()
        {
            return this.IsPaused ?
                this.source.WaitWhilePausedAsync() :
                PauseTokenSource.CompletedTask;
        } 
    }
}
