using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public class OgRelativeMarginTransformer : IOgTransformer
{
    public int Order { get; set; } = 20;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        new(!options.TryGetValue("RelativeMarginX", out float relativeX) ? rect.x : parentRect.x * (1 + relativeX),
            !options.TryGetValue("RelativeMarginY", out float relativeY) ? rect.y : parentRect.y * (1 + relativeY), rect.width, rect.height);
}