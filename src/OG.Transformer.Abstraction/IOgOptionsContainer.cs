using DK.Matching.Abstraction;
namespace OG.Transformer.Abstraction;
public interface IOgOptionsContainer
{
    IDkMatcherProvider<IOgTransformer, IOgTransformerOption> Provider { get; }
    void AddOption(IOgTransformerOption option);
    bool RemoveOption(IOgTransformerOption option);
}