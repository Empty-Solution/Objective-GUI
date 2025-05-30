using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using EH.Builder.Wrapping.DataTypes;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using UnityEngine;
namespace EH.Builder.Wrapping;
public class EhTabBuilderWrapper
{
    private readonly EhMainWindowBuilderWrapper m_MainWindowBuilder;
    private readonly EhTabBuilder               m_TabBuilder;
    private readonly EhTabButtonBuilder         m_TabButtonBuilder;
    public EhTabBuilderWrapper(EhConfigProvider configProvider, IEhVisualProvider visualProvider, EhMainWindowBuilderWrapper mainWindowBuilder)
    {
        EhBaseBackgroundBuilder backgroundBuilder = new();
        EhContainerBuilder      containerBuilder  = new();
        EhBaseTextBuilder       textBuilder       = new(visualProvider);
        EhBaseToggleBuilder     toggleBuilder     = new();
        m_TabBuilder        = new(configProvider, backgroundBuilder, containerBuilder, textBuilder);
        m_TabButtonBuilder  = new(configProvider, backgroundBuilder, containerBuilder, toggleBuilder);
        m_MainWindowBuilder = mainWindowBuilder;
    }
    public EhTab BuildTab(string buttonName, Texture2D texture, string leftContainerName, string rightContainerName)
    {
        m_MainWindowBuilder.TabButtons.Add(m_TabButtonBuilder.Build(buttonName, texture, m_MainWindowBuilder.TabSeparator!,
            m_MainWindowBuilder.TabContainer!, out IOgContainer<IOgElement> container));
        m_TabBuilder.Build(leftContainerName, rightContainerName, container, out IOgContainer<IOgElement> leftContainer,
            out IOgContainer<IOgElement> rightContainer);
        return new([leftContainer, rightContainer]);
    }
}