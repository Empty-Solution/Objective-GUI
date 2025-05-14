using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgMinSizeTransformer : IOgTransformer
{
    public int Order { get; set; } = 999;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        new(rect.x, rect.y, !options.TryGetValue("MinWidth", out float minWidth) ? rect.width : Mathf.Max(rect.width, minWidth),
            !options.TryGetValue("MinHeight", out float minHeight) ? rect.height : Mathf.Max(rect.height, minHeight));
}