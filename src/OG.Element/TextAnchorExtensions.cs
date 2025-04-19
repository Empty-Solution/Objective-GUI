using UnityEngine;

namespace OG.Element;

public static class TextAnchorExtensions
{
    public static Vector2 GetAlignmentOffset(this TextAnchor alignment, Rect parentRect, Vector2 elementSize)
    {
        float offsetX = alignment switch
        {
            TextAnchor.UpperLeft or TextAnchor.MiddleLeft or TextAnchor.LowerLeft => parentRect.x,
            TextAnchor.UpperCenter or TextAnchor.MiddleCenter or TextAnchor.LowerCenter => parentRect.x + ((parentRect.width - elementSize.x) * 0.5f),
            TextAnchor.UpperRight or TextAnchor.MiddleRight or TextAnchor.LowerRight => parentRect.xMax - elementSize.x,
            _ => 0f
        };

        float offsetY = alignment switch
        {
            TextAnchor.UpperLeft or TextAnchor.UpperCenter or TextAnchor.UpperRight => parentRect.y,
            TextAnchor.MiddleLeft or TextAnchor.MiddleCenter or TextAnchor.MiddleRight => parentRect.y + ((parentRect.height - elementSize.y) * 0.5f),
            TextAnchor.LowerLeft or TextAnchor.LowerCenter or TextAnchor.LowerRight => parentRect.yMax - elementSize.y,
            _ => 0f
        };
        return new(offsetX, offsetY);
    }
}