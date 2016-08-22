using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Common.Interfaces
{
    public interface IWindowsServiceOperator
    {
        Task Start(string serviceName);

        Task Stop(string serviceName);
    }
}
