using System.Threading.Tasks;

namespace Lotto.Processor.Interfaces
{
    public interface IPauseToken
    {
        bool IsPaused { get; }

        Task WaitWhilePausedAsync();
    }
}
