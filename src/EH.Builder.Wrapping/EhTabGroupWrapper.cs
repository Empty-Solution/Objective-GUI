using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
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
using System.Linq;
using UnityEngine;
namespace EH.Builder.Wrapping;
public class EhTabGroupWrapper
{
    private readonly EhButtonBuilder              m_ButtonBuilder;
    private readonly IEhConfigProvider            m_ConfigProvider;
    private readonly EhContainerBuilder           m_ContainerBuilder;
    private readonly EhDropdownBuilder            m_DropdownBuilder;
    private readonly EhInternalColorPickerBuilder m_InternalPickerBuilder;
    private readonly EhSliderBuilder              m_SliderBuilder;
    private readonly EhToggleBuilder              m_ToggleBuilder;
    public EhTabGroupWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider)
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
        m_DropdownBuilder = new(configProvider, new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder),
            textBuilder);
        m_ButtonBuilder    = new(configProvider, backgroundBuilder, textBuilder, buttonBuilder);
        m_ConfigProvider   = configProvider;
        m_ContainerBuilder = containerBuilder;
    }
    public void BuildToggleWithPicker(string name, IEhProperty<bool> property, IEhProperty<Color> color, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, subTab, group, out IOgContainer<IOgElement> tabContainer);
        sourceContainer.Add(BuildToggle(name, property, 0f));
        sourceContainer.Add(BuildPicker(name, color, m_ConfigProvider.ToggleConfig));
        tabContainer.Add(sourceContainer);
    }
    public void BuildSliderWithPicker(string name, IEhProperty<float> property, float min, float max, string textFormat, int round,
        IEhProperty<Color> color, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, subTab, group, out IOgContainer<IOgElement> tabContainer);
        sourceContainer.Add(BuildSlider(name, property, min, max, textFormat, round, 0f));
        sourceContainer.Add(BuildPicker(name, color, m_ConfigProvider.ToggleConfig));
        tabContainer.Add(sourceContainer);
    }
    public void BuildDropdownWithPicker(string name, IEhProperty<int> property, string[] values, IEhProperty<Color> color, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, subTab, group, out IOgContainer<IOgElement> tabContainer);
        sourceContainer.Add(BuildDropdown(name, property, values, 0f));
        sourceContainer.Add(BuildPicker(name, color, m_ConfigProvider.ToggleConfig));
        tabContainer.Add(sourceContainer);
    }
    public void BuildButton(string name, Action action, float x, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(subTab, group);
        container.Add(BuildButton(name, action, x, GetVerticalPadding(container)));
    }
    public void BuildToggle(string name, IEhProperty<bool> property, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(subTab, group);
        container.Add(BuildToggle(name, property, GetVerticalPadding(container)));
    }
    public void BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(subTab, group);
        container.Add(BuildSlider(name, property, min, max, textFormat, round, GetVerticalPadding(container)));
    }
    public void BuildDropdown(string name, IEhProperty<int> property, string[] values, EhSubTab subTab, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(subTab, group);
        container.Add(BuildDropdown(name, property, values, GetVerticalPadding(container)));
    }
    private IOgContainer<IOgElement> BuildPicker(string name, IEhProperty<Color> color, IEhElementConfig config) =>
        m_InternalPickerBuilder.Build(name, color, m_ConfigProvider.InteractableElementConfig.Width - config.Width - m_ConfigProvider.PickerConfig.Width,
            (m_ConfigProvider.InteractableElementConfig.Height - m_ConfigProvider.PickerConfig.Height) / 2);
    private IOgContainer<IOgElement> BuildContainer(string name, EhSubTab subTab, ushort group, out IOgContainer<IOgElement> tabContainer)
    {
        tabContainer = GetGroup(subTab, group);
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
    private IOgContainer<IOgElement> GetGroup(EhSubTab subTab, ushort group)
    {
        if(subTab.Groups.Count() <= group) throw new InvalidOperationException("Group doesn't exist");
        return subTab.Groups.ElementAt(group);
    }
    private float GetVerticalPadding(IOgContainer<IOgElement> container) =>
        m_ConfigProvider.InteractableElementConfig.VerticalPadding + (container.Elements.Count() *
                                                                      (m_ConfigProvider.InteractableElementConfig.Height +
                                                                       m_ConfigProvider.InteractableElementConfig.VerticalPadding));
}