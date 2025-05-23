using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgQuadFactory : IOgElementFactory<OgQuadElement, OgQuadFactoryArguments>
{
    public OgQuadElement Create(OgQuadFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            TopLeftColor     = arguments.TopLeft,
            TopRightColor    = arguments.TopRight,
            BottomLeftColor  = arguments.BottomLeft,
            BottomRightColor = arguments.BottomRight
        };
}