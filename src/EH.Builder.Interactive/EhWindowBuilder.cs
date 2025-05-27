using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Event;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhWindowBuilder
{
    private readonly EhBackgroundBuilder        m_BackgroundBuilder = new();
    private readonly EhContainerBuilder         m_ContainerBuilder  = new();
    private readonly EhInternalDraggableBuilder m_DraggableBuilder  = new();
    private readonly EhOptionsProvider          m_OptionsProvider   = new();
    public IOgContainer<IOgElement> Build(out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out IOgContainer<IOgElement> toolbarContainer, out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter, float x = 0,
        float y = 0) =>
        Build(y, x, out tabButtonsContainer, out tabContainer, out toolbarContainer, out tabSeparatorSelectorGetter, m_OptionsProvider);
    private IOgContainer<IOgElement> Build(float x, float y, out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out IOgContainer<IOgElement> toolbarContainer, out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter,
        EhOptionsProvider provider)
    {
        EhWindowOption option = provider.WindowOption;
        IOgDraggableElement<IOgElement> window = m_DraggableBuilder.Build("MainWindow", new OgScriptableBuilderProcess<OgDraggableBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height))
                   .SetOption(new OgMarginTransformerOption(x, y));
        }));
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build("MainWindowTabButtonsContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height));
            }));
        #region separators
        float tabButtonsContainerHeight =
            option.Height - option.ToolbarContainerHeight - (provider.SeparatorOffset * 2) - (option.ToolbarContainerOffset * 2);
        float   tabContainerX   = provider.TabOption.TabButtonSize + (provider.SeparatorOffset * 2) + (option.TabButtonsContainerOffset * 2);
        float   containerY      = option.ToolbarContainerHeight + option.ToolbarContainerOffset;
        float   xOffset         = tabContainerX - option.TabButtonsContainerOffset;
        Vector4 separatorBorder = new(provider.SeparatorBorder, provider.SeparatorBorder, provider.SeparatorBorder, provider.SeparatorBorder);
        OgTextureElement tabSeparator = m_BackgroundBuilder.Build("TabSeparator", provider.SeparatorColor, provider.SeparatorWidth,
            option.Height - containerY - (provider.SeparatorOffset * 2), xOffset, option.ToolbarContainerHeight + provider.SeparatorOffset,
            separatorBorder);
        OgTextureElement subTabSeparator = m_BackgroundBuilder.Build("SubTabSeparator", provider.SeparatorColor,
            option.Width - xOffset - (provider.SeparatorOffset * 2), provider.SeparatorWidth, xOffset + provider.SeparatorOffset,
            option.ToolbarContainerHeight, separatorBorder);
        OgTextureElement logoBottomSeparator = m_BackgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorColor,
            xOffset - (provider.SeparatorOffset * 2), provider.SeparatorWidth, provider.SeparatorOffset, option.ToolbarContainerHeight, separatorBorder);
        OgTextureElement logoRightSeparator = m_BackgroundBuilder.Build("LogoRightSeparator", provider.SeparatorColor, provider.SeparatorWidth,
            option.ToolbarContainerHeight - (provider.SeparatorOffset * 2), xOffset, provider.SeparatorOffset, separatorBorder);
        OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorThumbGetter = null!;
        OgTextureElement tabSeparatorThumb = m_BackgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorThumbColor, provider.SeparatorWidth * 3, 0,
            xOffset - provider.SeparatorWidth, containerY + option.ToolbarContainerOffset, separatorBorder, context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                tabSeparatorThumbGetter       = context.RectGetProvider;
            });
        sourceContainer.Add(tabSeparator);
        sourceContainer.Add(subTabSeparator);
        sourceContainer.Add(logoBottomSeparator);
        sourceContainer.Add(logoRightSeparator);
        sourceContainer.Add(tabSeparatorThumb);
        tabSeparatorSelectorGetter = tabSeparatorThumbGetter;
        #endregion
        toolbarContainer = m_ContainerBuilder.Build("MainWindowToolbarContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width - xOffset, option.ToolbarContainerHeight))
                   .SetOption(new OgMarginTransformerOption(xOffset, option.ToolbarContainerOffset));
        }));
        tabButtonsContainer = m_ContainerBuilder.Build("MainWindowTabButtonsContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabOption.TabButtonSize, tabButtonsContainerHeight))
                   .SetOption(new OgMarginTransformerOption(option.TabButtonsContainerOffset, containerY + option.ToolbarContainerOffset));
        }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options
              .SetOption(new OgSizeTransformerOption((provider.TabOption.TabContainerWidth * 2) + (provider.TabOption.TabButtonOffset * 3),
                  tabButtonsContainerHeight)).SetOption(new OgMarginTransformerOption(tabContainerX, containerY + option.ToolbarContainerOffset));
        tabContainer          = new OgInteractableElement<IOgElement>("MainWindowTabContainer", eventProvider, getter);
        getter.LayoutCallback = tabContainer;
        sourceContainer.Add(tabContainer);
        sourceContainer.Add(tabButtonsContainer);
        window.Add(m_BackgroundBuilder.Build("MainWindowBackground", option.BackgroundColorProperty, option.Width, option.Height, 0, 0,
            new(option.WindowBorderRadius, option.WindowBorderRadius, option.WindowBorderRadius, option.WindowBorderRadius)));
        window.Add(sourceContainer);
        return window;
    }
}