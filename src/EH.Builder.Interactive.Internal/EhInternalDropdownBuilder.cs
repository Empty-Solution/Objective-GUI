using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
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
using OG.Element.Visual.Abstraction;
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
    public IEhDropdown Build(string name, IDkProperty<int> selected, IDkGetProvider<string>[] values, float width, float height, float x, float y)
    {
        EhDropdownConfig    dropdownConfig   = provider.DropdownConfig;
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
                optionsContainer = context.RectGetProvider.Options;
            }));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> backgroundObserver = new((getter, value) =>
        {
            getter.SetTime();
            Rect rect = getter.TargetModifier;
            rect.height = value ? ((dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Length) + dropdownConfig.ModalItemPadding
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
        sourceContainer.Add(background);
        DkObservableProperty<string> property = new(new DkObservable<string>([]), values.ElementAt(selected.Get()).Get());
        OgTextElement text = m_TextBuilder.Build($"{name}Text", dropdownConfig.TextColor, property, dropdownConfig.TextFontSize,
            dropdownConfig.TextAlignment, dropdownConfig.Width, dropdownConfig.Height, x, 0, context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
            });
        sourceContainer.Add(text);
        DkScriptableObserver<bool> observer = new();
        observer.OnUpdate += state =>
        {
            background.ZOrder = state ? 2 : 0;
            text.ZOrder       = state ? 2 : 0;
        };
        IOgModalInteractable<IOgElement> modalInteractable = modalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(dropdownConfig.Width, dropdownConfig.Height))
                       .SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft)).SetOption(new OgMarginTransformerOption(x));
                context.Element.IsInteractingObserver?.AddObserver(backgroundObserver);
                context.Element.IsInteractingObserver?.AddObserver(observer);
                context.Element.IsInteractingObserver?.Notify(false);
            }));
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(dropdownConfig.Width,
                       (dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Length))
                   .SetOption(new OgMarginTransformerOption(0, dropdownConfig.Height - dropdownConfig.ModalItemPadding));
        }));
        modalInteractable.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, dropdownConfig.Height, dropdownConfig.Width,
                ((dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Length) - dropdownConfig.Height))));
        List<EhDropdownTextObserver> observers = [];
        for(int i = 0; i < values.Length; i++)
        {
            IDkGetProvider<string> value            = values.ElementAt(i);
            OgEventHandlerProvider textEventHandler = new();
            OgAnimationColorGetter textGetter       = new(textEventHandler);
            DkBinding<string>      binding          = new(new DkReadOnlyGetter<string>(value.Get()), property);
            EhDropdownTextObserver textObserver = new(observers, observers.Count, dropdownConfig.ItemTextColor, dropdownConfig.SelectedItemTextColor,
                textGetter, binding, modalInteractable);
            observers.Add(textObserver);
            IOgInteractableElement<IOgVisualElement> interactable =
                BuildDropdownItem(value, i, selected, textGetter, textEventHandler, textObserver, provider);
            container.Add(interactable);
        }
        backgroundObserver.Getter?.SetTime(1f);
        modalInteractable.Add(container);
        sourceContainer.Add(modalInteractable);
        return new EhDropdown(sourceContainer, container, optionsContainer, getProvider =>
        {
            OgEventHandlerProvider textEventHandler = new();
            OgAnimationColorGetter textGetter       = new(textEventHandler);
            DkBinding<string>      binding          = new(new DkReadOnlyGetter<string>(getProvider.Get()), property);
            EhDropdownTextObserver textObserver = new(observers, observers.Count, dropdownConfig.ItemTextColor, dropdownConfig.SelectedItemTextColor,
                textGetter, binding, modalInteractable);
            IOgInteractableElement<IOgVisualElement> interactable =
                BuildDropdownItem(getProvider, observers.Count, selected, textGetter, textEventHandler, textObserver, provider);
            observers.Add(textObserver);
            observers[selected.Get()].Update(false);
            return interactable;
        });
    }
}