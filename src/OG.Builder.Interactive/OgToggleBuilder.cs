using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Interactive;
public class OgToggleBuilder(IOgElementFactory<IOgToggle<IOgVisualElement>, OgElementFactoryArguments> factory,
    IDkProcessor<OgToggleBuildContext> processor)
    : OgInteractableBuilder<IOgElementFactory<IOgToggle<IOgVisualElement>, OgElementFactoryArguments>, IOgToggle<IOgVisualElement>,
        OgElementFactoryArguments, OgToggleBuildArguments, OgToggleBuildContext, OgTransformerRectGetter, IOgVisualElement>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgToggleBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgElementFactoryArguments BuildFactoryArguments(OgToggleBuildContext context, OgToggleBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider);
    protected override OgToggleBuildContext BuildContext(OgToggleBuildArguments args, IOgOptionsContainer container, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter)
    {
        DkObservable<bool>         observable = new([]);
        DkObservableProperty<bool> property   = new(observable, args.Value);
        return new(null!, getter, container, property, observable);
    }
}