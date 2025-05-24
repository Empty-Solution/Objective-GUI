using EH.Builder.Abstraction;
using EH.Builder.Interactive.Base;
using EH.Builder.Option;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhWindowBuilder : IEhWindowBuilder
{
    private readonly EhBackgroundBuilder        m_BackgroundBuilder = new();
    private readonly EhContainerBuilder         m_ContainerBuilder  = new();
    private readonly EhInternalDraggableBuilder m_DraggableBuilder  = new();
    private readonly EhOptionsProvider          m_OptionsProvider   = new();
    public IOgContainer<IOgElement> Build(out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter,
        out OgAnimationRectGetter<OgTransformerRectGetter> subTabSeparatorSelectorGetter, float x = 0, float y = 0) =>
        Build(y, x, out tabButtonsContainer, out tabContainer, out tabSeparatorSelectorGetter, out subTabSeparatorSelectorGetter, m_OptionsProvider);
    private IOgContainer<IOgElement> Build(float x, float y, out IOgContainer<IOgElement> tabButtonsContainer, out IOgContainer<IOgElement> tabContainer,
        out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter,
        out OgAnimationRectGetter<OgTransformerRectGetter> subTabSeparatorSelectorGetter, EhOptionsProvider provider)
    {
        EhWindowOption option = provider.WindowOption;
        IOgDraggableElement<IOgElement> window = m_DraggableBuilder.Build("MainWindow", new OgScriptableBuilderProcess<OgDraggableBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.WindowWidth, option.WindowHeight));
        }));
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build("TabButtonsContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.WindowWidth, option.WindowHeight))
                       .SetOption(new OgMarginTransformerOption(x, y));
            }));
        #region separators
        float tabButtonsContainerHeight = option.WindowHeight - option.ToolbarContainerHeight;
        float tabContainerX             = provider.TabButtonOption.TabButtonSize + (provider.SeparatorOffset * 2) + (option.TabButtonsContainerOffset * 2);
        float containerY                = option.ToolbarContainerHeight + option.ToolbarContainerOffset;
        float xOffset                   = tabContainerX - option.TabButtonsContainerOffset;
        OgTextureElement tabSeparator = m_BackgroundBuilder.Build("TabSeparator", provider.SeparatorColor, provider.SeparatorWidth,
            tabButtonsContainerHeight - (provider.SeparatorOffset * 2), xOffset, option.ToolbarContainerHeight + provider.SeparatorOffset,
            provider.SeparatorBorder);
        OgTextureElement subTabSeparator = m_BackgroundBuilder.Build("SubTabSeparator", provider.SeparatorColor,
            option.WindowWidth - xOffset - (provider.SeparatorOffset * 2), provider.SeparatorWidth, xOffset + provider.SeparatorOffset,
            option.ToolbarContainerHeight, provider.SeparatorBorder);
        OgTextureElement logoBottomSeparator = m_BackgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorColor,
            xOffset - (provider.SeparatorOffset * 2), provider.SeparatorWidth, provider.SeparatorOffset, option.ToolbarContainerHeight,
            provider.SeparatorBorder);
        OgTextureElement logoRightSeparator = m_BackgroundBuilder.Build("LogoRightSeparator", provider.SeparatorColor, provider.SeparatorWidth,
            option.ToolbarContainerHeight - (provider.SeparatorOffset * 2), xOffset, provider.SeparatorOffset, provider.SeparatorBorder);
        OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorThumbGetter = null!;
        OgTextureElement tabSeparatorThumb = m_BackgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorThumbColor, provider.SeparatorWidth * 3, 0,
            xOffset - provider.SeparatorWidth, containerY + option.ToolbarContainerOffset, provider.SeparatorBorder, context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                tabSeparatorThumbGetter       = context.RectGetProvider;
            });
        OgAnimationRectGetter<OgTransformerRectGetter> subTabSeparatorThumbGetter = null!;
        OgTextureElement subTabSeparatorThumb = m_BackgroundBuilder.Build("LogoBottomSeparator", provider.SeparatorThumbColor, 0,
            provider.SeparatorWidth * 3, 0, option.ToolbarContainerHeight - provider.SeparatorWidth, provider.SeparatorBorder, context =>
            {
                context.RectGetProvider.Speed = provider.AnimationSpeed;
                subTabSeparatorThumbGetter    = context.RectGetProvider;
            });
        sourceContainer.Add(tabSeparator);
        sourceContainer.Add(subTabSeparator);
        sourceContainer.Add(logoBottomSeparator);
        sourceContainer.Add(logoRightSeparator);
        sourceContainer.Add(tabSeparatorThumb);
        sourceContainer.Add(subTabSeparatorThumb);
        tabSeparatorSelectorGetter    = tabSeparatorThumbGetter;
        subTabSeparatorSelectorGetter = subTabSeparatorThumbGetter;
        #endregion
        tabButtonsContainer = m_ContainerBuilder.Build("TabButtonsContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabButtonOption.TabButtonSize, tabButtonsContainerHeight))
                   .SetOption(new OgMarginTransformerOption(option.TabButtonsContainerOffset, containerY + option.ToolbarContainerOffset));
        }));
        tabContainer = m_ContainerBuilder.Build("TabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabButtonOption.TabContainerWidth, tabButtonsContainerHeight))
                   .SetOption(new OgMarginTransformerOption(tabContainerX + option.TabButtonsContainerOffset, containerY + option.ToolbarContainerOffset));
        }));
        sourceContainer.Add(tabButtonsContainer);
        sourceContainer.Add(tabContainer);
        window.Add(m_BackgroundBuilder.Build("MainWindowBackground", option.BackgroundColorProperty, option.WindowWidth, option.WindowHeight, 0, 0,
            option.WindowBorderRadius));
        window.Add(sourceContainer);
        return window;
    }
}