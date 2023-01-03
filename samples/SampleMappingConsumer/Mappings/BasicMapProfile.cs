using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() {
        CreateMap<SourceObj, DestinationObj>();
        CreateMap<DestinationObj, SourceObj>();
    }
}
