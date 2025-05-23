using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using UnityEngine;
namespace OG.Factory.Visual;
public class OgTextureFactory : IOgElementFactory<OgTextureElement, OgTextureFactoryArguments>
{
    public OgTextureElement Create(OgTextureFactoryArguments arguments)
    {
        OgTextureElement texture = new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Texture        = arguments.Texture,
            BorderWidths   = arguments.BorderWidths,
            BorderRadiuses = arguments.BorderRadiuses,
            AlphaBlend     = arguments.AlphaBlend,
            ImageAspect    = arguments.ImageAspect
        };
        if(arguments.ColorGetter is not null)
            texture.ColorProvider = arguments.ColorGetter;
        else
            texture.Color = (Color)arguments.Color!;
        return texture;
    }
}