namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class ProfileWithoutRecognizedPrefixes : MapProfile, ISourceFile {
    public ProfileWithoutRecognizedPrefixes() => CreateMap<OtherSourceWithSinglePrefix, UnprefixedObject>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
