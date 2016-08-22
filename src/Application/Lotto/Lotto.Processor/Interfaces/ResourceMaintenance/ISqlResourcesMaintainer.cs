using System.Threading.Tasks;

namespace Lotto.Processor.Interfaces.ResourceMaintenance
{
    public interface ISqlResourcesMaintainer
    {
        Task MaintainResources();

        Task FreeResources();
    }
}
