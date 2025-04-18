using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Visual;

public abstract class OgVisual<TStyle, TScope, TGraphicsContext>(
    string name,
    TStyle style,
    TScope scope,
    IOgTransform transform,
    IOgGraphics<TGraphicsContext> graphics)
    : OgElement<TScope>(name, scope, transform), IOgVisual<TStyle> where TStyle : IOgStyle
    where TScope : IOgTransformScope
    where TGraphicsContext : IOgGraphicsContext
{
    public IOgGraphics<TGraphicsContext> Graphics => graphics;
    public TStyle Style => style;

    protected override void InternalOnGUI(OgEvent reason)
    {
        if(reason.Type is not EventType.Repaint) return;
        OnRepaint(reason);
    }

    protected void OnRepaint(OgEvent reason) => graphics.Draw(GetGraphicsContext(reason));

    protected abstract TGraphicsContext GetGraphicsContext(OgEvent reason);

    protected virtual Rect GetTransformRectWithOffset()
    {
        Rect rect = Transform.LocalRect;
        Vector4 offset = Style.Offset;

        if(offset == Vector4.zero) return rect;

        rect.x += offset.x;
        rect.y += offset.y;
        rect.width -= offset.z;
        rect.height -= offset.w;

        return rect;
    }
}