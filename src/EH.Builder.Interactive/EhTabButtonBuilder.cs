using DK.Getting.Generic;
using DK.Observing.Generic;
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
public class EhTabButtonBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseToggleBuilder toggleBuilder)
{
    private readonly List<EhTabObserver> m_Observers = [];
    public IOgToggle<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> sourceTabContainer, IOgContainer<IOgElement> sourceToolbarContainer, out IOgContainer<IOgElement> builtTabContainer,
        out IOgContainer<IOgElement> builtToolbarContainer)
    {
        EhTabButtonConfig tabButtonConfig = provider.TabButtonConfig;
        float tabContainerHeight = provider.MainWindowConfig.Height - provider.MainWindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.MainWindowConfig.ToolbarContainerOffset * 2);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? tabButtonConfig.InteractColor.Get() : tabButtonConfig.ButtonColor.Get();
        });
        float tabContainerX = provider.TabButtonConfig.TabButtonSize + (provider.SeparatorOffset * 2) +
                              (provider.MainWindowConfig.TabButtonsContainerOffset * 2);
        float xOffset = tabContainerX - provider.MainWindowConfig.TabButtonsContainerOffset;
        builtToolbarContainer = containerBuilder.Build($"{name}SourceToolbarContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.MainWindowConfig.Width - xOffset,
                provider.MainWindowConfig.ToolbarContainerHeight + provider.MainWindowConfig.ToolbarContainerOffset));
        }));
        builtTabContainer = containerBuilder.Build($"{name}SourceTabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(
                new OgSizeTransformerOption((provider.TabConfig.Width * 2) + (provider.TabConfig.TabContainerPadding * 3), tabContainerHeight));
        }));
        OgEventHandlerProvider eventHandler = new();
        OgAnimationColorGetter getter       = new(eventHandler);
        OgTextureElement image = backgroundBuilder.Build($"{name}Background", getter, tabButtonConfig.TabButtonSize, tabButtonConfig.TabButtonSize, 0, 0,
            new(tabButtonConfig.TabButtonBorder, tabButtonConfig.TabButtonBorder, tabButtonConfig.TabButtonBorder, tabButtonConfig.TabButtonBorder),
            context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                getter.Speed                  = provider.AnimationSpeed;
                backgroundObserver.Getter     = getter;
                getter.RenderCallback         = context.RectGetProvider;
                eventHandler.Register(getter);
            }, eventHandler, new(), texture);
        EhTabObserver tabObserver = new(m_Observers, sourceTabContainer, builtTabContainer, sourceToolbarContainer, builtToolbarContainer,
            tabButtonConfig.TabButtonSize, separatorSelectorGetter);
        m_Observers.Add(tabObserver);
        IOgToggle<IOgVisualElement> button = toggleBuilder.Build($"{name}Button", new DkObservableProperty<bool>(new DkObservable<bool>([]), false),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.ValueProvider.AddObserver(backgroundObserver);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(tabButtonConfig.TabButtonSize, tabButtonConfig.TabButtonSize))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, tabButtonConfig.TabButtonOffset));
                tabObserver.RectGetter         = context.RectGetProvider;
                tabObserver.LinkedInteractable = context.Element;
                context.ValueProvider.AddObserver(tabObserver);
                context.ValueProvider.Set(false);
            }));
        button.Add(image);
        return button;
    }
}