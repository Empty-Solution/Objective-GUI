using UnityEngine;
namespace OG.Transformer;
public class OgRelativeTransformer : OgTransformerBase<OgRelativeTransformerOption>
{
    public override Rect Transform(Rect rect, Rect parentRect, OgRelativeTransformerOption option) =>
        new(rect.x, rect.y, rect.width * option.RelativeSize, rect.height * option.RelativeSize);
}