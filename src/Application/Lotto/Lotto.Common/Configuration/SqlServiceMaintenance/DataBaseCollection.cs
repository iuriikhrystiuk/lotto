// <copyright file="DataBaseCollection.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Configuration;

namespace Lotto.Common.Configuration.SqlServiceMaintenance
{
    public class DataBaseCollection : ConfigurationElementCollection
    {
        public DataBaseElement this[int index]
        {
            get { return (DataBaseElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DataBaseElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataBaseElement)element).DataBaseName;
        }
    }
}
