using OG.DataTypes.Orientation;
namespace OG.Transformer.Options;
public class OgInvertedFlexiblePositionTransformerOption(EOgOrientation orientation = EOgOrientation.HORIZONTAL, float padding = 0f)
    : OgOrientableTransformerOption(orientation)
{
    public float Padding { get; } = padding;
}