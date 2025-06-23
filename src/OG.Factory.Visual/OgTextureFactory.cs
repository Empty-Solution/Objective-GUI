using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgTextureFactory : IOgElementFactory<OgTextureElement, OgTextureFactoryArguments>
{
    public OgTextureElement Create(OgTextureFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Texture = arguments.Texture,
            BorderWidths = arguments.BorderWidths,
            BorderRadiuses = arguments.BorderRadiuses,
            AlphaBlend = arguments.AlphaBlend,
            ImageAspect = arguments.ImageAspect,
            ColorProvider = arguments.ColorGetter
        };
}