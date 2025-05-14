using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public class OgSizeTransformer : IOgTransformer
{
    public int Order { get; set; } = 0;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options) =>
        new(rect.x, rect.y, !options.TryGetValue("Width", out float width) ? rect.width : rect.width + width,
            !options.TryGetValue("Height", out float height) ? rect.height : rect.height + height);
}