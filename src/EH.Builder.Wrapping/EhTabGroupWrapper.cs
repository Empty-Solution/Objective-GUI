using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using EH.Builder.Config.Abstraction;
using EH.Builder.DataTypes;
using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Options;
using System;
using System.Collections.Generic;
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
    public void BuildToggleWithPicker(string name, IEhProperty<bool> property, IEhProperty<Color> color, IEhSubTab subTab, ushort groupIndex)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, subTab, groupIndex, out IEhTabGroup group);
        BuildToggle(name, property, 0f, sourceContainer);
        BuildPicker(name, color, m_ConfigProvider.ToggleConfig, sourceContainer);
        group.LinkChild(sourceContainer);
    }
    public void BuildSliderWithPicker(string name, IEhProperty<float> property, float min, float max, string textFormat, int round,
        IEhProperty<Color> color, IEhSubTab subTab, ushort groupIndex)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, subTab, groupIndex, out IEhTabGroup group);
        BuildSlider(name, property, min, max, textFormat, round, 0f, sourceContainer);
        BuildPicker(name, color, m_ConfigProvider.SliderConfig, sourceContainer);
        group.LinkChild(sourceContainer);
    }
    public void BuildDropdownWithPicker(string name, IEhProperty<int> property, string[] values, IEhProperty<Color> color, IEhSubTab subTab,
        ushort groupIndex)
    {
        IOgContainer<IOgElement> sourceContainer = BuildContainer(name, subTab, groupIndex, out IEhTabGroup group);
        BuildDropdown(name, property, values, 0f, sourceContainer);
        BuildPicker(name, color, m_ConfigProvider.DropdownConfig, sourceContainer);
        group.LinkChild(sourceContainer);
    }
    public void BuildButton(string name, Action action, float x, IEhSubTab subTab, ushort groupIndex)
    {
        IEhTabGroup group = GetGroup(subTab, groupIndex);
        BuildButton(name, action, x, GetVerticalPadding(group), group.GroupContainer);
    }
    public void BuildToggle(string name, IEhProperty<bool> property, IEhSubTab subTab, ushort groupIndex)
    {
        IEhTabGroup group = GetGroup(subTab, groupIndex);
        BuildToggle(name, property, GetVerticalPadding(group), group.GroupContainer);
    }
    public void BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, IEhSubTab subTab,
        ushort groupIndex)
    {
        IEhTabGroup group = GetGroup(subTab, groupIndex);
        BuildSlider(name, property, min, max, textFormat, round, GetVerticalPadding(group), group.GroupContainer);
    }
    public void BuildDropdown(string name, IEhProperty<int> property, string[] values, IEhSubTab subTab, ushort groupIndex)
    {
        IEhTabGroup group = GetGroup(subTab, groupIndex);
        BuildDropdown(name, property, values, GetVerticalPadding(group), group.GroupContainer);
    }
    private IOgContainer<IOgElement> BuildContainer(string name, IEhSubTab subTab, ushort groupIndex, out IEhTabGroup group)
    {
        group = GetGroup(subTab, groupIndex);
        float verticalPadding = GetVerticalPadding(group);
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(m_ConfigProvider.InteractableElementConfig.Width,
                           m_ConfigProvider.InteractableElementConfig.Height)).SetOption(new OgMarginTransformerOption(0, verticalPadding));
            }));
        return sourceContainer;
    }
    private void BuildPicker(string name, IEhProperty<Color> color, IEhElementConfig config, IOgContainer<IOgElement> container) =>
        m_InternalPickerBuilder.Build(name, color, m_ConfigProvider.InteractableElementConfig.Width - config.Width - m_ConfigProvider.PickerConfig.Width,
            (m_ConfigProvider.InteractableElementConfig.Height - m_ConfigProvider.PickerConfig.Height) / 2).LinkSelf(container);
    private void BuildButton(string name, Action action, float x, float y, IOgContainer<IOgElement> container) =>
        m_ButtonBuilder.Build(new DkReadOnlyGetter<string>(name), action, x, y).LinkSelf(container);
    private void BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, float y,
        IOgContainer<IOgElement> container) =>
        m_SliderBuilder.Build(new DkReadOnlyGetter<string>(name), property, min, max, textFormat, round, y).LinkSelf(container);
    private void BuildToggle(string name, IEhProperty<bool> property, float y, IOgContainer<IOgElement> container) =>
        m_ToggleBuilder.Build(new DkReadOnlyGetter<string>(name), property, y).LinkSelf(container);
    private void BuildDropdown(string name, IEhProperty<int> property, string[] values, float y, IOgContainer<IOgElement> container)
    {
        List<IDkGetProvider<string>> valueGetters = [];
        foreach(string value in values) valueGetters.Add(new DkReadOnlyGetter<string>(value));
        m_DropdownBuilder.Build(new DkReadOnlyGetter<string>(name), property, valueGetters, y).LinkSelf(container);
    }
    private IEhTabGroup GetGroup(IEhSubTab subTab, ushort group)
    {
        if(subTab.Groups.Count() <= group) throw new InvalidOperationException("Group doesn't exist");
        return subTab.Groups.ElementAt(group);
    }
    private float GetVerticalPadding(IEhTabGroup group) =>
        m_ConfigProvider.InteractableElementConfig.VerticalPadding + (group.GroupContainer.Elements.Count() *
                                                                      (m_ConfigProvider.InteractableElementConfig.Height +
                                                                       m_ConfigProvider.InteractableElementConfig.VerticalPadding));
}