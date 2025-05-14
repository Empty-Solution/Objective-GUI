using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgAlignmentTransformer : IOgTransformer
{
    public int Order { get; set; } = 1000;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options)
    {
        if(!options.TryGetValue("Alignment", out TextAnchor alignment)) return rect;
        return new(alignment switch
        {
            TextAnchor.UpperRight or TextAnchor.MiddleRight or TextAnchor.LowerRight    => parentRect.xMax - rect.width,
            TextAnchor.UpperCenter or TextAnchor.MiddleCenter or TextAnchor.LowerCenter => parentRect.x + ((parentRect.width - rect.width) / 2),
            TextAnchor.UpperLeft or TextAnchor.MiddleLeft or TextAnchor.LowerLeft       => parentRect.x,
            _                                                                           => rect.x
        }, alignment switch
        {
            TextAnchor.LowerLeft or TextAnchor.LowerCenter or TextAnchor.LowerRight    => parentRect.yMax - rect.height,
            TextAnchor.MiddleLeft or TextAnchor.MiddleCenter or TextAnchor.MiddleRight => parentRect.y + ((parentRect.height - rect.height) / 2),
            TextAnchor.UpperLeft or TextAnchor.UpperCenter or TextAnchor.UpperRight    => parentRect.y,
            _                                                                          => rect.y
        }, rect.width, rect.height);
    }
}