using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public abstract class OgStyled<TElement, TStyle, TScope>(string name, TStyle style, TScope rootScope, IOgTransform transform)
    : OgControl<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgStyle
{
    private static readonly GUIStyle tempStyle = new();

    public TStyle Style { get; set; } = style;

    protected override void DoElement(OgEvent reason, Rect rect) => DoStyledElement(reason, GetTransformRectWithOffset(rect), Style);

    protected abstract void DoStyledElement(OgEvent reason, Rect rect, TStyle style);

    protected virtual Rect GetTransformRectWithOffset(Rect rect)
    {
        Vector4 offset = Style.Offset;

        if(offset == Vector4.zero) return rect;

        rect.x += offset.x;
        rect.y += offset.y;
        rect.width -= offset.z;
        rect.height -= offset.w;

        return rect;
    }
}