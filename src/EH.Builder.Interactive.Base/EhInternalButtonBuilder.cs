using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Factory.Interactive;
namespace EH.Builder.Interactive.Base;
public class EhInternalButtonBuilder
{
    private readonly OgButtonBuilder                   m_OgToggleBuilder;
    private readonly DkProcessor<OgButtonBuildContext> m_Processor;
    public EhInternalButtonBuilder()
    {
        m_Processor       = new();
        m_OgToggleBuilder = new(new OgButtonFactory(), m_Processor);
    }
    public IOgInteractableElement<IOgVisualElement> Build(string name, IDkProcess<OgButtonBuildContext> process)
    {
        m_Processor.AddProcess(process);
        IOgInteractableElement<IOgVisualElement> element = m_OgToggleBuilder.Build(new(name));
        m_Processor.RemoveProcess(process);
        return element;
    }
}