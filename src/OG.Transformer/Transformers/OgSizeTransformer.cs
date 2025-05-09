using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgSizeTransformer : OgTransformerBase<OgSizeOption>
{
    public override int Order => 0;
    public override Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgSizeOption option) =>
        new(rect.x, rect.y, rect.width + option.Width, rect.height + option.Height);
}