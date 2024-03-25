using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests;
internal static class SourceReader {
    public static string GetSourceFor<T>() where T : ISourceFile, new() => GetSourceFor<T>(new T());

    public static string GetSourceFor<T>(T source) where T : ISourceFile {
        var codePath = source.GetSourceFilePath();
        return File.ReadAllText(codePath);
    }

    public static string WhereAmI([CallerFilePath] string filePath = "") => filePath;
}
