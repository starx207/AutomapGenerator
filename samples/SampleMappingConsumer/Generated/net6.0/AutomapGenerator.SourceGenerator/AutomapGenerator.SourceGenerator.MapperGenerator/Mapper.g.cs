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
                case (SampleMappingConsumer.Models.SourceObj s, SampleMappingConsumer.Models.DestinationObj d):
                    d.Id = s.Id;
                    d.Type = s.Type;
                    d.Timestamp = s.Timestamp;
                    d.InUse = s.InUse;
                    break;
                case (SampleMappingConsumer.Models.DestinationObj s, SampleMappingConsumer.Models.SourceObj d):
                    d.Id = s.Id;
                    d.Type = s.Type;
                    d.Timestamp = s.Timestamp;
                    d.InUse = s.InUse;
                    break;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }

            return destination;
        }
    }
}