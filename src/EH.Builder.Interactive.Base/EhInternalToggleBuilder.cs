using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
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
    public IOgToggle<IOgVisualElement> Build(string name, bool value, DkObservable<bool> observable, IDkProcess<OgToggleBuildContext> process)
    {
        m_Processor.AddProcess(process);
        IOgToggle<IOgVisualElement> element = m_OgToggleBuilder.Build(new(name, value, observable));
        m_Processor.RemoveProcess(process);
        return element;
    }
}