// <copyright file="FileTools.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lotto.Common.Tools
{
    public static class FileTools
    {
        public static List<string> FilterByExtension(List<string> filesToFilter, string format)
        {
            string stringFileFormat = "." + format.ToLower();
            return filesToFilter.Where(x => Path.GetExtension(x) == stringFileFormat).ToList();
        }
    }
}
