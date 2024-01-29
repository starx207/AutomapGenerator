using System.Runtime.CompilerServices;

namespace AutomapGenerator.Generator.VerificationTests;
public static class ModuleInitializer {
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
