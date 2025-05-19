using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgSizeTransformer : OgBaseTransformer<OgSizeTransformerOption>
{
    public override int Order { get; set; } = 0;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgSizeTransformerOption option) =>
        new(rect.x, rect.y, rect.width + option.Width, rect.height + option.Height);
}