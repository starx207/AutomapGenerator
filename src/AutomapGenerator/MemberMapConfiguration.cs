using System.Linq.Expressions;

namespace AutomapGenerator;

public class MemberMapConfiguration<TSource, TProperty> {
    internal MemberMapConfiguration() {
    }

    public void Ignore() { }

    public void MapFrom(Expression<Func<TSource, TProperty>> sourceMapping) { }
}
