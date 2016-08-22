// <copyright file="DataBaseElement.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Configuration;

namespace Lotto.Common.Configuration.SqlServiceMaintenance
{
    public class DataBaseElement : ConfigurationElement
    {
        [ConfigurationProperty("dataBaseName", IsRequired = true)]
        public string DataBaseName
        {
            get
            {
                return (string)this["dataBaseName"];
            }
            set
            {
                this["dataBaseName"] = value;
            }
        }

        [ConfigurationProperty("dataBaseFile", IsRequired = true)]
        public string DataBaseFile
        {
            get
            {
                return (string)this["dataBaseFile"];
            }
            set
            {
                this["dataBaseFile"] = value;
            }
        }
    }
}
