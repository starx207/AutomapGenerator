using System.Runtime.CompilerServices;
using VerifyTests;

namespace AutomapGenerator.Generator.VerificationTests;
public static class ModuleInitializer {
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
