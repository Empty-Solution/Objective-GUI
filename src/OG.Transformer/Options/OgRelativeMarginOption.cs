using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgRelativeMarginOption(float relativeMarginX, float relativeMarginY) : IOgTransformerOption
{
    public float RelativeMarginX                 { get; set; } = relativeMarginX;
    public float RelativeMarginY                 { get; set; } = relativeMarginY;
    public bool  CanHandle(IOgTransformer value) => value is OgRelativeMarginTransformer;
}