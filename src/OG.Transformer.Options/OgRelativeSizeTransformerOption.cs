using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgRelativeSizeTransformerOption(float relativeWidth = 0, float relativeHeight = 0) : IOgTransformerOption
{
    public float RelativeWidth { get; } = relativeWidth;
    public float RelativeHeight { get; } = relativeHeight;
}