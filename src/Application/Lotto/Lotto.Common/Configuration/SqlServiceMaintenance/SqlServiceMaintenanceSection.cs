using System.Configuration;
using Lotto.Common.Interfaces;

namespace Lotto.Common.Configuration.SqlServiceMaintenance
{
    public class SqlServiceMaintenanceSection : ConfigurationSection, IConfigurationSection
    {
        [ConfigurationProperty("serviceName", IsRequired = true)]
        public string ServiceName
        {
            get
            {
                return (string)this["serviceName"];
            }
            set
            {
                this["serviceName"] = value;
            }
        }

        [ConfigurationProperty("serverName", IsRequired = true)]
        public string ServerName
        {
            get
            {
                return (string)this["serverName"];
            }
            set
            {
                this["serverName"] = value;
            }
        }

        [ConfigurationProperty("dataBases", IsRequired = true)]
        public DataBaseCollection DataBases
        {
            get
            {
                return (DataBaseCollection)this["dataBases"];
            }
            set
            {
                this["dataBases"] = value;
            }
        }

        public string SectionName => "sqlServiceMaintenance";
    }
}
