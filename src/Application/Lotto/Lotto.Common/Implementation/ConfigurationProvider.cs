// <copyright file="ConfigurationProvider.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Configuration;
using Lotto.Common.Interfaces;

namespace Lotto.Common.Implementation
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public T Section<T>() where T : IConfigurationSection, new()
        {
            T section = new T();
            return (T)ConfigurationManager.GetSection(section.SectionName);
        }
    }
}
