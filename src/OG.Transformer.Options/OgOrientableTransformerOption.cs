using OG.DataTypes.Orientation;
using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgOrientableTransformerOption(EOgOrientation orientation = EOgOrientation.HORIZONTAL) : IOgTransformerOption
{
    public EOgOrientation Orientation { get; } = orientation;
}