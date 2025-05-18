using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgHorizontalSliderFactory<TElement> : IOgElementFactory<IOgSlider<TElement>, OgSliderFactoryArguments> where TElement : IOgElement
{
    public IOgSlider<TElement> Create(OgSliderFactoryArguments arguments) =>
        new OgHorizontalSlider<TElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                                         arguments.ValueProvider);
}