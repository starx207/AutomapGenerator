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

    [Fact]
    public void MapServiceTestThatIncludesDerivedTypesInBaseMapping() {
        // Arrange
        // TODO: Currently, the profile for this is commented out because it causes an error in the generated code.
        // TODO: The way the generator is now working, I would not actually need the "IncludeAllDerived" api as it would include the derived classes by default
        var source = _mocker.Create<CreateShapeCommand>();

        // Act
        var footprint = _mapper.Map<ShapeFootprint>(source);
        var other = _mapper.Map<ShapeOther>(source);

        // Assert
        using (new AssertionScope()) {
            footprint.LayerId.Should().BeEmpty();
            footprint.ShapeArea.Should().BeNull();
            footprint.Code.Should().Be(source.Code);
            footprint.Description.Should().Be(source.Description);
            footprint.Altitude.Should().Be(source.Altitude);
            footprint.ShapeColor.Should().Be(source.ShapeColor);
            footprint.AdditionalInfo.Should().Be(source.AdditionalInfo);
            footprint.ExternalId.Should().Be(source.ExternalId);
            footprint.SourceRecId.Should().Be(source.SourceRecId);
            footprint.BinaryCheckSum.Should().Be(source.BinaryCheckSum);
            footprint.RotationAngle.Should().Be(source.RotationAngle);

            other.LayerId.Should().BeEmpty();
            other.ShapeArea.Should().BeNull();
            other.Code.Should().Be(source.Code);
            other.Description.Should().Be(source.Description);
            other.Altitude.Should().Be(source.Altitude);
            other.ShapeColor.Should().Be(source.ShapeColor);
            other.AdditionalInfo.Should().Be(source.AdditionalInfo);
            other.ExternalId.Should().Be(source.ExternalId);
            other.SourceRecId.Should().Be(source.SourceRecId);
            other.BinaryCheckSum.Should().Be(source.BinaryCheckSum);
            other.RotationAngle.Should().Be(source.RotationAngle);
        }
    }

    [Fact]
    public void ParcelServiceTestThatUtilizesConstructUsing() {
        // Arrange
        // TODO: The profile for this is currently commented out as the methods in it are not-yet-implemented
        var source = _mocker.Create<Parcel>();

        // Act
        var destination = _mapper.Map<RelatedParcelViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.RelatedParcelNumber.Should().Be(source.Number);
            destination.District.Should().BeNull();
        }
    }
}
