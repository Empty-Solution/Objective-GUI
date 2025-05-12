using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgRelativeWidthOption(float relativeWidth) : IOgTransformerOption
{
    public float RelativeWidth  { get; set; } = relativeWidth;
    public bool  CanHandle(IOgTransformer value) => value is OgRelativeSizeTransformer;
}