using AutomapGenerator.FunctionalTests.Models;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Moq.AutoMock;

namespace AutomapGenerator.FunctionalTests;

public class MappingUseCases {
    private readonly IMapper _mapper;
    private readonly AutoMocker _mocker;

    public MappingUseCases() {
        _mapper = new Mapper();
        _mocker = new AutoMocker();
    }

    [Fact]
    public void NewDocumentInput_To_CreateDocumentCommand() {
        // Arrange
        _mocker.Use<IFormFile>(x => x.Name == "NewFile.html");
        var source = new NewDocumentInput() {
            DocDate = DateTime.Parse("5/2/2020"),
            DocType = "official mail",
            Keywords = new[] { "searchable", "keywords" },
            Format = "HTML",
            File = _mocker.Get<IFormFile>()
        };

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
        var source = new DocMetaData() {
            Id = Guid.NewGuid(),
            DocTitle = "My Document",
            CreateUser = new() { UserName = "joe user" },
            DocDate = DateTime.Parse("8/18/1800"),
            TimeStamp = DateTime.Parse("9/24/1901"),
            DocDateTypeIdx = 34,
            DeleteDate = DateTime.Parse("9/30/2030"),
            Bcs = (long?)90.431,
            DocLen = 729,
            InstanceNameId = null,
            DateTimeAddedToMaster = DateTime.Parse("9/9/2099"),
            CreateDate = DateTime.Parse("10/10/2010"),
            ChangeDate = DateTime.Parse("10/11/2020"),
            Type = new() { Code = "test", SortIndex = 40 }
        };

        // Act
        var destination = _mapper.Map<DocSearchViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.Id.Should().Be(source.Id);
            destination.DocTitle.Should().Be(source.DocTitle);
            destination.Creator.Should().Be(source.CreateUser.UserName);
            destination.CreateDate.Should().Be(source.CreateDate);
            destination.ChangeDate.Should().Be(source.ChangeDate);
            destination.DocDeleteDate.Should().Be(source.DeleteDate);
            destination.DocDate.Should().Be(source.DocDate);
            destination.Length.Should().Be(source.DocLen);
            destination.Type.Should().Be(source.Type.Code);
            destination.SortIndex.Should().Be(source.Type.SortIndex);
        }
    }

    [Fact]
    public void DocMetaData_To_DocSearchViewModel_WithNullFallbacks() {
        // Arrange
        var source = new DocMetaData() {
            DocUser = "joe user",
        };

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
        var source = new DocMetaData() {
            Type = new() { Code = "PDF" },
            DocTitle = "Cool_title",
            DocContent = new() { DocBinary = new byte[] { 9, 34, 23, 62 } }
        };

        // Act
        var destination = _mapper.Map<DocumentDownloadViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.FileFormat.Should().Be(source.Type.Code);
            destination.FileName.Should().Be($"{source.DocTitle}.{source.Type.Code.ToLower()}");
            destination.Content.Should().BeEquivalentTo(source.DocContent.DocBinary, opt => opt.WithStrictOrdering());
        }
    }

    [Fact]
    public void DocMetaData_To_DocumentDownloadViewModel_WithNullFallbacks() {
        // Arrange
        var source = new DocMetaData() {
            Type = null,
            DocTitle = "Cool_title"
        };

        // Act
        var destination = _mapper.Map<DocumentDownloadViewModel>(source);

        // Assert
        using (new AssertionScope()) {
            destination.FileFormat.Should().BeNull();
            destination.FileName.Should().Be(source.DocTitle);
        }
    }

    [Fact]
    public void DocMetaData_To_ModifyDocPatch() {
        // Arrange
        var source = new DocMetaData() {
            DocTitle = "super_cool_title",
            DocDate = DateTime.Parse("11/1/2023"),
            Keywords = new List<Keyword>() {
                new("word1"),
                new("word2")
            }
        };

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
        var source = new DocMetaData() {
            DocTitle = "some_title",
            Type = new() { Code = "my_code" }
        };

        // Act
        var destination = _mapper.Map<MoveDocPatch>(source);

        // Assert
        using (new AssertionScope()) {
            destination.NewDocTitle.Should().Be(source.DocTitle);
            destination.NewTypeCode.Should().Be(source.Type.Code);
        }
    }

    [Fact]
    public void DocMetaData_To_MoveDocPatch_WithNullFallbacks() {
        // Arrange
        var source = new DocMetaData() {
            Type = null
        };

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
