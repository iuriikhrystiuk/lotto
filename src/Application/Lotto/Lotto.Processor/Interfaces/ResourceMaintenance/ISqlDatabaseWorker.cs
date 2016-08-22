using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Processor.Interfaces.ResourceMaintenance
{
    public interface ISqlDatabaseWorker
    {
        Task Attach(string serverName, string dataBaseName, IEnumerable<string> files);

        Task Detach(string serverName, string dataBaseName);
    }
}
