using EH.Builder.DataTypes;
using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using EH.Builder.Visual;
using EH.Builder.Wrapping.DataTypes;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System;
using System.Linq;
namespace EH.Builder.Wrapping;
public class EhTabGroupWrapper
{
    private readonly EhConfigProvider             m_ConfigProvider;
    private readonly EhDropdownBuilder            m_DropdownBuilder;
    private readonly EhInternalColorPickerBuilder m_InternalPickerBuilder;
    private readonly EhSliderBuilder              m_SliderBuilder;
    private readonly EhTab                        m_Tab;
    private readonly EhToggleBuilder              m_ToggleBuilder;
    public EhTabGroupWrapper(EhConfigProvider configProvider, IEhVisualProvider visualProvider, EhTab tab)
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
        m_DropdownBuilder = new(configProvider, backgroundBuilder, containerBuilder, buttonBuilder, interactableBuilder, textBuilder);
        m_Tab             = tab;
        m_ConfigProvider  = configProvider;
    }
    public void BuildToggle(string name, IEhProperty<bool> property, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        IOgContainer<IOgElement> slider    = BuildToggle(name, property, GetVerticalPadding(container));
        container.Add(slider);
    }
    public void BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        IOgContainer<IOgElement> slider    = BuildSlider(name, property, min, max, textFormat, round, GetVerticalPadding(container));
        container.Add(slider);
    }
    public void BuildDropdown(string name, IEhProperty<int> property, string[] values, ushort group)
    {
        IOgContainer<IOgElement> container = GetGroup(group);
        IOgContainer<IOgElement> slider    = BuildDropdown(name, property, values, GetVerticalPadding(container));
        container.Add(slider);
    }
    private IOgContainer<IOgElement> BuildSlider(string name, IEhProperty<float> property, float min, float max, string textFormat, int round, float y) =>
        m_SliderBuilder.Build(name, property, min, max, textFormat, round, y);
    private IOgContainer<IOgElement> BuildToggle(string name, IEhProperty<bool> property, float y) => m_ToggleBuilder.Build(name, property, y);
    private IOgContainer<IOgElement> BuildDropdown(string name, IEhProperty<int> property, string[] values, float y) =>
        m_DropdownBuilder.Build(name, property, values, y);
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