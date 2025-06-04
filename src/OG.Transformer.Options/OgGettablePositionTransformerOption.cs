using DK.Getting.Abstraction.Generic;
using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgGettablePositionTransformerOption(IDkGetProvider<float>? x = null, IDkGetProvider<float>? y = null) : IOgTransformerOption
{
    public IDkGetProvider<float>? X { get; } = x;
    public IDkGetProvider<float>? Y { get; } = y;
}