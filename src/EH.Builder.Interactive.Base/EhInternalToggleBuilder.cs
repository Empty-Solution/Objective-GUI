using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Factory.Interactive;
namespace EH.Builder.Interactive.Base;
public class EhInternalToggleBuilder
{
    private readonly OgToggleBuilder                   m_OgToggleBuilder;
    private readonly DkProcessor<OgToggleBuildContext> m_Processor;
    public EhInternalToggleBuilder()
    {
        m_Processor       = new();
        m_OgToggleBuilder = new(new OgToggleFactory(), m_Processor);
    }
    public IOgToggle<IOgVisualElement> Build(string name, IDkObservableProperty<bool> value, IDkProcess<OgToggleBuildContext> process)
    {
        m_Processor.AddProcess(process);
        IOgToggle<IOgVisualElement> element = m_OgToggleBuilder.Build(new(name, value));
        m_Processor.RemoveProcess(process);
        return element;
    }
}