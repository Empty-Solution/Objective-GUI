using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextureElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement<OgTextureGraphicsContext>(name, provider, rectGetter)
{
    public IDkGetProvider<Color>? ColorProvider  { get; set; }
    public IDkGetProvider<Texture2D>?      Texture        { get; set; }
    public Vector4                BorderWidths   { get; set; }
    public Vector4                BorderRadiuses { get; set; }
    public float                  ImageAspect    { get; set; }
    public bool                   AlphaBlend     { get; set; }
    protected override void FillContext()
    {
        m_RenderContext                ??= new();
        m_RenderContext.Color          =   ColorProvider!.Get();
        m_RenderContext.Texture        =   Texture?.Get();
        m_RenderContext.BorderRadiuses =   BorderRadiuses;
        m_RenderContext.BorderWidths   =   BorderWidths;
        m_RenderContext.ImageAspect    =   ImageAspect;
        m_RenderContext.AlphaBlend     =   AlphaBlend;
    }
}