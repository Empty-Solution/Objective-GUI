using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgSizeTransformerOption(float width = 0, float height = 0) : IOgTransformerOption
{
    public float Width  { get; } = width;
    public float Height { get; } = height;
}