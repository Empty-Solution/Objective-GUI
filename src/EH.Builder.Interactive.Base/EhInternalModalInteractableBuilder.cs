using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Factory.Interactive;
namespace EH.Builder.Interactive.Base;
public class EhInternalModalInteractableBuilder
{
    private readonly OgModalButtonBuilder                   m_ModalButtonBuilder;
    private readonly DkProcessor<OgModalButtonBuildContext> m_Processor;
    public EhInternalModalInteractableBuilder()
    {
        m_Processor          = new();
        m_ModalButtonBuilder = new(new OgModalButtonFactory(), m_Processor);
    }
    public IOgModalInteractable<IOgElement> Build(string name, bool rightClick, IDkProcess<OgModalButtonBuildContext>? process = null)
    {
        if(process != null) m_Processor.AddProcess(process);
        IOgModalInteractable<IOgElement> button = m_ModalButtonBuilder.Build(new(name, rightClick));
        if(process != null) m_Processor.RemoveProcess(process);
        return button;
    }
}