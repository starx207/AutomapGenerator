namespace AutomapGenerator;
/*
 * TODO: Additional features to add support for:
 *   1. ✅ Ignoring members on the destination object (opt.Ignore())
 *   2. ~✅ Specifying where on the source to map the destination member from (opt.MapFrom(src => src.Somewhere))
 *       - Should I prevent people from putting business logic in here? I could make it so the source generator will only map it if the property exists on the source.
 *       - UPDATE: The way this is currently implemented, it will only allow simple member access, therefore it would prevent business logic leakage. If I decide to
 *                 allow business logic, further changes will be required.
 *       - TODO: Not sure what would happen if I did something like calling ToString() on a member. What about calling a function that requires parameters?
 *               On the face of it, I would say neither should be allowed as it wouldn't translate to an IQueryable for something like SQL server, but should that
 *               be enforced by the generator or should I let the users decide whether to do that or not? If I allow it, the same issue could present itself as would
 *               happen by allowing more than simple member access expressions - namely, the how to handle the lambda parameter name being referenced in places other
 *               than the beginning of the expression.
 *       - UPDATE 2: I decided to allow any type of LINQ expression for explicit mappings. The user is responsible for making sure it is compatible with SQL if they use it.
 *                   This greatly simplified my approach to null handling.
 *   3. ✅ Add additional source prefixes that the mapping will recognize (RecognizePrefixes())
 *   4. ✅ Add destination prefixes that the mapping will recognize (RecognizeDestinationPrefixes())
 *   5. Support for mapping to a constructor?????
 *       - Not sure what this would entail. I think Automapper does this automatically if it can find a constructor
 *       - If I add this, I should also add:
 *          1. Ability to disable constructor mapping (DisableConstructorMapping())
 *   6. Ability to configure reverse mapping (CreateMap().ReverseMap())
 *       - Does (should) this work for CreateProjection too?
 *       - Initially, I'll just support reversing mappings that don't do any flattening. If the mapping does any flattening, unflattening it
 *         should be possible, but will require we know how to construct the nested object, which could get complicated.
 *   7. Inheriting a MapProfile from another assembly (or nuget package) should still work
 *   8. Ability to define base class mapping and include it in all derived mappings (CreateMap().IncludeAllDerived())
 *   9. Ability to specify how to construct the destination object (CreateMap().ConstructUsing(src => new Dest(src.Prop)))
 *       - Does (should) this work for CreateProjection too?
 *  10. Configure all destination properties at once (CreateMap().ForAllMembers(opt => opt.whatever()))
 *       - Should the options here be the same as those for a single member? Or should I have a different options object for
 *         configuring all at once?
 *  11. Don't map between 2 properties of different types (skip them and report a compiler warning instead)
 *  12. ~✅ Would it be possible to do code-analysis on the places where IMapper.Map or IMapper.ProjectTo are called and generate the mappings on the fly?
 *       - This would reduce the need for so many map profiles to be created explicitly.
 *       - We'd need warnings and errors for anything where the mapping is not straight forward, so the user knows they need a map profile to handle the
 *         anomalies.
 *       - This would also change some of the warnings/errors listed below (or negate some entirely, like 6 and 7)
 */

/*
 * TODO: These are the types of warnings/errors I need to report:
 *   1. WARNING: Not all properties of the destination can be mapped. Maybe the warning could list the properties
 *       - Should I distinguish between properties that can't be mapped because they have no match in the source
 *         and those that have a match, but have different types?
 *   2. ERROR?: Duplicate mapping definitions
 *   3. ERROR?: Invalid MapFrom expression. Any expression that is not simple member access (unless I later add support for it)
 *       - I think this should be an error because it would result in a mapping other than what the user expects. An argument could be made for
 *         making it a warning as the mapper will still be generated and the invalid MapFrom simply ignored, but I'm thinking an error is more appropriate.
 *   4. WARNING: When unable to determine the value of a prefix in RecognizePrefixes(). Such as when a local variable or a field is being passed to the function.
 *       - I may be able to support these scenarios to an extent by looking to see where the variable is defined and get its value there, but this could introduce lots of other complexities.
 *          => The variable could get re-assigned before being passed to the function in question
 *          => The variable might not have a value initially and get its value from a function call
 *       - Consider the value of adding the support and perhaps limit the allowed values to string literals (or constants?) only. In which case this should be changed from a warning to an error.
 *   5. ERROR: Mapping to a destination type that is unable to be constructed (This could mean a lot of things, and will need refined once I've completed more of the additional features above)
 *       - If I don't add constructor mapping support, I'd need this error for anything without a parameterless constructor and no "ConstructUsing"
 *       - If I do add constructor mapping support, I'd need this error if no suitable constructor is found and no "ConstructUsing"
 *   6. ERROR: IMapper.Map called with unsupported types
 *   7. ERROR: IMapper.ProjectTo called with unsupported types
 *       - For this and #6, how would we handle generic types? If IMapper used somewhere that we have a generic method or class that then passes its type params to IMapper,
 *         would we still be able to do this analysis? Maybe we add a warning instead of an error? Maybe we do nothing in that instance and let the user hang themselves 😱
 */
public abstract class MapProfile {
    protected MapBuilder<TSource, TDestination> CreateMap<TSource, TDestination>() => new();
    protected MapBuilder<TSource, TDestination> CreateProjection<TSource, TDestination>() => new();
    protected void RecognizePrefixes(params string[] prefixes) { }
    protected void RecognizeDestinationPrefixes(params string[] prefixes) { }
}
