using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeSizeTransformer : OgTransformerBase<OgRelativeSizeOption>
{
    public override int Order => 0;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, OgRelativeSizeOption option) =>
        new(rect.x, rect.y, rect.width * option.RelativeWidth, rect.height * option.RelativeHeight);
}