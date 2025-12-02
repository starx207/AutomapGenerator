using System;
using System.Collections.Generic;

namespace AutomapGenerator.Generator.VerificationTests.CollectionMapping.Sources;
public class ClassThatMapsCollections : ISourceFile {
    private readonly IMapper _mapper = null!;

    private readonly FullSourceObj[] _sourceArray = Array.Empty<FullSourceObj>();

    // private readonly List<FullSourceObj> _sourceList = new();
    // private readonly HashSet<FullSourceObj> _sourceHash = new();

    public void Test() {
        // TODO: Once I figure out this set, I should create other tests for the other source types
        var arrayToArray = _mapper.Map<FullDestinationObj[]>(_sourceArray);
        var arrayToList = _mapper.Map<List<FullDestinationObj>>(_sourceArray);
        var arrayToReadOnlyList = _mapper.Map<IReadOnlyList<FullDestinationObj>>(_sourceArray);
        var arrayToCollection = _mapper.Map<ICollection<FullDestinationObj>>(_sourceArray);
        var arrayToEnumerable = _mapper.Map<IEnumerable<FullDestinationObj>>(_sourceArray);
        // TODO: ToHashSet is not supported in netstandard. Do I need the source generator to do something different depending on the target framework?
        var arrayToHashSet = _mapper.Map<HashSet<FullDestinationObj>>(_sourceArray);
        var arrayToSet = _mapper.Map<ISet<FullDestinationObj>>(_sourceArray);

        // var listToArray = _mapper.Map<FullDestinationObj[]>(_sourceList);
        // var listToList = _mapper.Map<List<FullDestinationObj>>(_sourceList);
        // var listToReadOnlyList = _mapper.Map<IReadOnlyList<FullDestinationObj>>(_sourceList);
        // var listToCollection = _mapper.Map<ICollection<FullDestinationObj>>(_sourceList);
        // var listToEnumerable = _mapper.Map<IEnumerable<FullDestinationObj>>(_sourceList);

        // var hashToArray = _mapper.Map<FullDestinationObj[]>(_sourceHash);
        // var hashToList = _mapper.Map<List<FullDestinationObj>>(_sourceHash);
        // var hashToReadOnlyList = _mapper.Map<IReadOnlyList<FullDestinationObj>>(_sourceHash);
        // var hashToCollection = _mapper.Map<ICollection<FullDestinationObj>>(_sourceHash);
        // var hashToEnumerable = _mapper.Map<IEnumerable<FullDestinationObj>>(_sourceHash);
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
