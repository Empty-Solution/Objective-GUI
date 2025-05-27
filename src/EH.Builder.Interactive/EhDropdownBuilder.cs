using DK.Binding.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Observing;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhDropdownBuilder(IEhVisualOption visual) : EhBaseDropdownBuilder(visual)
{
    protected override IOgContainer<IOgVisualElement> BuildDropdownItem(string name, int index, IDkProperty<int> selected,
        DkObservableProperty<string> property, List<EhDropdownTextObserver> observers, IOgModalInteractable<IOgElement> interactable,
        EhOptionsProvider provider)
    {
        EhDropdownOption           option   = provider.DropdownOption;
        DkScriptableObserver<bool> observer = new();
        observer.OnUpdate += state =>
        {
            selected.Set(index);
        };
        DkBinding<string>      binding          = new(new DkReadOnlyGetter<string>(name), property);
        OgEventHandlerProvider textEventHandler = new();
        OgAnimationColorGetter textGetter       = new(textEventHandler);
        EhDropdownTextObserver textObserver = new(observers, observers.Count, option.ItemTextColor, option.SelectedItemTextColor, textGetter, binding,
            interactable);
        observers.Add(textObserver);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.ItemBackgroundHoverColor.Get() : option.ItemBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        backgroundHoverObserver.Getter = backgroundGetter;
        OgTextureElement background = m_BackgroundBuilder.Build($"{name}Background", backgroundGetter, (option.Width * 0.9f) - (option.Width * 0.05f),
            option.ModalItemHeight, option.Width * 0.05f, 0, new(option.Border, option.Border, option.Border, option.Border), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        background.ZOrder = 9999;
        OgTextElement text = m_TextBuilder.BuildStaticText($"{name}Text", textGetter, name, option.ItemTextFontSize, option.ItemTextAlignment,
            (option.Width * 0.9f) - (option.Width * 0.05f), option.ModalItemHeight, option.Width * 0.05f, 0, context =>
            {
                textGetter.Speed          = provider.AnimationSpeed;
                textGetter.RenderCallback = context.RectGetProvider;
                textEventHandler.Register(textGetter);
            }, textEventHandler);
        text.ZOrder = 9999;
        IOgInteractableElement<IOgVisualElement> button = m_ButtonBuilder.Build(name, new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption((option.Width * 0.9f) - (option.Width * 0.05f), option.ModalItemHeight))
                   .SetOption(new OgMarginTransformerOption(option.Width * 0.05f))
                   .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, option.ModalItemPadding));
            context.Element.IsInteractingObserver?.AddObserver(textObserver);
            context.Element.IsInteractingObserver?.AddObserver(observer);
            context.Element.IsInteractingObserver?.Notify(selected.Get() == index);
            context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
            context.Element.IsHoveringObserver?.Notify(false);
        }));
        button.Add(background);
        button.Add(text);
        return button;
    }
}