using System.Collections.Generic;
namespace OG.Transformer.Abstraction;
public interface IOgOptionsContainer
{
    IEnumerable<IOgTransformerOption> Options { get; }
    IOgOptionsContainer SetOption(IOgTransformerOption option);
    IOgOptionsContainer RemoveOption(IOgTransformerOption option);
}