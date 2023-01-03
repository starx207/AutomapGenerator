//HintName: Mapper.g.cs
namespace AutomapGenerator
{
    public class Mapper : IMapper
    {
        public TDestination Map<TDestination>(object source)
            where TDestination : new() => Map<TDestination>(source, new TDestination());
        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (SampleMappingConsumer.Models.Source1Obj s, SampleMappingConsumer.Models.DestinationObj d):
                    d.Id = s.Id;
                    d.Type = s.Type;
                    break;
                case (SampleMappingConsumer.Models.Source2Obj s, SampleMappingConsumer.Models.DestinationObj d):
                    d.Id = s.Id;
                    d.Type = s.Type;
                    break;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }

            return destination;
        }
    }
}
