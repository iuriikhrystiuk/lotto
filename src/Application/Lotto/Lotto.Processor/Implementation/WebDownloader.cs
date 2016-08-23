// <copyright file="WebDownloader.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Net;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    internal class WebDownloader : IDownloader
    {
        public string Download(string url)
        {
            WebClient client = new WebClient();
            var fileName = Guid.NewGuid() + ".zip";
            client.DownloadFile(url, fileName);
            return fileName;
        }
    }
}
