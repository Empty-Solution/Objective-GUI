using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public class OgRelativeMarginYOption(float relativeMarginY) : IOgTransformerOption
{
    public float RelativeMarginY { get; set; } = relativeMarginY;
    public bool  CanHandle(IOgTransformer value) => value is OgRelativeMarginTransformer;
}