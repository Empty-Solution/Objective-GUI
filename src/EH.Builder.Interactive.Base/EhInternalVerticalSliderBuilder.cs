using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace EH.Builder.Interactive.Base;
public class EhInternalVerticalSliderBuilder
{
    private readonly OgVerticalSliderBuilder           m_OgSliderBuilder;
    private readonly DkProcessor<OgSliderBuildContext> m_Processor;
    public EhInternalVerticalSliderBuilder()
    {
        m_Processor       = new();
        m_OgSliderBuilder = new(new(), m_Processor);
    }
    public IOgSlider<IOgVisualElement> Build(string name, IDkObservableProperty<float> value, float min, float max,
        IDkProcess<OgSliderBuildContext> process)
    {
        m_Processor.AddProcess(process);
        IOgSlider<IOgVisualElement> element = m_OgSliderBuilder.Build(new(name, value, min, max));
        m_Processor.RemoveProcess(process);
        return element;
    }
}