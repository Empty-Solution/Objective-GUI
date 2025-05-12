namespace OG.Transformer.Abstraction;
public interface IOgOptionsContainer
{
    bool TryGetOption(IOgTransformer       transformer, out IOgTransformerOption option);
    void AddOption(IOgTransformerOption    option);
    bool RemoveOption(IOgTransformerOption option);
}