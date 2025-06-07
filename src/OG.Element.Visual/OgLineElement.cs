using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgLineElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkGetProvider<float> lineWidth,
    IDkGetProvider<Vector2> endPosition, IDkGetProvider<Vector2> startPosition)
    : OgVisualElement<OgLineGraphicsContext>(name, provider, rectGetter), IOgLineElement
{
    public IDkGetProvider<Vector2> StartPosition => startPosition;
    public IDkGetProvider<Vector2> EndPosition   => endPosition;
    public IDkGetProvider<float>   LineWidth     => lineWidth;
    protected override void FillContext()
    {
        m_RenderContext               ??= new();
        m_RenderContext.StartPosition =   StartPosition.Get();
        m_RenderContext.EndPosition   =   EndPosition.Get();
        m_RenderContext.LineWidth     =   LineWidth.Get();
    }
}