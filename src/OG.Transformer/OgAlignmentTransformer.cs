using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgAlignmentTransformer : OgBaseTransformer<OgAlignmentTransformerOption>
{
    public override int Order { get; set; } = 1000;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgAlignmentTransformerOption option) =>
        new(option.Alignment switch
        {
            TextAnchor.UpperRight or TextAnchor.MiddleRight or TextAnchor.LowerRight    => parentRect.xMax - rect.width,
            TextAnchor.UpperCenter or TextAnchor.MiddleCenter or TextAnchor.LowerCenter => parentRect.x + ((parentRect.width - rect.width) / 2),
            TextAnchor.UpperLeft or TextAnchor.MiddleLeft or TextAnchor.LowerLeft       => parentRect.x,
            _                                                                           => rect.x
        }, option.Alignment switch
        {
            TextAnchor.LowerLeft or TextAnchor.LowerCenter or TextAnchor.LowerRight    => parentRect.yMax - rect.height,
            TextAnchor.MiddleLeft or TextAnchor.MiddleCenter or TextAnchor.MiddleRight => parentRect.y + ((parentRect.height - rect.height) / 2),
            TextAnchor.UpperLeft or TextAnchor.UpperCenter or TextAnchor.UpperRight    => parentRect.y,
            _                                                                          => rect.y
        }, rect.width, rect.height);
}