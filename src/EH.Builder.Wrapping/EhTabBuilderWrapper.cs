using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
using EH.Builder.Wrapping.DataTypes;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Wrapping;
public class EhTabBuilderWrapper
{
    private readonly EhMainWindowBuilderWrapper      m_MainWindowBuilder;
    private readonly EhTabBuilder                    m_TabBuilder;
    private readonly EhTabButtonBuilder              m_TabButtonBuilder;
    private readonly Dictionary<string, EhSourceTab> m_Tabs = [];
    public EhTabBuilderWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider, EhMainWindowBuilderWrapper mainWindowBuilder)
    {
        EhBaseBackgroundBuilder backgroundBuilder = new();
        EhContainerBuilder      containerBuilder  = new();
        EhBaseTextBuilder       textBuilder       = new(visualProvider);
        EhBaseToggleBuilder     toggleBuilder     = new();
        m_TabBuilder        = new(configProvider, backgroundBuilder, containerBuilder, textBuilder);
        m_TabButtonBuilder  = new(configProvider, backgroundBuilder, containerBuilder, toggleBuilder);
        m_MainWindowBuilder = mainWindowBuilder;
    }
    public EhSourceTab BuildTab(string name, Texture2D texture, string leftContainerName, string rightContainerName)
    {
        IOgToggle<IOgVisualElement> button = m_TabButtonBuilder.Build(name, texture, m_MainWindowBuilder.TabSeparator!, m_MainWindowBuilder.TabContainer,
            m_MainWindowBuilder.ToolBar, out IOgContainer<IOgElement> container, out IOgContainer<IOgElement> toolbarContainer);
        m_MainWindowBuilder.TabButtons.Add(button);
        m_TabBuilder.Build(leftContainerName, rightContainerName, container, out IOgContainer<IOgElement> leftContainer,
            out IOgContainer<IOgElement> rightContainer);
        EhSourceTab builtTab = new([new([leftContainer, rightContainer])], container, toolbarContainer, button);
        m_Tabs.Add(name, builtTab);
        return builtTab;
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
        foreach(IOgContainer<IOgElement> container in tab.Tabs)
        {
            m_MainWindowBuilder.TabContainer.Remove(container);
        }
        foreach(IOgContainer<IOgElement> container in tab.Tabs)
        {
        }
    }
}