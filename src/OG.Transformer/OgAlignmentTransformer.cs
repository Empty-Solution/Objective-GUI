using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgAlignmentTransformer : OgBaseTransformer<OgAlignmentTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgAlignmentTransformerOption option)
    {
        float x = option.Alignment switch
        {
            TextAnchor.UpperCenter or TextAnchor.MiddleCenter or TextAnchor.LowerCenter => (parentRect.width - rect.width) / 2,
            TextAnchor.UpperRight or TextAnchor.MiddleRight or TextAnchor.LowerRight    => parentRect.width - rect.width,
            _                                                                           => rect.x
        };
        float y = option.Alignment switch
        {
            TextAnchor.MiddleLeft or TextAnchor.MiddleCenter or TextAnchor.MiddleRight => (parentRect.height - rect.height) / 2,
            TextAnchor.LowerLeft or TextAnchor.LowerCenter or TextAnchor.LowerRight    => parentRect.height - rect.height,
            _                                                                          => rect.y
        };
        return new(x, y, rect.width, rect.height);
    }
}