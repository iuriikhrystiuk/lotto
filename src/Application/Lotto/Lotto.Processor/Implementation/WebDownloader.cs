// <copyright file="WebDownloader.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Lotto.Common.Tools;
using Lotto.Processor.Interfaces;

namespace Lotto.Processor.Implementation
{
    internal class WebDownloader : IDownloader
    {
        public string Download(string url, string fileNamePattern)
        {
            WebClient client = new WebClient();
            var fileName = Guid.NewGuid().ToString();
            client.DownloadFile(url, fileName + ".zip");
            string directory = Directory.GetCurrentDirectory();
            List<string> files = ZipTools.Unzip(fileName + ".zip", directory);
            string file = files.FirstOrDefault(f => f.Contains(fileNamePattern));
            return file;
        }
    }
}
