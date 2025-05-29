using DK.Getting.Abstraction.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhTabBuilder(EhConfigProvider provider, EhBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder, EhTextBuilder textBuilder)
{
    public void Build(string leftContainerName, string rightContainerName, IOgContainer<IOgElement> sourceTab,
        out IOgContainer<IOgElement> builtLeftContainer, out IOgContainer<IOgElement> builtRightContainer)
    {
        EhTabConfig tabConfig = provider.TabConfig;
        float tabContainerHeight = provider.WindowConfig.Height - provider.WindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.WindowConfig.ToolbarContainerOffset * 2);
        sourceTab.Add(BuildTabContainer(leftContainerName, out builtLeftContainer, tabConfig.TabContainerWidth, tabContainerHeight, 0,
            tabConfig.BackgroundBorder, tabConfig.BackgroundColor, tabConfig));
        sourceTab.Add(BuildTabContainer(rightContainerName, out builtRightContainer, tabConfig.TabContainerWidth, tabContainerHeight,
            tabConfig.TabContainerPadding + tabConfig.TabContainerWidth, tabConfig.BackgroundBorder, tabConfig.BackgroundColor, tabConfig));
    }
    private IOgContainer<IOgElement> BuildTabContainer(string name, out IOgContainer<IOgElement> builtContainer, float width, float height, float x,
        float border, IDkGetProvider<Color> colorGetter, EhTabConfig tabConfig)
    {
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceTabContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x));
            }));
        sourceContainer.Add(textBuilder.BuildStaticText($"{name}TabContainerTitle", colorGetter, name, tabConfig.TabTitleFontSize,
            tabConfig.TabTitleAlignment, width, height * 0.05f));
        builtContainer = containerBuilder.Build($"{name}TabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height))
                   .SetOption(new OgMarginTransformerOption(0, height * 0.05f));
        }));
        sourceContainer.Add(backgroundBuilder.Build($"{name}TabContainerBackground", colorGetter, width, height, 0, 0,
            new(border, border, border, border)));
        sourceContainer.Add(builtContainer);
        return sourceContainer;
    }
}