using DK.Getting.Generic;
using DK.Observing.Generic;
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
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhTabBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseToggleBuilder toggleBuilder)
{
    private readonly List<EhTabObserver> m_Observers = [];
    public IEhTab Build(string name, Texture2D texture, IEhWindow window)
    {
        EhTabButtonConfig tabButtonConfig = provider.TabButtonConfig;
        float tabContainerHeight = provider.MainWindowConfig.Height - provider.MainWindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.MainWindowConfig.ToolbarContainerOffset * 2);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? tabButtonConfig.InteractColor.Get() : tabButtonConfig.ButtonColor.Get();
        });
        float tabContainerX = provider.TabButtonConfig.Width + (provider.SeparatorOffset * 2) + (provider.MainWindowConfig.TabButtonsContainerOffset * 2);
        float xOffset       = tabContainerX - provider.MainWindowConfig.TabButtonsContainerOffset;
        IOgContainer<IOgElement> builtToolbarContainer = containerBuilder.Build($"{name}SourceToolbarContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.MainWindowConfig.Width - xOffset,
                    provider.MainWindowConfig.ToolbarContainerHeight + provider.MainWindowConfig.ToolbarContainerOffset));
            }));
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> builtTabContainer = containerBuilder.Build($"{name}SourceTabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(
                    new OgSizeTransformerOption((provider.TabGroupConfig.Width * 2) + (provider.TabGroupConfig.TabContainerPadding * 3),
                        tabContainerHeight));
                optionsContainer = context.RectGetProvider.Options;
            }));
        OgEventHandlerProvider eventHandler = new();
        OgAnimationColorGetter getter       = new(eventHandler);
        OgTextureElement image = backgroundBuilder.Build($"{name}Background", getter, tabButtonConfig.Width, tabButtonConfig.Height, 0, 0,
            new(tabButtonConfig.TabButtonBorder, tabButtonConfig.TabButtonBorder, tabButtonConfig.TabButtonBorder, tabButtonConfig.TabButtonBorder),
            context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                getter.Speed                  = provider.AnimationSpeed;
                backgroundObserver.Getter     = getter;
                getter.RenderCallback         = context.RectGetProvider;
                eventHandler.Register(getter);
            }, eventHandler, new(), texture);
        EhTabObserver tabObserver = new(m_Observers, window.TabContainer, builtTabContainer, window.ToolbarContainer, builtToolbarContainer,
            tabButtonConfig.Height, window.TabSeparatorSelectorGetter);
        m_Observers.Add(tabObserver);
        IOgToggle<IOgVisualElement> button = toggleBuilder.Build($"{name}Button", new DkObservableProperty<bool>(new DkObservable<bool>([]), false),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.ValueProvider.AddObserver(backgroundObserver);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(tabButtonConfig.Width, tabButtonConfig.Height))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, tabButtonConfig.TabButtonOffset));
                tabObserver.RectGetter         = context.RectGetProvider;
                tabObserver.LinkedInteractable = context.Element;
                context.ValueProvider.AddObserver(tabObserver);
                context.ValueProvider.Set(false);
            }));
        button.Add(image);
        return new EhTab(button, builtTabContainer, builtToolbarContainer, optionsContainer);
    }
}