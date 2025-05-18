using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgTextureFactory : IOgElementFactory<OgQuadElement, OgTextureFactoryArguments>
{
    public OgQuadElement Create(OgTextureFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Color    = arguments.Color,
            Material = arguments.Material
        };
}