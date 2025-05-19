using OG.DataTypes.Orientation;
namespace OG.Transformer.Options;
public class OgFlexibleSizeTransformerOption(EOgOrientation orientation = EOgOrientation.HORIZONTAL) : OgOrientableTransformerOption(orientation);