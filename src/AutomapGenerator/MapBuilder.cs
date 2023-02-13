using System.Linq.Expressions;

namespace AutomapGenerator;
public class MapBuilder<TSource, TDestination> {
    internal MapBuilder() {

    }

    public MapBuilder<TSource, TDestination> ForMember<TProperty>(
        Expression<Func<TSource, TProperty>> destinationMember,
        Expression<Action<MemberMapConfiguration<TSource, TProperty>>> mapConfiguration
    ) => this;
}
