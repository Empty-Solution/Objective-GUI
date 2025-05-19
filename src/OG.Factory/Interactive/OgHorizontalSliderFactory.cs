using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgHorizontalSliderFactory : IOgElementFactory<IOgSlider<IOgVisualElement>, OgSliderFactoryArguments>
{
    public IOgSlider<IOgVisualElement> Create(OgSliderFactoryArguments arguments) =>
        new OgHorizontalSlider<IOgVisualElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                                                 arguments.ValueProvider);
}