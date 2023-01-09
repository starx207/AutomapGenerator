using AutomapGenerator;
using SampleEFCoreMappingConsumer.Dto;
using SampleEFCoreMappingConsumer.Entities;

namespace SampleEFCoreMappingConsumer;
internal class MappingProfile : MapProfile {
    public MappingProfile() {
        CreateMap<SourceEntity, SourceDto>();
    }
}
