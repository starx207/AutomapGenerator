using System.Linq.Expressions;

namespace AutomapGenerator;
public class MapBuilder<TSource, TDestination> {
    internal MapBuilder() {

    }

    public MapBuilder<TSource, TDestination> ForMember<TProperty>(
        Expression<Func<TDestination, TProperty>> destinationMember,
        Expression<Action<MemberMapConfiguration<TSource, TProperty>>> mapConfiguration
    ) => this;

    public MapBuilder<TSource, TDestination> ForMember<TProperty>(
        Expression<Func<TDestination, TProperty>> destinationMember,
        Expression<Action<NullableValueMemberMapConfiguration<TSource, TProperty>>> mapConfiguration
    ) where TProperty : struct => this;
}
