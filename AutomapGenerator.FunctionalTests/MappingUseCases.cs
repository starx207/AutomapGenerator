using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutomapGenerator.FunctionalTests.Models;
using FluentAssertions.Execution;
using NSubstitute;

namespace AutomapGenerator.FunctionalTests;

public class MappingUseCases {
    private readonly IMapper _mapper;
    private readonly Fixture _mocker;

    public MappingUseCases() {
        _mocker = new Fixture();
        _mocker.Customize(new AutoNSubstituteCustomization());
        _mapper = new Mapper();
    }

    [Fact]
    public void NewDocumentInput_To_CreateDocumentCommand() {
        var source = _mocker.Create<NewDocumentInput>();
        source.File!.Name.Returns(_mocker.Create<string>());

        // Act
        var destination = _mapper.Map<CreateDocumentCommand>(source);

        // Assert
        using (new AssertionScope()) {
            destination.DocDate.Should().Be(source.DocDate);
            destination.DocType.Should().Be(source.DocType);
            destination.Keywords.Should().BeEquivalentTo(source.Keywords);
            destination.Format.Should().Be(source.Format);
            destination.FileName.Should().Be(source.File.Name);
            destination.Content.Should().BeNull();
        }
    }

    [Fact]
    public void DocMetaData_To_DocSearchViewModel() {
        // Arrange
        var source = _mocker.Create<DocMetaData>();

        // Act
        var destination = _mapper.Map<DocSearchViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.Id.Should().Be(source.Id);
            destination.DocTitle.Should().Be(source.DocTitle);
            destination.Creator.Should().Be(source.CreateUser!.UserName);
            destination.CreateDate.Should().Be(source.CreateDate);
            destination.ChangeDate.Should().Be(source.ChangeDate);
            destination.DocDeleteDate.Should().Be(source.DeleteDate);
            destination.DocDate.Should().Be(source.DocDate);
            destination.Length.Should().Be(source.DocLen);
            destination.Type.Should().Be(source.Type!.Code);
            destination.SortIndex.Should().Be(source.Type.SortIndex);
        }
    }

    [Fact]
    public void DocMetaData_To_DocSearchViewModel_WithNullFallbacks() {
        // Arrange
        var source = _mocker.Build<DocMetaData>()
            .Without(x => x.CreateUser)
            .Without(x => x.Type)
            .Create();

        // Act
        var destination = _mapper.Map<DocSearchViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.Creator.Should().Be(source.DocUser);
            destination.Type.Should().BeNull();
            destination.SortIndex.Should().BeNull();
        }
    }

    [Fact]
    public void DocMetaData_To_DocumentDownloadViewModel() {
        // Arrange
        var source = _mocker.Create<DocMetaData>();

        // Act
        var destination = _mapper.Map<DocumentDownloadViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.FileFormat.Should().Be(source.Type!.Code);
            destination.FileName.Should().Be($"{source.DocTitle}.{source.Type.Code.ToLower()}");
            destination.Content.Should().BeEquivalentTo(source.DocContent.DocBinary, opt => opt.WithStrictOrdering());
        }
    }

    [Fact]
    public void DocMetaData_To_DocumentDownloadViewModel_WithNullFallbacks() {
        // Arrange
        var source = _mocker.Build<DocMetaData>()
            .Without(x => x.Type)
            .Create();

        // Act
        var destination = _mapper.Map<DocumentDownloadViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.FileFormat.Should().BeEmpty();
            destination.FileName.Should().Be(source.DocTitle);
        }
    }

    [Fact]
    public void DocMetaData_To_ModifyDocPatch() {
        // Arrange
        var source = _mocker.Create<DocMetaData>();

        // Act
        var destination = _mapper.Map<ModifyDocPatch>(source);

        // Assert
        using (new AssertionScope()) {
            destination.DocTitle.Should().Be(source.DocTitle);
            destination.DocDate.Should().Be(source.DocDate);
            // TODO: The mapper should automatically call the ToString method for each keyword in order to map it
            //destination.Keywords.Should().BeEquivalentTo(source.Keywords.Select(k => k.ToString()));
            destination.Should().BeNull(because: "The mapper should automatically map Keyword objects to strings!");
        }
    }

    [Fact]
    public void DocMetaData_To_MoveDocPatch() {
        // Arrange
        var source = _mocker.Create<DocMetaData>();

        // Act
        var destination = _mapper.Map<MoveDocPatch>(source);

        // Assert
        using (new AssertionScope()) {
            destination.NewDocTitle.Should().Be(source.DocTitle);
            destination.NewTypeCode.Should().Be(source.Type!.Code);
        }
    }

    [Fact]
    public void DocMetaData_To_MoveDocPatch_WithNullFallbacks() {
        // Arrange
        var source = _mocker.Build<DocMetaData>()
            .Without(x => x.Type)
            .Create();

        // Act
        var destination = _mapper.Map<MoveDocPatch>(source);

        // Assert
        destination.NewTypeCode.Should().BeNull();
    }

    [Fact(Skip = "Not Implemented")]
    public void MapServiceTestThatIncludesDerivedTypesInBaseMapping() {
        // Arrange
        // TODO: The Map service creates a base class mapping and uses IncludeAllDerived(). Make a functional test for that scenario

        // Act

        // Assert

    }

    [Fact(Skip = "Not Implemented")]
    public void ParcelServiceTestThatUtilizesConstructUsing() {
        // Arrange
        // TODO: Parcel service has a mapping for related parcels that specifies a constructor and ignores all members

        // Act

        // Assert

    }
}
