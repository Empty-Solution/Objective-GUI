using DK.Getting.Abstraction.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhTabBuilder(EhConfigProvider provider, IEhVisualProvider visualProvider)
{
    private readonly EhBackgroundBuilder m_BackgroundBuilder = new();
    private readonly EhContainerBuilder  m_ContainerBuilder  = new();
    private readonly EhTextBuilder       m_TextBuilder       = new(visualProvider);
    public void Build(string leftContainerName, string rightContainerName, IOgContainer<IOgElement> sourceTab,
        out IOgContainer<IOgElement> builtLeftContainer, out IOgContainer<IOgElement> builtRightContainer)
    {
        EhTabConfig tabConfig = provider.TabConfig;
        float tabContainerHeight = provider.WindowConfig.Height - provider.WindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.WindowConfig.ToolbarContainerOffset * 2);
        sourceTab.Add(BuildTabContainer(leftContainerName, out builtLeftContainer, tabConfig.TabContainerWidth, tabContainerHeight, 0,
            tabConfig.BackgroundBorder, tabConfig.BackgroundColor));
        sourceTab.Add(BuildTabContainer(rightContainerName, out builtRightContainer, tabConfig.TabContainerWidth, tabContainerHeight,
            tabConfig.TabContainerPadding + tabConfig.TabContainerWidth, tabConfig.BackgroundBorder, tabConfig.BackgroundColor));
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