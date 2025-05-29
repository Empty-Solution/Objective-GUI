using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Wrapping.DataTypes;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System;
using UnityEngine;
namespace EH.Builder.Wrapping;
public class EhWindowBuilderWrapper
{
    private readonly EhTabBuilderWrapper                             m_TabBuilderWrapper;
    private readonly EhTabButtonBuilder                              m_TabButtonBuilder;
    private readonly IOgContainer<IOgElement>?                       m_TabButtons;
    private readonly IOgContainer<IOgElement>?                       m_TabContainer;
    private readonly OgAnimationRectGetter<OgTransformerRectGetter>? m_TabSeparator;
    private readonly IOgContainer<IOgElement>                        m_Window;
    private          IOgContainer<IOgElement>?                       m_ToolBar;
    public EhWindowBuilderWrapper(EhConfigProvider configProvider, EhTabBuilderWrapper tabBuilderWrapper)
    {
        EhBackgroundBuilder        backgroundBuilder = new();
        EhContainerBuilder         containerBuilder  = new();
        EhInternalDraggableBuilder draggableBuilder  = new();
        EhInternalToggleBuilder    toggleBuilder     = new();
        EhMainWindowBuilder        mainWindowBuilder = new(configProvider, backgroundBuilder, containerBuilder, draggableBuilder);
        m_TabButtonBuilder  = new(configProvider, backgroundBuilder, containerBuilder, toggleBuilder);
        m_TabBuilderWrapper = tabBuilderWrapper;
        m_Window            = mainWindowBuilder.Build(out m_TabButtons, out m_TabContainer, out m_ToolBar, out m_TabSeparator);
    }
    public IOgContainer<IOgElement> GetMainWindow() => m_Window;
    public IOgContainer<IOgElement> BuildTab(string name, Texture2D texture)
    {
        if(m_Window is null) throw new InvalidOperationException("Window isn't built yet. Call GetMainWindow() first");
        IOgToggle<IOgVisualElement> button =
            m_TabButtonBuilder.Build(name, texture, m_TabSeparator!, m_TabContainer!, out IOgContainer<IOgElement> container);
        m_TabButtons!.Add(button);
        return container;
    }
    public EhTab BuildTab(string name, Texture2D texture, string leftContainerName, string rightContainerName)
    {
        IOgContainer<IOgElement> container = BuildTab(name, texture);
        return m_TabBuilderWrapper.BuildTab(leftContainerName, rightContainerName, container);
    }
}