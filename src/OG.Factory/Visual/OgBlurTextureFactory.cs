using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgBlurTextureFactory : IOgElementFactory<OgBlurTextureElement, OgBlurTextureFactoryArguments>
{
    public OgBlurTextureElement Create(OgBlurTextureFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Color        = arguments.Color,
            Material     = arguments.Material,
            Borders      = arguments.Borders,
            BlurStrength = arguments.BlurStrength
        };
}