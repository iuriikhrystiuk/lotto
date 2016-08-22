// <copyright file="WindowsServiceOperator.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using IK.Logging.Interfaces;
using Lotto.Common.Interfaces;

namespace Lotto.Common.Implementation
{
    public class WindowsServiceOperator : IWindowsServiceOperator
    {
        private readonly ILogger logger;

        public WindowsServiceOperator(ILogger logger)
        {
            this.logger = logger;
        }

        public Task Start(string serviceName)
        {
            var service = new ServiceController(serviceName);
            if (service.Status != ServiceControllerStatus.Running)
            {
                service.Start();
                this.logger.Info("Starting service '{0}'", serviceName);
                return Task.Factory.StartNew(() => service.WaitForStatus(ServiceControllerStatus.Running));
            }

            return Task.FromResult(0);
        }

        public Task Stop(string serviceName)
        {
            var service = new ServiceController(serviceName);
            if (service.Status != ServiceControllerStatus.Stopped)
            {
                service.Stop();
                this.logger.Info("Stopping service '{0}'", serviceName);
                return Task.Factory.StartNew(() => service.WaitForStatus(ServiceControllerStatus.Stopped));
            }

            return Task.FromResult(0);
        }
    }
}
