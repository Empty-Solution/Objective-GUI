using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextureElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement<OgTextureGraphicsContext>(name, provider, rectGetter)
{
    public IDkGetProvider<Color>?      ColorProvider  { get; set; }
    public IDkGetProvider<Texture2D?>? Texture        { get; set; }
    public IDkGetProvider<Vector4>?    BorderWidths   { get; set; }
    public IDkGetProvider<Vector4>?    BorderRadiuses { get; set; }
    public float                       ImageAspect    { get; set; }
    public bool                        AlphaBlend     { get; set; }
    public ScaleMode                   ScaleMode      { get; set; } = ScaleMode.StretchToFill;
    protected override void FillContext()
    {
        m_RenderContext                ??= new();
        m_RenderContext.ScaleMode      =   ScaleMode;
        m_RenderContext.Color          =   ColorProvider!.Get();
        m_RenderContext.Texture        =   Texture?.Get();
        m_RenderContext.BorderRadiuses =   BorderRadiuses?.Get() ?? new();
        m_RenderContext.BorderWidths   =   BorderWidths?.Get() ?? new();
        m_RenderContext.ImageAspect    =   ImageAspect;
        m_RenderContext.AlphaBlend     =   AlphaBlend;
    }
}