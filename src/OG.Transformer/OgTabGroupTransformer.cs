using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgTabGroupTransformer : OgBaseTransformer<OgTabGroupTransformerOption>
{
    public override int Order { get; set; } = 25;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgTabGroupTransformerOption option) => rect;
}