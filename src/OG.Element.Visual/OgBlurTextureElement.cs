using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Visual;
public class OgBlurTextureElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgTextureElement(name, provider, rectGetter)
{
    public float BlurStrength
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
        Material?.SetFloat("_BlurStrength", BlurStrength);
        return base.Invoke(reason);
    }
}