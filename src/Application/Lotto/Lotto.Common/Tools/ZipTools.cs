// <copyright file="ZipTools.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using Ionic.Zip;

namespace Lotto.Common.Tools
{
    public static class ZipTools
    {
        public static List<string> Unzip(string fileName, string targetDirectory)
        {
            List<string> fileNames = new List<string>();
            using (ZipFile zip = ZipFile.Read(fileName))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
                    fileNames.Add(e.FileName);
                }
            }
            return fileNames;
        }
    }
}
