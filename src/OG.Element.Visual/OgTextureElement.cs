using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Visual;
public class OgTextureElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : OgQuadElement(name, provider, rectGetter)
{
    public Vector4 Borders
    {
        get;
        set
        {
            if(field.Equals(value)) return;
            field = value;
        }
    }
    public override bool Invoke(IOgRenderEvent reason)
    {
        Material?.SetVector("_Borders", Borders);
        Rect rect = ElementRect.Get();
        Material?.SetFloat("_AspectRatio", rect.width / rect.height);
        if(m_RenderContext != null) m_RenderContext.Rect = rect;
        return base.Invoke(reason);
    }
}