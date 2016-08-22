// <copyright file="SqlResourcesMaintainer.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Linq;
using System.Threading.Tasks;
using Lotto.Common.Configuration.SqlServiceMaintenance;
using Lotto.Common.Interfaces;
using Lotto.Processor.Interfaces.ResourceMaintenance;

namespace Lotto.Processor.Implementation.ResourceMaintenance
{
    public class SqlResourcesMaintainer : ISqlResourcesMaintainer
    {
        private readonly SqlServiceMaintenanceSection maintenanceSection;
        private readonly IWindowsServiceOperator serviceOperator;
        private readonly ISqlDatabaseWorker dataBaseWorker;

        public SqlResourcesMaintainer(
            IWindowsServiceOperator serviceOperator, 
            IConfigurationProvider configurationProvider,
            ISqlDatabaseWorker dataBaseWorker)
        {
            this.serviceOperator = serviceOperator;
            this.dataBaseWorker = dataBaseWorker;
            this.maintenanceSection = configurationProvider.Section<SqlServiceMaintenanceSection>();
        }

        public async Task MaintainResources()
        {
            await this.serviceOperator.Start(this.maintenanceSection.ServiceName);
            foreach (DataBaseElement dataBase in this.maintenanceSection.DataBases)
            {
                await
                    this.dataBaseWorker.Attach(this.maintenanceSection.ServerName, dataBase.DataBaseName,
                        Enumerable.Repeat(dataBase.DataBaseFile, 1));
            }
        }

        public async Task FreeResources()
        {
            foreach (DataBaseElement dataBase in this.maintenanceSection.DataBases)
            {
                await this.dataBaseWorker.Detach(this.maintenanceSection.ServerName, dataBase.DataBaseName);
            }
            await this.serviceOperator.Stop(this.maintenanceSection.ServiceName);
        }
    }
}
