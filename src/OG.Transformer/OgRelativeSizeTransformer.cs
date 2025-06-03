using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgRelativeSizeTransformer : OgBaseTransformer<OgRelativeSizeTransformerOption>
{
    public override int Order { get; set; } = -1;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgRelativeSizeTransformerOption option) =>
        new(rect.x, rect.y, option.RelativeWidth == 0 ? rect.width : parentRect.width * option.RelativeWidth,
            option.RelativeHeight == 0 ? rect.height : parentRect.height * option.RelativeHeight);
}