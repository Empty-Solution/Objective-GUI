using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using EH.Builder.Wrapping.DataTypes;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable LoopCanBeConvertedToQuery
namespace EH.Builder.Wrapping;
public class EhTabBuilderWrapper
{
    private readonly IEhConfigProvider               m_ConfigProvider;
    private readonly EhContainerBuilder              m_ContainerBuilder;
    private readonly EhInternalDropdownBuilder       m_DropdownBuilder;
    private readonly EhMainWindowBuilderWrapper      m_MainWindowBuilder;
    private readonly EhTabBuilder                    m_TabBuilder;
    private readonly EhTabButtonBuilder              m_TabButtonBuilder;
    private readonly Dictionary<string, EhSourceTab> m_Tabs = [];
    public EhTabBuilderWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider, EhMainWindowBuilderWrapper mainWindowBuilder)
    {
        EhBaseBackgroundBuilder        backgroundBuilder   = new();
        EhContainerBuilder             containerBuilder    = new();
        EhBaseTextBuilder              textBuilder         = new(visualProvider);
        EhBaseToggleBuilder            toggleBuilder       = new();
        EhBaseModalInteractableBuilder interactableBuilder = new();
        EhBaseButtonBuilder            buttonBuilder       = new();
        m_TabBuilder        = new(configProvider, backgroundBuilder, containerBuilder, textBuilder);
        m_TabButtonBuilder  = new(configProvider, backgroundBuilder, containerBuilder, toggleBuilder);
        m_DropdownBuilder   = new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder);
        m_ContainerBuilder  = containerBuilder;
        m_ConfigProvider    = configProvider;
        m_MainWindowBuilder = mainWindowBuilder;
    }
    public EhSourceTab BuildTab(string name, Texture2D texture)
    {
        IOgToggle<IOgVisualElement> button = m_TabButtonBuilder.Build(name, texture, m_MainWindowBuilder.TabSeparator!, m_MainWindowBuilder.TabContainer,
            m_MainWindowBuilder.ToolBar, out IOgContainer<IOgElement> container, out IOgContainer<IOgElement> toolbarContainer);
        m_MainWindowBuilder.TabButtons.Add(button);
        EhSourceTab builtTab = new(container, toolbarContainer, button);
        m_Tabs.Add(name, builtTab);
        return builtTab;
    }
    public void BuildMultiplySubTabs(IEnumerable<string> names, int initial, EhSourceTab sourceTab)
    {
        List<IDkGetProvider<string>> valueGetters = [];
        foreach(string name in names) valueGetters.Add(new DkReadOnlyGetter<string>(name));
        DkObservableProperty<int> property = new(new DkObservable<int>([]), 0);
        IOgContainer<IOgElement> dropdown = m_DropdownBuilder.Build("SubTabSelector", property, valueGetters, m_ConfigProvider.DropdownConfig.Width,
            m_ConfigProvider.DropdownConfig.Height, 0, 0, out IOgOptionsContainer options);
        options.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleRight))
               .SetOption(new OgMarginTransformerOption(-m_ConfigProvider.InteractableElementConfig.HorizontalPadding));
        float tabContainerHeight = m_ConfigProvider.MainWindowConfig.Height - m_ConfigProvider.MainWindowConfig.ToolbarContainerHeight -
                                   (m_ConfigProvider.SeparatorOffset * 2) - (m_ConfigProvider.MainWindowConfig.ToolbarContainerOffset * 2);
        foreach(string name in names)
        {
            IOgContainer<IOgElement> container = m_ContainerBuilder.Build(name, new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(m_ConfigProvider.TabConfig.Width, tabContainerHeight));
            }));
            sourceTab.AddSubTab(new(container));
        }
        DkScriptableObserver<int> subTabObserver = new();
        subTabObserver.OnUpdate += value =>
        {
            sourceTab.SourceContainer.Clear();
            sourceTab.SourceContainer.Add(sourceTab.SubTabs.ElementAt(value).SourceContainer);
        };
        property.AddObserver(subTabObserver);
        property.Set(initial);
        sourceTab.Toolbar.Add(dropdown);
    }
    public EhSubTab BuildSingleSubTab(string leftContainerName, string rightContainerName, EhSourceTab sourceTab)
    {
        if(sourceTab.SubTabs.Any()) return sourceTab.SubTabs.ElementAt(0);
        EhSubTab subTab = new(sourceTab.SourceContainer);
        BuildTabGroups(leftContainerName, rightContainerName, subTab);
        sourceTab.AddSubTab(subTab);
        return subTab;
    }
    public void BuildTabGroups(string leftContainerName, string rightContainerName, EhSubTab subTab)
    {
        m_TabBuilder.Build(new DkReadOnlyGetter<string>(leftContainerName), new DkReadOnlyGetter<string>(rightContainerName), subTab.SourceContainer,
            out IOgContainer<IOgElement> leftContainer, out IOgContainer<IOgElement> rightContainer);
        subTab.AddGroup(leftContainer);
        subTab.AddGroup(rightContainer);
    }
    public EhSourceTab? GetTab(string name) => m_Tabs.TryGetValue(name, out EhSourceTab tab) ? tab : null;
    public void RemoveTab(string name)
    {
        EhSourceTab? tab = GetTab(name);
        if(tab == null) return;
        m_Tabs.Remove(name);
        m_MainWindowBuilder.TabButtons.Remove(tab.Button);
        m_MainWindowBuilder.TabContainer.Remove(tab.SourceContainer);
        m_MainWindowBuilder.ToolBar.Remove(tab.Toolbar);
    }
}