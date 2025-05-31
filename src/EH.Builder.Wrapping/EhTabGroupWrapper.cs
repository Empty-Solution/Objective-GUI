using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Abstraction.Generic;
using EH.Builder.Config.Abstraction;
using EH.Builder.DataTypes;
using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using EH.Builder.Visual;
using EH.Builder.Wrapping.DataTypes;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EH.Builder.Wrapping;

public class EhSubTabWrapper
{
    private readonly EhButtonBuilder              m_ButtonBuilder;
    private readonly IEhConfigProvider            m_ConfigProvider;
    private readonly EhContainerBuilder           m_ContainerBuilder;
    private readonly EhDropdownBuilder            m_DropdownBuilder;
    private readonly EhInternalColorPickerBuilder m_InternalPickerBuilder;
    private readonly EhSliderBuilder              m_SliderBuilder;
    private readonly EhSourceTab                  m_Tab;
    private readonly EhToggleBuilder              m_ToggleBuilder;
    public EhSubTabWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider, EhSourceTab tab)
    {
        EhQuadBuilder                  quadBuilder             = new(visualProvider);
        EhBaseTextBuilder              textBuilder             = new(visualProvider);
        EhBaseBackgroundBuilder        backgroundBuilder       = new();
        EhContainerBuilder             containerBuilder        = new();
        EhBaseFillBuilder              baseFillBuilder         = new();
        EhBaseThumbBuilder             thumbBuilder            = new();
        EhBaseHorizontalSliderBuilder  horizontalSliderBuilder = new();
        EhBaseVerticalSliderBuilder    verticalSliderBuilder   = new();
        EhBaseVectorBuilder            vectorBuilder           = new();
        EhBaseToggleBuilder            toggleBuilder           = new();
        EhBaseModalInteractableBuilder interactableBuilder     = new();
        EhBaseButtonBuilder            buttonBuilder           = new();
        EhBaseBindableBuilder<float>   sliderBindBuilder       = new();
        EhBaseBindableBuilder<bool>    toggleBindBuilder       = new();
        EhInternalBindModalBuilder<float> sliderBindModalBuilder = new(configProvider, backgroundBuilder, containerBuilder, textBuilder,
            interactableBuilder, buttonBuilder, sliderBindBuilder);
        EhInternalBindModalBuilder<bool> toggleBindModalBuilder = new(configProvider, backgroundBuilder, containerBuilder, textBuilder,
            interactableBuilder, buttonBuilder, toggleBindBuilder);
        m_SliderBuilder = new(configProvider, containerBuilder, textBuilder,
            new(configProvider, backgroundBuilder, baseFillBuilder, textBuilder, thumbBuilder, horizontalSliderBuilder), sliderBindModalBuilder);
        m_ToggleBuilder = new(configProvider, containerBuilder, textBuilder,
            new(configProvider, backgroundBuilder, baseFillBuilder, thumbBuilder, toggleBuilder), toggleBindModalBuilder);
        m_InternalPickerBuilder = new(configProvider, backgroundBuilder, containerBuilder, interactableBuilder, quadBuilder, vectorBuilder,
            horizontalSliderBuilder, verticalSliderBuilder);
        m_DropdownBuilder  = new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder);
        m_ButtonBuilder    = new(configProvider, backgroundBuilder, textBuilder, buttonBuilder);
        m_Tab              = tab;
        m_ConfigProvider   = configProvider;
        m_ContainerBuilder = containerBuilder;
    }
    public void BuildSubTabs(IEnumerable<string> names)
    {
        List<IDkGetProvider<string>> valueGetters = [];
        foreach(string name in names) valueGetters.Add(new DkReadOnlyGetter<string>(name));

        m_DropdownBuilder.Build(new DkReadOnlyGetter<string>(name), property, valueGetters, y);
    }
}
public class EhSubTabObserver(EhSourceTab tab) : IDkObserver<int>
{
    public void Update(int state)
    {
        tab.SourceContainer.Clear();
        tab.SourceContainer.Add(tab.Tabs.ElementAt(state).SourceContainer);
    }
    public void Update(object state)
    {
        if(state is int value) Update(value);
    }
}
public class EhTabGroupWrapper
{
    private readonly EhButtonBuilder              m_ButtonBuilder;
    private readonly IEhConfigProvider            m_ConfigProvider;
    private readonly EhContainerBuilder           m_ContainerBuilder;
    private readonly EhDropdownBuilder            m_DropdownBuilder;
    private readonly EhInternalColorPickerBuilder m_InternalPickerBuilder;
    private readonly EhSliderBuilder              m_SliderBuilder;
    private readonly EhTab                        m_Tab;
    private readonly EhToggleBuilder              m_ToggleBuilder;
    public EhTabGroupWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider, EhTab tab)
    {
        EhQuadBuilder                  quadBuilder             = new(visualProvider);
        EhBaseTextBuilder              textBuilder             = new(visualProvider);
        EhBaseBackgroundBuilder        backgroundBuilder       = new();
        EhContainerBuilder             containerBuilder        = new();
        EhBaseFillBuilder              baseFillBuilder         = new();
        EhBaseThumbBuilder             thumbBuilder            = new();
        EhBaseHorizontalSliderBuilder  horizontalSliderBuilder = new();
        EhBaseVerticalSliderBuilder    verticalSliderBuilder   = new();
        EhBaseVectorBuilder            vectorBuilder           = new();
        EhBaseToggleBuilder            toggleBuilder           = new();
        EhBaseModalInteractableBuilder interactableBuilder     = new();
        EhBaseButtonBuilder            buttonBuilder           = new();
        EhBaseBindableBuilder<float>   sliderBindBuilder       = new();
        EhBaseBindableBuilder<bool>    toggleBindBuilder       = new();
        EhInternalBindModalBuilder<float> sliderBindModalBuilder = new(configProvider, backgroundBuilder, containerBuilder, textBuilder,
            interactableBuilder, buttonBuilder, sliderBindBuilder);
        EhInternalBindModalBuilder<bool> toggleBindModalBuilder = new(configProvider, backgroundBuilder, containerBuilder, textBuilder,
            interactableBuilder, buttonBuilder, toggleBindBuilder);
        m_SliderBuilder = new(configProvider, containerBuilder, textBuilder,
            new(configProvider, backgroundBuilder, baseFillBuilder, textBuilder, thumbBuilder, horizontalSliderBuilder), sliderBindModalBuilder);
        m_ToggleBuilder = new(configProvider, containerBuilder, textBuilder,
            new(configProvider, backgroundBuilder, baseFillBuilder, thumbBuilder, toggleBuilder), toggleBindModalBuilder);
        m_InternalPickerBuilder = new(configProvider, backgroundBuilder, containerBuilder, interactableBuilder, quadBuilder, vectorBuilder,
            horizontalSliderBuilder, verticalSliderBuilder);
        m_DropdownBuilder  = new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder);
        m_ButtonBuilder    = new(configProvider, backgroundBuilder, textBuilder, buttonBuilder);
        m_Tab              = tab;
        m_ConfigProvider   = configProvider;
        m_ContainerBuilder = containerBuilder;
    }
    public void BuildToggleWithPicker(string name, IEhProperty<bool> property, IEhProperty<Color> color, ushort group)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, group, out IOgContainer<IOgElement> tabContainer);
        sourceContainer.Add(BuildToggle(name, property, 0f));
        sourceContainer.Add(BuildPicker(name, color, m_ConfigProvider.ToggleConfig));
        tabContainer.Add(sourceContainer);
    }
    public void BuildSliderWithPicker(string name, IEhProperty<float> property, float min, float max, string textFormat, int round,
        IEhProperty<Color> color, ushort group)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, group, out IOgContainer<IOgElement> tabContainer);
        sourceContainer.Add(BuildSlider(name, property, min, max, textFormat, round, 0f));
        sourceContainer.Add(BuildPicker(name, color, m_ConfigProvider.ToggleConfig));
        tabContainer.Add(sourceContainer);
    }
    public void BuildDropdownWithPicker(string name, IEhProperty<int> property, string[] values, IEhProperty<Color> color, ushort group)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, group, out IOgContainer<IOgElement> tabContainer);
        sourceContainer.Add(BuildDropdown(name, property, values, 0f));
        sourceContainer.Add(BuildPicker(name, color, m_ConfigProvider.ToggleConfig));
        tabContainer.Add(sourceContainer);
    }
    public void BuildButton(string name, Action action, float x, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        container.Add(BuildButton(name, action, x, GetVerticalPadding(container)));
    }
    public void BuildToggle(string name, IEhProperty<bool> property, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        container.Add(BuildToggle(name, property, GetVerticalPadding(container)));
    }
    public void BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        container.Add(BuildSlider(name, property, min, max, textFormat, round, GetVerticalPadding(container)));
    }
    public void BuildDropdown(string name, IEhProperty<int> property, string[] values, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        container.Add(BuildDropdown(name, property, values, GetVerticalPadding(container)));
    }
    private IOgContainer<IOgElement> BuildPicker(string name, IEhProperty<Color> color, IEhElementConfig config) =>
        m_InternalPickerBuilder.Build(name, color, m_ConfigProvider.InteractableElementConfig.Width - config.Width - m_ConfigProvider.PickerConfig.Width,
            (m_ConfigProvider.InteractableElementConfig.Height - m_ConfigProvider.PickerConfig.Height) / 2);
    private IOgContainer<IOgElement> BuildContainer(string name, ushort group, out IOgContainer<IOgElement> tabContainer)
    {
        tabContainer = GetGroup(group);
        float verticalPadding = GetVerticalPadding(tabContainer);
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(m_ConfigProvider.InteractableElementConfig.Width,
                           m_ConfigProvider.InteractableElementConfig.Height)).SetOption(new OgMarginTransformerOption(0, verticalPadding));
            }));
        return sourceContainer;
    }
    private IOgContainer<IOgVisualElement> BuildButton(string name, Action action, float x, float y) =>
        m_ButtonBuilder.Build(new DkReadOnlyGetter<string>(name), action, x, y);
    private IOgContainer<IOgElement> BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, float y) =>
        m_SliderBuilder.Build(new DkReadOnlyGetter<string>(name), property, min, max, textFormat, round, y);
    private IOgContainer<IOgElement> BuildToggle(string name, IEhProperty<bool> property, float y) =>
        m_ToggleBuilder.Build(new DkReadOnlyGetter<string>(name), property, y);
    private IOgContainer<IOgElement> BuildDropdown(string name, IEhProperty<int> property, string[] values, float y)
    {
        IDkGetProvider<string>[] valueGetters                  = new IDkGetProvider<string>[values.Length];
        for(int i = 0; i < values.Length; i++) valueGetters[i] = new DkReadOnlyGetter<string>(values[i]);
        return m_DropdownBuilder.Build(new DkReadOnlyGetter<string>(name), property, valueGetters, y);
    }
    private IOgContainer<IOgElement> GetGroup(ushort group)
    {
        if(m_Tab.Groups.Count() <= group) throw new InvalidOperationException("Group doesn't exist");
        return m_Tab.Groups.ElementAt(group);
    }
    private float GetVerticalPadding(IOgContainer<IOgElement> container) =>
        m_ConfigProvider.InteractableElementConfig.VerticalPadding + (container.Elements.Count() *
                                                                      (m_ConfigProvider.InteractableElementConfig.Height +
                                                                       m_ConfigProvider.InteractableElementConfig.VerticalPadding));
}