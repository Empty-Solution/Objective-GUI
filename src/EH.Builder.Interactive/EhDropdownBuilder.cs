using DK.Binding.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Observing;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhDropdownBuilder(EhConfigProvider provider, EhBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhInternalButtonBuilder buttonBuilder, EhInternalModalInteractableBuilder modalInteractableBuilder, EhTextBuilder textBuilder)
{
    public IOgContainer<IOgElement> Build(string name, IDkProperty<int> selected, string[] values)
    {
        EhDropdownConfig dropdownConfig = provider.DropdownConfig;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, provider.InteractableElementConfig.VerticalPadding))
                       .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding));
            }));
        container.Add(textBuilder.BuildStaticText($"{name}NameText", dropdownConfig.TextColor, name, dropdownConfig.TextNameFontSize,
            dropdownConfig.TextNameAlignment, provider.InteractableElementConfig.Width - dropdownConfig.Width, provider.InteractableElementConfig.Height));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> backgroundObserver = new((getter, value) =>
        {
            getter.SetTime();
            Rect rect = getter.TargetModifier;
            rect.height = value ? ((dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Length) + dropdownConfig.ModalItemPadding
                              : 0;
            getter.TargetModifier = rect;
        });
        OgTextureElement background = backgroundBuilder.Build($"{name}Background", dropdownConfig.BackgroundColor, dropdownConfig.Width,
            dropdownConfig.Height, provider.InteractableElementConfig.Width - dropdownConfig.Width, 0,
            new(dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border), context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
                backgroundObserver.Getter     = context.RectGetProvider;
                context.RectGetProvider.Speed = provider.AnimationSpeed;
            });
        container.Add(background);
        DkObservableProperty<string> property = new(new DkObservable<string>([]), values[selected.Get()]);
        OgTextElement text = textBuilder.BuildBindableText($"{name}Text", dropdownConfig.TextColor, property, dropdownConfig.TextFontSize,
            dropdownConfig.TextAlignment, dropdownConfig.Width, dropdownConfig.Height, provider.InteractableElementConfig.Width - dropdownConfig.Width, 0,
            context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
            });
        container.Add(text);
        DkScriptableObserver<bool> observer = new();
        observer.OnUpdate += state =>
        {
            background.ZOrder = state ? 9999 : 0;
            text.ZOrder       = state ? 9999 : 0;
        };
        IOgModalInteractable<IOgElement> button = modalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(dropdownConfig.Width, dropdownConfig.Height))
                       .SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft))
                       .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.Width - dropdownConfig.Width));
                context.Element.IsInteractingObserver?.AddObserver(backgroundObserver);
                context.Element.IsInteractingObserver?.AddObserver(observer);
                context.Element.IsInteractingObserver?.Notify(false);
            }));
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(dropdownConfig.Width,
                           (dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Length))
                       .SetOption(new OgMarginTransformerOption(0, dropdownConfig.Height - dropdownConfig.ModalItemPadding));
            }));
        button.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, 0, dropdownConfig.Width,
                (dropdownConfig.ModalItemHeight + dropdownConfig.ModalItemPadding) * values.Length))));
        List<EhDropdownTextObserver> observers = [];
        for(int i = 0; i < values.Length; i++) sourceContainer.Add(BuildDropdownItem(values[i], i, selected, property, observers, button, provider));
        observers[selected.Get()].Update(false);
        button.Add(sourceContainer);
        container.Add(button);
        return container;
    }
    private IOgInteractableElement<IOgVisualElement> BuildDropdownItem(string name, int index, IDkProperty<int> selected,
        DkObservableProperty<string> property, List<EhDropdownTextObserver> observers, IOgModalInteractable<IOgElement> interactable,
        EhConfigProvider provider)
    {
        EhDropdownConfig           dropdownConfig = provider.DropdownConfig;
        DkScriptableObserver<bool> observer       = new();
        observer.OnUpdate += state =>
        {
            selected.Set(index);
        };
        DkBinding<string>      binding          = new(new DkReadOnlyGetter<string>(name), property);
        OgEventHandlerProvider textEventHandler = new();
        OgAnimationColorGetter textGetter       = new(textEventHandler);
        EhDropdownTextObserver textObserver = new(observers, observers.Count, dropdownConfig.ItemTextColor, dropdownConfig.SelectedItemTextColor,
            textGetter, binding, interactable);
        observers.Add(textObserver);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? dropdownConfig.ItemBackgroundHoverColor.Get() : dropdownConfig.ItemBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        backgroundHoverObserver.Getter = backgroundGetter;
        OgTextureElement background = backgroundBuilder.Build($"{name}Background", backgroundGetter,
            (dropdownConfig.Width * 0.9f) - (dropdownConfig.Width * 0.05f), dropdownConfig.ModalItemHeight, dropdownConfig.Width * 0.05f, 0,
            new(dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        background.ZOrder = 9999;
        OgTextElement text = textBuilder.BuildStaticText($"{name}Text", textGetter, name, dropdownConfig.ItemTextFontSize,
            dropdownConfig.ItemTextAlignment, (dropdownConfig.Width * 0.9f) - (dropdownConfig.Width * 0.05f), dropdownConfig.ModalItemHeight,
            dropdownConfig.Width * 0.05f, 0, context =>
            {
                textGetter.Speed          = provider.AnimationSpeed;
                textGetter.RenderCallback = context.RectGetProvider;
                textEventHandler.Register(textGetter);
            }, textEventHandler);
        text.ZOrder = 9999;
        IOgInteractableElement<IOgVisualElement> button = buttonBuilder.Build(name, new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption((dropdownConfig.Width * 0.9f) - (dropdownConfig.Width * 0.05f), dropdownConfig.ModalItemHeight))
                   .SetOption(new OgMarginTransformerOption(dropdownConfig.Width * 0.05f))
                   .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, dropdownConfig.ModalItemPadding));
            context.Element.IsInteractingObserver?.AddObserver(textObserver);
            context.Element.IsInteractingObserver?.AddObserver(observer);
            context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
            context.Element.IsHoveringObserver?.Notify(false);
        }));
        button.Add(background);
        button.Add(text);
        return button;
    }
}