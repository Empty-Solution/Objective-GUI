using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Observing;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
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
public class EhSubTabButtonBuilder(IEhVisualOption option)
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly List<EhBaseTabObserver> m_Observers         = [];
    private readonly EhOptionsProvider       m_OptionsProvider   = new();
    private readonly EhTextBuilder           m_TextBuilder       = new(option);
    private readonly EhInternalToggleBuilder m_ToggleBuilder     = new();
    public IOgContainer<IOgVisualElement> Build(string name, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> builtTabContainer) =>
        Build(name, separatorSelectorGetter, source, out builtTabContainer, m_OptionsProvider);
    private IOgToggle<IOgVisualElement> Build(string name, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> builtTabContainer, EhOptionsProvider provider)
    {
        EhSubTabButtonOption option             = provider.SubTabButtonOption;
        float                tabContainerHeight = provider.WindowOption.WindowHeight - provider.WindowOption.ToolbarContainerHeight;
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? option.BackgroundInteractColor.Get() : option.BackgroundColor.Get();
        });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? option.BackgroundHoverColor.Get() : option.BackgroundColor.Get();
        });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> textHoverObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? option.TextHoverColor.Get() : option.TextColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgEventHandlerProvider textEventHandler       = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgAnimationColorGetter textGetter             = new(textEventHandler);
        builtTabContainer = m_ContainerBuilder.Build($"{name}TabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, tabContainerHeight));
        }));
        OgTextureElement background = m_BackgroundBuilder.Build($"{name}Background", backgroundGetter, option.Width, option.Height, 0, 0, option.Border,
            context =>
            {
                context.RectGetProvider.Speed   = provider.AnimationSpeed;
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundObserver.Getter       = backgroundGetter;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                textEventHandler.Register(backgroundGetter);
            }, textEventHandler);
        OgTextElement text = m_TextBuilder.BuildStaticText($"{name}Text", textGetter, name, option.FontSize, option.Alignment, option.Width, option.Height,
            0, 0, context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(
                    new OgInvertedFlexiblePositionTransformerOption(EOgOrientation.HORIZONTAL, option.Padding));
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                textGetter.Speed              = provider.AnimationSpeed;
                textHoverObserver.Getter      = textGetter;
                textGetter.RenderCallback     = context.RectGetProvider;
                backgroundEventHandler.Register(textGetter);
            }, textEventHandler);
        EhSubTabObserver tabObserver = new(m_Observers, source, builtTabContainer, option.Height, separatorSelectorGetter);
        IOgToggle<IOgVisualElement> button = m_ToggleBuilder.Build($"{name}Button", new DkObservableProperty<bool>(new DkObservable<bool>([]), false),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.ValueProvider.AddObserver(backgroundObserver);
                context.ValueProvider.AddObserver(backgroundHoverObserver);
                context.ValueProvider.AddObserver(textHoverObserver);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height));
                tabObserver.RectGetter         = context.RectGetProvider;
                tabObserver.LinkedInteractable = context.Element;
                context.ValueProvider.AddObserver(tabObserver);
                context.ValueProvider.Set(false);
            }));
        button.Add(background);
        button.Add(text);
        return button;
    }
}