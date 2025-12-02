namespace AutomapGenerator.Generator.VerificationTests.Troubleshoot.Sources;
public class Demo : ISourceFile {
    private readonly IMapper _mapper = null!;

    public void RunDemo() {
        _ = _mapper.Map<CreateDocumentCommand>(new NewDocumentInput());
        _ = _mapper.Map<DocSearchViewModel>(new DocMetaData());
        _ = _mapper.Map<DocSearchViewModel>(new DocMetaData());
        _ = _mapper.Map<DocumentDownloadViewModel>(new DocMetaData());
        _ = _mapper.Map<DocumentDownloadViewModel>(new DocMetaData());
        _ = _mapper.Map<ModifyDocPatch>(new DocMetaData());
        _ = _mapper.Map<MoveDocPatch>(new DocMetaData());
        _ = _mapper.Map<MoveDocPatch>(new DocMetaData());
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
