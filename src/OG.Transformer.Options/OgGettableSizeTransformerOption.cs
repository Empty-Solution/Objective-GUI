using DK.Getting.Abstraction.Generic;
using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgGettableSizeTransformerOption(IDkGetProvider<float>? width = null, IDkGetProvider<float>? height = null) : IOgTransformerOption
{
    public IDkGetProvider<float>? Width { get; } = width;
    public IDkGetProvider<float>? Height { get; } = height;
}