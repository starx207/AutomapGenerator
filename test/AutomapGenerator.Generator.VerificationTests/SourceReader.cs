using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests;
internal static class SourceReader {
    public static string GetSourceFor<T>() where T : ISourceFile, new() {
        var codePath = new T().GetSourceFilePath();
        return File.ReadAllText(codePath);
    }

    public static string WhereAmI([CallerFilePath] string filePath = "") => filePath;
}
