using DK.Getting.Abstraction.Generic;
using EH.Builder.DataTypes;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhSubTabBuilder(IEhConfigProvider provider, EhContainerBuilder containerBuilder)
{
    public IEhSubTab Build(IDkGetProvider<string> name) => new EhSubTab(BuildContainer(name.Get(), out IOgOptionsContainer options), options);
    public IEhSubTab Build(string name) => new EhSubTab(BuildContainer(name, out IOgOptionsContainer options), options);
    private IOgContainer<IOgElement> BuildContainer(string name, out IOgOptionsContainer options)
    {
        float tabContainerHeight = provider.MainWindowConfig.Height - provider.MainWindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.MainWindowConfig.ToolbarContainerOffset * 2);
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> container = containerBuilder.Build(name, new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabGroupConfig.Width, tabContainerHeight));
            optionsContainer = context.RectGetProvider.Options;
        }));
        options = optionsContainer;
        return container;
    }
}