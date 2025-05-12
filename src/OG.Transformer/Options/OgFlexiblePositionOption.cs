using OG.DataTypes.Orientation;
using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
namespace OG.Transformer.Options;
public sealed class OgFlexiblePositionOption : IOgTransformerOption
{
    public EOgOrientation Orientation                     { get; set; } = EOgOrientation.HORIZONTAL;
    public bool           CanHandle(IOgTransformer value) => value is OgFlexiblePositionTransformer;
}