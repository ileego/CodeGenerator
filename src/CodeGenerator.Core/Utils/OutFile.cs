using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeGenerator.Core.Utils
{
    public static class OutFile
    {
        public static void WriteText(string path, string context)
        {
            var dir = path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(path, context);
        }
    }
}
