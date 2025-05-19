using OG.DataTypes.Orientation;
namespace OG.Transformer.Options;
public class OgFlexibleTransformerOption(EOgOrientation orientation = EOgOrientation.HORIZONTAL) : OgOrientableTransformerOption(orientation);