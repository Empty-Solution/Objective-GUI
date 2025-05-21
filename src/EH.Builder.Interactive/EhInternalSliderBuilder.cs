using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace EH.Builder.Interactive;
public class EhInternalSliderBuilder
{
    private readonly OgHorizontalSliderBuilder         m_OgTextureBuilder;
    private readonly DkProcessor<OgSliderBuildContext> m_Processor;
    public EhInternalSliderBuilder()
    {
        m_Processor        = new();
        m_OgTextureBuilder = new(new(), m_Processor);
    }
    public IOgSlider<IOgVisualElement> Build(string name, DkObservable<float> observable, float value, float min, float max,
        IDkProcess<OgSliderBuildContext> process)
    {
        m_Processor.AddProcess(process);
        IOgSlider<IOgVisualElement> element = m_OgTextureBuilder.Build(new(name, value, observable, min, max));
        m_Processor.RemoveProcess(process);
        return element;
    }
}