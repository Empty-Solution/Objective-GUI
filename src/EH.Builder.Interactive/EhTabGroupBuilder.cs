using DK.Getting.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhTabGroupBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseTextBuilder textBuilder)
{
    public (IEhTabGroup group0, IEhTabGroup group1) Build(IDkGetProvider<string> leftContainerName, IDkGetProvider<string> rightContainerName)
    {
        EhTabGroupConfig tabGroupConfig = provider.TabGroupConfig;
        float tabContainerHeight = provider.MainWindowConfig.Height - provider.MainWindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.MainWindowConfig.ToolbarContainerOffset * 2);
        IEhTabGroup group0 = BuildTabContainer(leftContainerName, tabGroupConfig.Width, tabContainerHeight, 0, 0, tabGroupConfig);
        IEhTabGroup group1 = BuildTabContainer(rightContainerName, tabGroupConfig.Width, tabContainerHeight,
            tabGroupConfig.TabContainerPadding + tabGroupConfig.Width, 0, tabGroupConfig);
        return (group0, group1);
    }
    private IEhTabGroup BuildTabContainer(IDkGetProvider<string> name, float width, float height, float x, float y, EhTabGroupConfig tabGroupConfig)
    {
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceTabGroupContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
                optionsContainer = context.RectGetProvider.Options;
            }));
        IOgContainer<IOgElement> builtContainer = containerBuilder.Build($"{name}TabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height - tabGroupConfig.GroupTitleHeight))
                       .SetOption(new OgMarginTransformerOption(0, tabGroupConfig.GroupTitleHeight));
            }));
        sourceContainer.Add(backgroundBuilder.Build($"{name}TabContainerBackground", tabGroupConfig.BackgroundColor, width,
            height - tabGroupConfig.GroupTitleHeight, 0, tabGroupConfig.GroupTitleHeight,
            new(tabGroupConfig.BackgroundBorder, tabGroupConfig.BackgroundBorder, tabGroupConfig.BackgroundBorder, tabGroupConfig.BackgroundBorder)));
        sourceContainer.Add(textBuilder.Build($"{name}TabContainerTitle", tabGroupConfig.GroupTitleColor, name, tabGroupConfig.GroupTitleFontSize,
            tabGroupConfig.GroupTitleAlignment, width, tabGroupConfig.GroupTitleHeight));
        sourceContainer.Add(builtContainer);
        return new EhTabGroup(sourceContainer, builtContainer, optionsContainer);
    }
}