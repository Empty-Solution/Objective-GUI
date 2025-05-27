using DK.Getting.Abstraction.Generic;
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
public class EhTabBuilder
{
    private static readonly List<EhBaseTabObserver> observers           = [];
    private readonly        EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly        EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly        EhOptionsProvider       m_OptionsProvider   = new();
    private readonly        EhInternalToggleBuilder m_ToggleBuilder     = new();
    public IOgContainer<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> builtLeftTabContainer, out IOgContainer<IOgElement> builtRightTabContainer) =>
        Build(name, texture, separatorSelectorGetter, source, out builtLeftTabContainer, out builtRightTabContainer, m_OptionsProvider);
    private IOgToggle<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> builtLeftTabContainer, out IOgContainer<IOgElement> builtRightTabContainer,
        EhOptionsProvider provider)
    {
        EhTabOption option = provider.TabOption;
        float tabContainerHeight = provider.WindowOption.Height - provider.WindowOption.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.WindowOption.ToolbarContainerOffset * 2);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? option.InteractColor.Get() : option.ButtonColor.Get();
        });
        OgEventHandlerProvider eventHandler = new();
        OgAnimationColorGetter getter       = new(eventHandler);
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}SourceGlobalTabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption((option.TabContainerWidth * 2) + (option.TabContainerPadding * 3),
                    tabContainerHeight));
            }));
        sourceContainer.Add(BuildTabContainer(name, out builtLeftTabContainer, option.TabContainerWidth, tabContainerHeight, option.TabButtonOffset,
            option.BackgroundBorder, option.BackgroundColor));
        sourceContainer.Add(BuildTabContainer(name, out builtRightTabContainer, option.TabContainerWidth, tabContainerHeight,
            (option.TabButtonOffset * 2) + option.TabContainerWidth, option.BackgroundBorder, option.BackgroundColor));
        OgTextureElement image = m_BackgroundBuilder.Build($"{name}Background", getter, option.TabButtonSize, option.TabButtonSize, 0, 0,
            new(option.TabButtonBorder, option.TabButtonBorder, option.TabButtonBorder, option.TabButtonBorder), context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                getter.Speed                  = provider.AnimationSpeed;
                backgroundObserver.Getter     = getter;
                getter.RenderCallback         = context.RectGetProvider;
                eventHandler.Register(getter);
            }, eventHandler, new(), texture);
        EhTabObserver tabObserver = new(observers, source, sourceContainer, option.TabButtonSize, separatorSelectorGetter);
        IOgToggle<IOgVisualElement> button = m_ToggleBuilder.Build($"{name}Button", new DkObservableProperty<bool>(new DkObservable<bool>([]), false),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.ValueProvider.AddObserver(backgroundObserver);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.TabButtonSize, option.TabButtonSize))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, option.TabButtonOffset));
                tabObserver.RectGetter         = context.RectGetProvider;
                tabObserver.LinkedInteractable = context.Element;
                context.ValueProvider.AddObserver(tabObserver);
                context.ValueProvider.Set(false);
            }));
        button.Add(image);
        return button;
    }
    private IOgContainer<IOgElement> BuildTabContainer(string name, out IOgContainer<IOgElement> builtContainer, float width, float height, float x,
        float border, IDkGetProvider<Color> colorGetter)
    {
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}SourceTabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x));
            }));
        builtContainer = m_ContainerBuilder.Build($"{name}TabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height));
        }));
        sourceContainer.Add(m_BackgroundBuilder.Build($"{name}TabContainerBackground", colorGetter, width, height, 0, 0,
            new(border, border, border, border)));
        sourceContainer.Add(builtContainer);
        return sourceContainer;
    }
}