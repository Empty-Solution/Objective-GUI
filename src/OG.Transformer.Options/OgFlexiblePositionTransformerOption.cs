using OG.DataTypes.Orientation;
namespace OG.Transformer.Options;
public class OgFlexiblePositionTransformerOption(EOgOrientation orientation = EOgOrientation.HORIZONTAL) : OgOrientableTransformerOption(orientation);