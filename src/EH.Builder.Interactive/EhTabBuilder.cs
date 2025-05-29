using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhTabBuilder(EhConfigProvider provider, EhBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder, EhTextBuilder textBuilder)
{
    public void Build(string leftContainerName, string rightContainerName, IOgContainer<IOgElement> sourceTab, out IOgContainer<IOgElement> builtLeftGroup,
        out IOgContainer<IOgElement> builtRightGroup)
    {
        EhTabConfig tabConfig = provider.TabConfig;
        float tabContainerHeight = provider.WindowConfig.Height - provider.WindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.WindowConfig.ToolbarContainerOffset * 2);
        sourceTab.Add(BuildTabContainer(leftContainerName, out builtLeftGroup, tabConfig.TabContainerWidth, tabContainerHeight, 0, 0, tabConfig));
        sourceTab.Add(BuildTabContainer(rightContainerName, out builtRightGroup, tabConfig.TabContainerWidth, tabContainerHeight,
            tabConfig.TabContainerPadding + tabConfig.TabContainerWidth, 0, tabConfig));
    }
    private IOgContainer<IOgElement> BuildTabContainer(string name, out IOgContainer<IOgElement> builtContainer, float width, float height, float x,
        float y, EhTabConfig tabConfig)
    {
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceTabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
            }));
        builtContainer = containerBuilder.Build($"{name}TabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height - tabConfig.GroupTitleHeight))
                   .SetOption(new OgMarginTransformerOption(0, tabConfig.GroupTitleHeight));
        }));
        sourceContainer.Add(backgroundBuilder.Build($"{name}TabContainerBackground", tabConfig.BackgroundColor, width, height - tabConfig.GroupTitleHeight,
            0, tabConfig.GroupTitleHeight,
            new(tabConfig.BackgroundBorder, tabConfig.BackgroundBorder, tabConfig.BackgroundBorder, tabConfig.BackgroundBorder)));
        sourceContainer.Add(textBuilder.BuildStaticText($"{name}TabContainerTitle", tabConfig.GroupTitleColor, name, tabConfig.GroupTitleFontSize,
            tabConfig.GroupTitleAlignment, width, tabConfig.GroupTitleHeight));
        sourceContainer.Add(builtContainer);
        return sourceContainer;
    }
}