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
public class EhMainWindowBuilder(EhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseDraggableBuilder draggableBuilder)
{
    public IOgContainer<IOgElement> Build(Texture2D texture, out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out IOgContainer<IOgElement> toolbarContainer, out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter)
    {
        EhMainWindowConfig windowConfig = provider.MainWindowConfig;
        IOgDraggableElement<IOgElement> window = draggableBuilder.Build("MainWindow", new OgScriptableBuilderProcess<OgDraggableBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(windowConfig.Width, windowConfig.Height));
        }));
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build("MainWindowSourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(windowConfig.Width, windowConfig.Height));
            }));
        #region separators
        float tabButtonsContainerHeight = windowConfig.Height - windowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                          (windowConfig.ToolbarContainerOffset * 2);
        float   tabContainerX   = provider.TabButtonConfig.TabButtonSize + (provider.SeparatorOffset * 2) + (windowConfig.TabButtonsContainerOffset * 2);
        float   containerY      = windowConfig.ToolbarContainerHeight + windowConfig.ToolbarContainerOffset;
        float   xOffset         = tabContainerX - windowConfig.TabButtonsContainerOffset;
        Vector4 separatorBorder = new(provider.SeparatorBorder, provider.SeparatorBorder, provider.SeparatorBorder, provider.SeparatorBorder);
        OgTextureElement tabSeparator = backgroundBuilder.Build("TabSeparator", provider.SeparatorColor, provider.SeparatorSize,
            windowConfig.Height - containerY - (provider.SeparatorOffset * 2), xOffset, windowConfig.ToolbarContainerHeight + provider.SeparatorOffset,
            separatorBorder);
        OgTextureElement subTabSeparator = backgroundBuilder.Build("SubTabSeparator", provider.SeparatorColor,
            windowConfig.Width - xOffset - (provider.SeparatorOffset * 2), provider.SeparatorSize, xOffset + provider.SeparatorOffset,
            windowConfig.ToolbarContainerHeight, separatorBorder);
        OgTextureElement logoBottomSeparator = backgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorColor,
            xOffset - (provider.SeparatorOffset * 2), provider.SeparatorSize, provider.SeparatorOffset, windowConfig.ToolbarContainerHeight,
            separatorBorder);
        OgTextureElement logoRightSeparator = backgroundBuilder.Build("LogoRightSeparator", provider.SeparatorColor, provider.SeparatorSize,
            windowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2), xOffset, provider.SeparatorOffset, separatorBorder);
        OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorThumbGetter = null!;
        OgTextureElement tabSeparatorThumb = backgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorThumbColor, provider.SeparatorSize * 3, 0,
            xOffset - provider.SeparatorSize, containerY + windowConfig.ToolbarContainerOffset, separatorBorder, context =>
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
        toolbarContainer = containerBuilder.Build("MainWindowToolbarContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(windowConfig.Width - xOffset,
                       windowConfig.ToolbarContainerHeight + windowConfig.ToolbarContainerOffset))
                   .SetOption(new OgMarginTransformerOption(xOffset, windowConfig.ToolbarContainerOffset));
        }));
        tabButtonsContainer = containerBuilder.Build("MainWindowTabButtonsContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabButtonConfig.TabButtonSize, tabButtonsContainerHeight))
                   .SetOption(new OgMarginTransformerOption(windowConfig.TabButtonsContainerOffset, containerY + windowConfig.ToolbarContainerOffset));
        }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options
              .SetOption(new OgSizeTransformerOption((provider.TabConfig.TabContainerWidth * 2) + (provider.TabButtonConfig.TabButtonOffset * 3),
                  tabButtonsContainerHeight)).SetOption(new OgMarginTransformerOption(tabContainerX, containerY + windowConfig.ToolbarContainerOffset));
        tabContainer          = new OgInteractableElement<IOgElement>("MainWindowTabContainer", eventProvider, getter);
        getter.LayoutCallback = tabContainer;
        sourceContainer.Add(tabContainer);
        sourceContainer.Add(tabButtonsContainer);
        window.Add(backgroundBuilder.Build("MainWindowBackground", windowConfig.BackgroundColorProperty, windowConfig.Width, windowConfig.Height, 0, 0,
            new(windowConfig.WindowBorderRadius, windowConfig.WindowBorderRadius, windowConfig.WindowBorderRadius, windowConfig.WindowBorderRadius)));
        IOgContainer<IOgElement> logoContainer = containerBuilder.Build("MainWindowLogoContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(xOffset, windowConfig.ToolbarContainerHeight))
                       .SetOption(new OgMarginTransformerOption(provider.SeparatorOffset / 2));
            }));
        logoContainer.Add(backgroundBuilder.Build("MainWindowLogo", windowConfig.LogoColor, windowConfig.LogoSize, windowConfig.LogoSize, 0, 0, new(),
            context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleCenter));
            }, null, new(), texture));
        window.Add(logoContainer);
        window.Add(sourceContainer);
        window.Add(toolbarContainer);
        return window;
    }
}