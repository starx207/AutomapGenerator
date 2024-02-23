namespace AutomapGenerator;
// TODO: I think a better name for this project and NuGet package could be "AutomapWeaver". I think it is catchier than "AutomapGenerator"
//       and still conveys source-generation and a Automapper replacement package.
public interface IMapper {
    TDestination Map<TDestination>(object source);
    // TODO: Make the source type generic on this method to avoid boxing
    TDestination Map<TDestination>(object source, TDestination destination);
    IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source);
}
