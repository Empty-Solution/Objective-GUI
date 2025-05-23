using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Factory.Interactive;
namespace EH.Builder.Interactive.Base;
public class EhInternalDraggableBuilder
{
    private readonly OgDraggableBuilder                   m_OgDraggableBuilder;
    private readonly DkProcessor<OgDraggableBuildContext> m_Processor;
    public EhInternalDraggableBuilder()
    {
        m_Processor          = new();
        m_OgDraggableBuilder = new(new OgDraggableFactory(), m_Processor);
    }
    public IOgDraggableElement<IOgElement> Build(string name, IDkProcess<OgDraggableBuildContext>? process)
    {
        if(process is not null) m_Processor.AddProcess(process);
        IOgDraggableElement<IOgElement> element = m_OgDraggableBuilder.Build(new(name));
        if(process is not null) m_Processor.RemoveProcess(process);
        return element;
    }
}