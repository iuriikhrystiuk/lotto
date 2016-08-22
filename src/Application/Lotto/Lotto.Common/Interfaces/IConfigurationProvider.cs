namespace Lotto.Common.Interfaces
{
    public interface IConfigurationProvider
    {
        T Section<T>() where T : IConfigurationSection, new();
    }
}
