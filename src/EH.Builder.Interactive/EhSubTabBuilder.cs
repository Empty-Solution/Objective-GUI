using DK.Getting.Abstraction.Generic;
using EH.Builder.DataTypes;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhSubTabBuilder(IEhConfigProvider provider, EhContainerBuilder containerBuilder)
{
    public IEhSubTab Build(IDkGetProvider<string> name, IEhDropdown dropdown)
    {
        dropdown.AddItem(name);
        return new EhSubTab(BuildContainer(name.Get()));
    }
    public IEhSubTab Build(string name) => new EhSubTab(BuildContainer(name));
    private IOgContainer<IOgElement> BuildContainer(string name)
    {
        float tabContainerHeight = provider.MainWindowConfig.Height - provider.MainWindowConfig.ToolbarContainerHeight - (provider.SeparatorOffset * 2) -
                                   (provider.MainWindowConfig.ToolbarContainerOffset * 2);
        IOgContainer<IOgElement> container = containerBuilder.Build(name, new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(provider.TabGroupConfig.Width, tabContainerHeight));
        }));
        return container;
    }
}