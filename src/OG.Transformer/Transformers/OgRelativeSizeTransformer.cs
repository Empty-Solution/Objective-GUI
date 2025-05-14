using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeSizeTransformer : IOgTransformer
{
    public int Order { get; set; } = 0;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        new(rect.x, rect.y, !options.TryGetValue("RelativeWidth", out float relativeWidth) ? rect.width : parentRect.width * (1 + relativeWidth),
            !options.TryGetValue("RelativeHeight", out float relativeHeight) ? rect.height : parentRect.height * (1 + relativeHeight));
}