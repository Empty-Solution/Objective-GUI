using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeWidthTransformer : OgTransformerBase<OgRelativeWidthOption>
{
    public override int Order { get; set; } = 0;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgRelativeWidthOption option) =>
        new(rect.x, rect.y, parentRect.width * (1 + option.RelativeWidth), rect.height);
}