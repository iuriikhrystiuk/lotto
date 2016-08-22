namespace Lotto.Processor.Interfaces
{
    internal interface IDownloader
    {
        string Download(string url, string fileNamePattern);
    }
}
