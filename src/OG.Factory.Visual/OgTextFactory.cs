using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Visual;
public class OgTextFactory : IOgElementFactory<OgTextElement, OgTextFactoryArguments>
{
    public OgTextElement Create(OgTextFactoryArguments arguments) =>
        new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Color         = arguments.Color,
            Font          = arguments.Font,
            FontSize      = arguments.FontSize,
            FontStyle     = arguments.FontStyle,
            PixelsPerUnit = arguments.PixelsPerUnit,
            Spacing       = arguments.Spacing,
            Text          = arguments.Text
        };
}