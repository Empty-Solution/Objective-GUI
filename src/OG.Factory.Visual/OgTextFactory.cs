using OG.Element.Visual;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using UnityEngine;
namespace OG.Factory.Visual;
public class OgTextFactory : IOgElementFactory<OgTextElement, OgTextFactoryArguments>
{
    public OgTextElement Create(OgTextFactoryArguments arguments)
    {
        OgTextElement text = new(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider)
        {
            Font         = arguments.Font,
            FontSize     = arguments.FontSize,
            FontStyle    = arguments.FontStyle,
            Alignment    = arguments.Alignment,
            Text         = arguments.Text,
            WordWrap     = arguments.WordWrap,
            TextClipping = arguments.TextClipping
        };
        if(arguments.ColorGetter is not null)
            text.ColorProvider = arguments.ColorGetter;
        else
            text.Color = (Color)arguments.Color!;
        return text;
    }
}