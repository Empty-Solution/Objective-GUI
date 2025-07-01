using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgLineFactory : IOgElementFactory<OgLineElement, OgLineFactoryArguments>
{
    public OgLineElement Create(OgLineFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider, arguments.LineWidth,
            arguments.StartPosition, arguments.EndPosition, arguments.Color);
}