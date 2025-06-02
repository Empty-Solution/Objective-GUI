using OG.DataKit.Transformer;
using UnityEngine;
namespace OG.DataKit.Animation.Extensions;
public static class OgAnimationGetterExtensions
{
    public static Rect AdjustRect(this OgAnimationGetter<OgTransformerRectGetter, Rect> getter, bool value, Rect rect, float xOffset, float yOffset)
    {
        getter.SetTime();
        if(value)
        {
            rect.position += new Vector2(xOffset, yOffset);
            rect.size     -= new Vector2(xOffset * 2, yOffset * 2);
        }
        else
        {
            rect.position -= new Vector2(xOffset, yOffset);
            rect.size     += new Vector2(xOffset * 2, yOffset * 2);
        }
        return rect;
    }
}