using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgRelativeMarginXOption(float relativeMarginX) : IOgTransformerOption
{
    public float RelativeMarginX { get; set; } = relativeMarginX;
    public bool  CanHandle(IOgTransformer value) => value is OgRelativeMarginTransformer;
}