using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgTextureFactory : IOgElementFactory<OgQuadElement, OgVisualFactoryArguments>
{
    public OgQuadElement Create(OgVisualFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Color = arguments.Color
        };
}