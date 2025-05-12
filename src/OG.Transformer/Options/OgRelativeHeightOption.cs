using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgRelativeHeightOption(float relativeHeight) : IOgTransformerOption
{
    public float RelativeHeight { get; set; } = relativeHeight;
    public bool  CanHandle(IOgTransformer value) => value is OgRelativeSizeTransformer;
}