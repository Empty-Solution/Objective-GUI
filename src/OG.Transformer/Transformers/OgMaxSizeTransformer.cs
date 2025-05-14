using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgMaxSizeTransformer : IOgTransformer
{
    public int Order { get; set; } = 999;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        new(rect.x, rect.y, !options.TryGetValue("MaxWidth", out float maxWidth) ? rect.width : Mathf.Min(rect.width, maxWidth),
            !options.TryGetValue("MaxHeight", out float maxHeight) ? rect.height : Mathf.Min(rect.height, maxHeight));
}