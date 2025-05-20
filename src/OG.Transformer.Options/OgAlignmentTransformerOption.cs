using OG.DataTypes.Alignment;
using OG.Transformer.Abstraction;
namespace OG.Transformer.Options;
public class OgAlignmentTransformerOption(EOgAlignment alignment) : IOgTransformerOption
{
    public EOgAlignment Alignment { get; } = alignment;
}