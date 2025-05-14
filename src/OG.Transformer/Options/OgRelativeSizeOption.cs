using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgRelativeSizeOption(float relativeWidth, float relativeHeight) : IOgTransformerOption
{
    public float RelativeWidth  { get; set; } = relativeWidth;
    public float RelativeHeight { get; set; } = relativeHeight;
    public bool CanHandle(IOgTransformer value) => value is OgRelativeSizeTransformer;
}