using DK.Processing.Abstraction.Generic;
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
public class OgBindableBuilder<TValue>(
    IOgElementFactory<IOgInteractableValueElement<IOgVisualElement, TValue>, OgBindableFactoryArguments<TValue>> factory,
    IDkProcessor<OgBindableBuildContext<TValue>>? processor)
    : OgInteractableBuilder<IOgElementFactory<IOgInteractableValueElement<IOgVisualElement, TValue>, OgBindableFactoryArguments<TValue>>,
        IOgInteractableValueElement<IOgVisualElement, TValue>, OgBindableFactoryArguments<TValue>, OgBindableBuildArguments<TValue>,
        OgBindableBuildContext<TValue>, OgTransformerRectGetter, IOgVisualElement>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgBindableBuildArguments<TValue> args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgBindableFactoryArguments<TValue> BuildFactoryArguments(OgBindableBuildContext<TValue> context,
        OgBindableBuildArguments<TValue> args, IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, context.ValueProvider, args.ValueOverride, args.Bind, args.BindTypeGetProvider);
    protected override OgBindableBuildContext<TValue> BuildContext(OgBindableBuildArguments<TValue> args, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter) =>
        new(null!, getter, args.Value);
}