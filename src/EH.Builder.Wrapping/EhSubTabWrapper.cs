using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using EH.Builder.Wrapping.DataTypes;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Wrapping;
public class EhSubTabWrapper
{
    private readonly IEhConfigProvider         m_ConfigProvider;
    private readonly EhContainerBuilder        m_ContainerBuilder;
    private readonly EhInternalDropdownBuilder m_DropdownBuilder;
    public EhSubTabWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider)
    {
        EhBaseTextBuilder              textBuilder         = new(visualProvider);
        EhBaseBackgroundBuilder        backgroundBuilder   = new();
        EhContainerBuilder             containerBuilder    = new();
        EhBaseModalInteractableBuilder interactableBuilder = new();
        EhBaseButtonBuilder            buttonBuilder       = new();
        m_DropdownBuilder  = new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder);
        m_ContainerBuilder = containerBuilder;
        m_ConfigProvider   = configProvider;
    }
    public void BuildSubTabs(IEnumerable<string> names, int initial, EhSourceTab sourceTab)
    {
        List<IDkGetProvider<string>> valueGetters = [];
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach(string name in names) valueGetters.Add(new DkReadOnlyGetter<string>(name));
        DkObservableProperty<int> property = new(new DkObservable<int>([]), initial);
        m_DropdownBuilder.Build("SubTabSelector", property, valueGetters, 0, 0, out IOgOptionsContainer options);
        options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleRight));
        float tabContainerHeight = m_ConfigProvider.MainWindowConfig.Height - m_ConfigProvider.MainWindowConfig.ToolbarContainerHeight -
                                   (m_ConfigProvider.SeparatorOffset * 2) - (m_ConfigProvider.MainWindowConfig.ToolbarContainerOffset * 2);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach(string name in names)
        {
            IOgContainer<IOgElement> container = m_ContainerBuilder.Build(name, new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(m_ConfigProvider.TabConfig.Width, tabContainerHeight));
            }));
            sourceTab.AddSubTab(new(container));
        }
        property.AddObserver(new EhSubTabObserver(sourceTab));
    }
}