using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgMaxSizeTransformer : OgTransformerBase<OgMaxSizeOption>
{
    public override int Order => int.MaxValue;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, OgMaxSizeOption option) =>
        new(rect.x, rect.y, Mathf.Min(rect.width, option.MaxWidth), Mathf.Min(rect.height, option.MaxHeight));
}