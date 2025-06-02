using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgMinSizeTransformerOption(float minWidth = 0, float minHeight = 0) : IOgTransformerOption
{
    public float MinWidth  { get; } = minWidth;
    public float MinHeight { get; } = minHeight;
}