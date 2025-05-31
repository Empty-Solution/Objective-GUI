using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Config;
using EH.Builder.Interactive.Base;
using EH.Builder.Observing;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Event;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EH.Builder.Interactive.Internal;
public class EhInternalDropdownBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseButtonBuilder buttonBuilder, EhBaseModalInteractableBuilder modalInteractableBuilder, EhBaseTextBuilder textBuilder)
    : EhBaseDropdownBuilder(backgroundBuilder, buttonBuilder, textBuilder)
{
    private readonly EhBaseBackgroundBuilder m_BackgroundBuilder = backgroundBuilder;
    private readonly EhBaseTextBuilder       m_TextBuilder       = textBuilder;
    public IOgContainer<IOgElement> Build(string name, IDkProperty<int> selected, IEnumerable<IDkGetProvider<string>> values, float width, float height,
        float x, float y, out IOgOptionsContainer options)
    {
        EhDropdownConfig    dropdownConfig   = provider.DropdownConfig;
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
            optionsContainer = context.RectGetProvider.Options;
        }));
        options = optionsContainer;
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> backgroundObserver = new((getter, value) =>
        {
            getter.SetTime();
            Rect rect = getter.TargetModifier;
            rect.height = value ? ((dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Count()) + dropdownConfig.ModalItemPadding
                              : 0;
            getter.TargetModifier = rect;
        });
        OgTextureElement background = m_BackgroundBuilder.Build($"{name}Background", dropdownConfig.BackgroundColor, dropdownConfig.Width,
            dropdownConfig.Height, x, 0, new(dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border), context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
                backgroundObserver.Getter     = context.RectGetProvider;
                context.RectGetProvider.Speed = provider.AnimationSpeed;
            });
        container.Add(background);
        DkObservableProperty<string> property = new(new DkObservable<string>([]), values.ElementAt(selected.Get()).Get());
        OgTextElement text = m_TextBuilder.Build($"{name}Text", dropdownConfig.TextColor, property, dropdownConfig.TextFontSize,
            dropdownConfig.TextAlignment, dropdownConfig.Width, dropdownConfig.Height, x, 0, context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
            });
        container.Add(text);
        DkScriptableObserver<bool> observer = new();
        observer.OnUpdate += state =>
        {
            background.ZOrder = state ? 2 : 0;
            text.ZOrder       = state ? 2 : 0;
        };
        IOgModalInteractable<IOgElement> button = modalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(dropdownConfig.Width, dropdownConfig.Height))
                       .SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft)).SetOption(new OgMarginTransformerOption(x));
                context.Element.IsInteractingObserver?.AddObserver(backgroundObserver);
                context.Element.IsInteractingObserver?.AddObserver(observer);
                context.Element.IsInteractingObserver?.Notify(false);
            }));
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(dropdownConfig.Width,
                           (dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Count()))
                       .SetOption(new OgMarginTransformerOption(0, dropdownConfig.Height - dropdownConfig.ModalItemPadding));
            }));
        button.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, dropdownConfig.Height, dropdownConfig.Width,
                ((dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Count()) - dropdownConfig.Height))));
        List<EhDropdownTextObserver> observers = [];
        for(int i = 0; i < values.Count(); i++)
        {
            IDkGetProvider<string> value            = values.ElementAt(i);
            OgEventHandlerProvider textEventHandler = new();
            OgAnimationColorGetter textGetter       = new(textEventHandler);
            DkBinding<string>      binding          = new(new DkReadOnlyGetter<string>(value.Get()), property);
            EhDropdownTextObserver textObserver = new(observers, observers.Count, dropdownConfig.ItemTextColor, dropdownConfig.SelectedItemTextColor,
                textGetter, binding, button);
            observers.Add(textObserver);
            sourceContainer.Add(BuildDropdownItem(value, i, selected, textGetter, textEventHandler, textObserver, provider));
        }
        observers[selected.Get()].Update(false);
        backgroundObserver.Getter?.SetTime(1f);
        button.Add(sourceContainer);
        container.Add(button);
        return container;
    }
}