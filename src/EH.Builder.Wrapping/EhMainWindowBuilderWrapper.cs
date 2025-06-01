using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.DataTypes;
using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using OG.Transformer.Options;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EH.Builder.Wrapping;
public class EhMainWindowBuilderWrapper
{
    private readonly IEhConfigProvider         m_ConfigProvider;
    private readonly EhContainerBuilder        m_ContainerBuilder;
    private readonly EhInternalDropdownBuilder m_DropdownBuilder;
    private readonly EhSubTabBuilder           m_SubTabBuilder;
    private readonly EhTabBuilder              m_TabBuilder;
    private readonly EhTabGroupBuilder         m_TabGroupBuilder;
    private readonly IEhWindow                 m_Window;
    public EhMainWindowBuilderWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider)
    {
        EhBaseBackgroundBuilder        backgroundBuilder   = new();
        EhContainerBuilder             containerBuilder    = new();
        EhBaseTextBuilder              textBuilder         = new(visualProvider);
        EhBaseToggleBuilder            toggleBuilder       = new();
        EhBaseModalInteractableBuilder interactableBuilder = new();
        EhBaseButtonBuilder            buttonBuilder       = new();
        m_TabGroupBuilder  = new(configProvider, backgroundBuilder, containerBuilder, textBuilder);
        m_TabBuilder       = new(configProvider, backgroundBuilder, containerBuilder, toggleBuilder);
        m_DropdownBuilder  = new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder);
        m_SubTabBuilder    = new(configProvider, containerBuilder);
        m_ContainerBuilder = containerBuilder;
        m_ConfigProvider   = configProvider;
        EhBaseDraggableBuilder draggableBuilder  = new();
        EhMainWindowBuilder    mainWindowBuilder = new(configProvider, backgroundBuilder, containerBuilder, draggableBuilder);
        m_Window = mainWindowBuilder.Build(visualProvider.LogoTexture);
    }
    public IEhTab BuildTab(string name, Texture2D texture)
    {
        IEhTab tab = m_TabBuilder.Build(name, texture, m_Window);
        m_Window.LinkTabButton(tab.Button);
        return tab;
    }
    public void BuildMultiplySubTabs(IEnumerable<string> names, int initial, IEhTab sourceTab)
    {
        IDkGetProvider<string>[] valueGetters                  = new IDkGetProvider<string>[names.Count()];
        for(int i = 0; i < names.Count(); i++) valueGetters[i] = new DkReadOnlyGetter<string>(names.ElementAt(i));
        DkObservableProperty<int> property                     = new(new DkObservable<int>([]), 0);
        IEhDropdown dropdown = m_DropdownBuilder.Build("SubTabSelector", property, valueGetters, m_ConfigProvider.DropdownConfig.Width,
            m_ConfigProvider.DropdownConfig.Height, 0, 0);
        dropdown.OptionsContainer.SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleRight))
                .SetOption(new OgMarginTransformerOption(-m_ConfigProvider.InteractableElementConfig.HorizontalPadding));
        foreach(IDkGetProvider<string> name in valueGetters) sourceTab.AddSubTab(m_SubTabBuilder.Build(name, dropdown));
        DkScriptableObserver<int> subTabObserver = new();
        subTabObserver.OnUpdate += value =>
        {
            sourceTab.TabContainer.Clear();
            sourceTab.SubTabs.ElementAt(value).LinkSelf(sourceTab.TabContainer);
        };
        property.AddObserver(subTabObserver);
        property.Set(initial);
        dropdown.LinkSelf(sourceTab.ToolbarContainer);
    }
    public IEhSubTab BuildSingleSubTab(string leftContainerName, string rightContainerName, IEhTab sourceTab)
    {
        if(sourceTab.SubTabs.Any()) return sourceTab.SubTabs.ElementAt(0);
        IEhSubTab subTab = m_SubTabBuilder.Build(leftContainerName);
        BuildTabGroups(leftContainerName, rightContainerName, subTab);
        subTab.LinkSelf(sourceTab.TabContainer);
        return subTab;
    }
    public void BuildTabGroups(string leftContainerName, string rightContainerName, IEhSubTab subTab)
    {
        (IEhTabGroup group0, IEhTabGroup group1) groups =
            m_TabGroupBuilder.Build(new DkReadOnlyGetter<string>(leftContainerName), new DkReadOnlyGetter<string>(rightContainerName));
        subTab.AddGroup(groups.group0);
        subTab.AddGroup(groups.group1);
    }
    public IEhWindow GetMainWindow() => m_Window;
}