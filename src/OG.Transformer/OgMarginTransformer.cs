using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public class OgMarginTransformer : IOgTransformer
{
    public int Order { get; set; } = 20;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        new(!options.TryGetValue("MarginX", out float marginX) ? rect.x : rect.x + marginX,
            !options.TryGetValue("MarginY", out float marginY) ? rect.y : rect.y + marginY, rect.width, rect.height);
}