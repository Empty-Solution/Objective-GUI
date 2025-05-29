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
public class EhWindowBuilder(EhConfigProvider provider, EhBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder, EhInternalDraggableBuilder draggableBuilder)
{
    public IOgContainer<IOgElement> Build(out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out IOgContainer<IOgElement> toolbarContainer, out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter, float x = 0,
        float y = 0)
    {
        EhWindowConfig option = provider.WindowConfig;
        IOgDraggableElement<IOgElement> window = draggableBuilder.Build("MainWindow", new OgScriptableBuilderProcess<OgDraggableBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height))
                   .SetOption(new OgMarginTransformerOption(x, y));
        }));
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build("MainWindowTabButtonsContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height));
            }));
        #region separators
        float tabButtonsContainerHeight =
            option.Height - option.ToolbarContainerHeight - (provider.SeparatorOffset * 2) - (option.ToolbarContainerOffset * 2);
        float   tabContainerX   = provider.TabButtonConfig.TabButtonSize + (provider.SeparatorOffset * 2) + (option.TabButtonsContainerOffset * 2);
        float   containerY      = option.ToolbarContainerHeight + option.ToolbarContainerOffset;
        float   xOffset         = tabContainerX - option.TabButtonsContainerOffset;
        Vector4 separatorBorder = new(provider.SeparatorBorder, provider.SeparatorBorder, provider.SeparatorBorder, provider.SeparatorBorder);
        OgTextureElement tabSeparator = backgroundBuilder.Build("TabSeparator", provider.SeparatorColor, provider.SeparatorWidth,
            option.Height - containerY - (provider.SeparatorOffset * 2), xOffset, option.ToolbarContainerHeight + provider.SeparatorOffset,
            separatorBorder);
        OgTextureElement subTabSeparator = backgroundBuilder.Build("SubTabSeparator", provider.SeparatorColor,
            option.Width - xOffset - (provider.SeparatorOffset * 2), provider.SeparatorWidth, xOffset + provider.SeparatorOffset,
            option.ToolbarContainerHeight, separatorBorder);
        OgTextureElement logoBottomSeparator = backgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorColor,
            xOffset - (provider.SeparatorOffset * 2), provider.SeparatorWidth, provider.SeparatorOffset, option.ToolbarContainerHeight, separatorBorder);
        OgTextureElement logoRightSeparator = backgroundBuilder.Build("LogoRightSeparator", provider.SeparatorColor, provider.SeparatorWidth,
            option.ToolbarContainerHeight - (provider.SeparatorOffset * 2), xOffset, provider.SeparatorOffset, separatorBorder);
        OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorThumbGetter = null!;
        OgTextureElement tabSeparatorThumb = backgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorThumbColor, provider.SeparatorWidth * 3, 0,
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
        toolbarContainer = containerBuilder.Build("MainWindowToolbarContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width - xOffset, option.ToolbarContainerHeight))
                   .SetOption(new OgMarginTransformerOption(xOffset, option.ToolbarContainerOffset));
        }));
        tabButtonsContainer = containerBuilder.Build("MainWindowTabButtonsContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabButtonConfig.TabButtonSize, tabButtonsContainerHeight))
                   .SetOption(new OgMarginTransformerOption(option.TabButtonsContainerOffset, containerY + option.ToolbarContainerOffset));
        }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options
              .SetOption(new OgSizeTransformerOption((provider.TabConfig.TabContainerWidth * 2) + (provider.TabButtonConfig.TabButtonOffset * 3),
                  tabButtonsContainerHeight)).SetOption(new OgMarginTransformerOption(tabContainerX, containerY + option.ToolbarContainerOffset));
        tabContainer          = new OgInteractableElement<IOgElement>("MainWindowTabContainer", eventProvider, getter);
        getter.LayoutCallback = tabContainer;
        sourceContainer.Add(tabContainer);
        sourceContainer.Add(tabButtonsContainer);
        window.Add(backgroundBuilder.Build("MainWindowBackground", option.BackgroundColorProperty, option.Width, option.Height, 0, 0,
            new(option.WindowBorderRadius, option.WindowBorderRadius, option.WindowBorderRadius, option.WindowBorderRadius)));
        window.Add(sourceContainer);
        return window;
    }
}