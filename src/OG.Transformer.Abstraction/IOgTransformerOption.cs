using DK.Matching.Abstraction;
namespace OG.Transformer.Abstraction;
public interface IOgTransformerOption : IDkMatcher<IOgTransformer>
{
    int Order { get; }
}