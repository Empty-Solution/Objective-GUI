using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgMarginTransformerOption(float x = 0, float y = 0) : IOgTransformerOption
{
    public float MarginX { get; } = x;
    public float MarginY { get; } = y;
}