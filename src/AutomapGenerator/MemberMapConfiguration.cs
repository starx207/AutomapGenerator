#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.Linq.Expressions;

namespace AutomapGenerator;

public class MemberMapConfiguration<TSource, TProperty> {
    internal MemberMapConfiguration() {
    }

    public void Ignore() { }

    public void MapFrom(Expression<Func<TSource, TProperty>> sourceMapping) { }

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
    public void MapFrom(Expression<Func<TSource, TProperty?>> sourceMapping, [DisallowNull] TProperty nullFallback) { }
#else
    public void MapFrom(Expression<Func<TSource, TProperty?>> sourceMapping, TProperty nullFallback) { }
#endif

    public void MapFrom(Expression<Func<TSource, TProperty?>> sourceMapping, Expression<Func<TSource, TProperty>> nullFallback) { }

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
    public void NullFallback([DisallowNull] TProperty value) { }
#else
    public void NullFallback(TProperty value) { }
#endif
    public void NullFallback(Expression<Func<TSource, TProperty>> fallbackMapping) { }
}
