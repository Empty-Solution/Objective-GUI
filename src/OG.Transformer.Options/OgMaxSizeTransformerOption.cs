using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgMaxSizeTransformerOption(float maxWidth = float.MaxValue, float maxHeight = float.MaxValue) : IOgTransformerOption
{
    public float MaxWidth { get; } = maxWidth;
    public float MaxHeight { get; } = maxHeight;
}