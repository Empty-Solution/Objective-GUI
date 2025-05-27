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
using OG.Transformer.Options;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive;
public abstract class EhBaseDropdownBuilder(IEhVisualOption visual)
{
    protected readonly EhBackgroundBuilder                m_BackgroundBuilder        = new();
    protected readonly EhInternalButtonBuilder            m_ButtonBuilder            = new();
    protected readonly EhContainerBuilder                 m_ContainerBuilder         = new();
    protected readonly EhInternalModalInteractableBuilder m_ModalInteractableBuilder = new();
    protected readonly EhOptionsProvider                  m_OptionsProvider          = new();
    protected readonly EhTextBuilder                      m_TextBuilder              = new(visual);
    public IOgContainer<IOgElement> Build(string name, IDkProperty<int> selected, string[] values) => Build(name, selected, values, m_OptionsProvider);
    public IOgContainer<IOgElement> Build(string name, IDkProperty<int> selected, string[] values, EhOptionsProvider provider)
    {
        EhDropdownOption option = provider.DropdownOption;
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(provider.InteractableElementOption.Width, provider.InteractableElementOption.Height))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, provider.InteractableElementOption.VerticalPadding))
                       .SetOption(new OgMarginTransformerOption(provider.InteractableElementOption.HorizontalPadding));
            }));
        container.Add(m_TextBuilder.BuildStaticText($"{name}NameText", option.TextColor, name, option.TextNameFontSize, option.TextNameAlignment,
            provider.InteractableElementOption.Width - option.Width, provider.InteractableElementOption.Height));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> backgroundObserver = new((getter, value) =>
        {
            getter.SetTime();
            Rect rect = getter.TargetModifier;
            rect.height           = value ? ((option.ModalItemHeight + option.ModalItemPadding) * values.Length) + option.ModalItemPadding : 0;
            getter.TargetModifier = rect;
        });
        OgTextureElement background = m_BackgroundBuilder.Build($"{name}Background", option.BackgroundColor, option.Width, option.Height,
            provider.InteractableElementOption.Width - option.Width, 0, new(option.Border, option.Border, option.Border, option.Border), context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
                backgroundObserver.Getter     = context.RectGetProvider;
                context.RectGetProvider.Speed = provider.AnimationSpeed;
            });
        container.Add(background);
        DkObservableProperty<string> property = new(new DkObservable<string>([]), values[selected.Get()]);
        OgTextElement text = m_TextBuilder.BuildBindableText($"{name}Text", option.TextColor, property, option.TextFontSize, option.TextAlignment,
            option.Width, option.Height, provider.InteractableElementOption.Width - option.Width, 0, context =>
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
        IOgModalInteractable<IOgElement> button = m_ModalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height))
                       .SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft))
                       .SetOption(new OgMarginTransformerOption(provider.InteractableElementOption.Width - option.Width));
                context.Element.IsInteractingObserver?.AddObserver(backgroundObserver);
                context.Element.IsInteractingObserver?.AddObserver(observer);
                context.Element.IsInteractingObserver?.Notify(false);
            }));
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(option.Width, (option.ModalItemHeight + option.ModalItemPadding) * values.Length))
                       .SetOption(new OgMarginTransformerOption(0, option.Height - option.ModalItemPadding));
            }));
        button.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, 0, option.Width, (option.ModalItemHeight + option.ModalItemPadding) * values.Length))));
        List<EhDropdownTextObserver> observers = [];
        for(int i = 0; i < values.Length; i++) sourceContainer.Add(BuildDropdownItem(values[i], i, selected, property, observers, button, provider));
        observers[selected.Get()].Update(false);
        button.Add(sourceContainer);
        container.Add(button);
        return container;
    }
    protected abstract IOgInteractableElement<IOgVisualElement> BuildDropdownItem(string name, int index, IDkProperty<int> selected,
        DkObservableProperty<string> property, List<EhDropdownTextObserver> observers, IOgModalInteractable<IOgElement> interactable,
        EhOptionsProvider provider);
}