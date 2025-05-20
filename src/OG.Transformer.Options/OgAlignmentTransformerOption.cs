using OG.DataTypes.Alignment;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Options;
public class OgAlignmentTransformerOption(EOgAlignment alignment) : IOgTransformerOption
{
    public EOgAlignment Alignment { get; } = alignment;
}