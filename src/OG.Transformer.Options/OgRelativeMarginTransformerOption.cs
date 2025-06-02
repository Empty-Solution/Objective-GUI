using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgRelativeMarginTransformerOption(float relativeX = 0, float relativeY = 0) : IOgTransformerOption
{
    public float RelativeMarginX { get; } = relativeX;
    public float RelativeMarginY { get; } = relativeY;
}