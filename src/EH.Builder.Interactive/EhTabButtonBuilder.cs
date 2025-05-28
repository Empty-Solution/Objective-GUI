using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Observing;
using EH.Builder.Options;
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
public class EhTabButtonBuilder(EhOptionsProvider provider)
{
    private static readonly List<EhBaseTabObserver> observers           = [];
    private readonly        EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly        EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly        EhInternalToggleBuilder m_ToggleBuilder     = new();
    public IOgToggle<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> tabContainer)
    {
        EhTabButtonOption buttonOption = provider.TabButtonOption;
        float tabContainerHeight = provider.WindowOption.Height - provider.WindowOption.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.WindowOption.ToolbarContainerOffset * 2);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? buttonOption.InteractColor.Get() : buttonOption.ButtonColor.Get();
        });
        OgEventHandlerProvider eventHandler = new();
        OgAnimationColorGetter getter       = new(eventHandler);
        tabContainer = m_ContainerBuilder.Build($"{name}SourceGlobalTabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption((provider.TabOption.TabContainerWidth * 2) + (provider.TabOption.TabContainerPadding * 3),
                    tabContainerHeight));
            }));
        OgTextureElement image = m_BackgroundBuilder.Build($"{name}Background", getter, buttonOption.TabButtonSize, buttonOption.TabButtonSize, 0, 0,
            new(buttonOption.TabButtonBorder, buttonOption.TabButtonBorder, buttonOption.TabButtonBorder, buttonOption.TabButtonBorder), context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                getter.Speed                  = provider.AnimationSpeed;
                backgroundObserver.Getter     = getter;
                getter.RenderCallback         = context.RectGetProvider;
                eventHandler.Register(getter);
            }, eventHandler, new(), texture);
        EhTabObserver tabObserver = new(observers, source, tabContainer, buttonOption.TabButtonSize, separatorSelectorGetter);
        IOgToggle<IOgVisualElement> button = m_ToggleBuilder.Build($"{name}Button", new DkObservableProperty<bool>(new DkObservable<bool>([]), false),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.ValueProvider.AddObserver(backgroundObserver);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(buttonOption.TabButtonSize, buttonOption.TabButtonSize))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, buttonOption.TabButtonOffset));
                tabObserver.RectGetter         = context.RectGetProvider;
                tabObserver.LinkedInteractable = context.Element;
                context.ValueProvider.AddObserver(tabObserver);
                context.ValueProvider.Set(false);
            }));
        button.Add(image);
        return button;
    }
}