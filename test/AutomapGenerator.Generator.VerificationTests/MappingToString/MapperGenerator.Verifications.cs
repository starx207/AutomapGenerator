using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.MappingToString;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("MappingToString", "Snapshots");

    #region Tests

    [Fact]
    public Task MapObjectsToStrings() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.ObjWithoutStringOverride>(),
        SourceReader.GetSourceFor<Sources.ObjWithStringOverride>(),
        SourceReader.GetSourceFor<Sources.DemoMappingToString>()
    }, _snapshotLocation);

    #endregion
}

