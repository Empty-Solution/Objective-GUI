using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgMaxSizeOption(float maxWidth, float maxHeight) : IOgTransformerOption
{
    public float MaxWidth  { get; set; } = maxWidth;
    public float MaxHeight { get; set; } = maxHeight;
    public bool CanHandle(IOgTransformer value) => value is OgMaxSizeTransformer;
}