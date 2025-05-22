using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using OG.Builder;
using OG.Builder.Contexts;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Factory;
namespace EH.Builder;
public class EhContainerBuilder
{
    private readonly OgContainerBuilder                   m_OgContainerBuilder;
    private readonly DkProcessor<OgContainerBuildContext> m_Processor;
    public EhContainerBuilder()
    {
        m_Processor          = new();
        m_OgContainerBuilder = new(new OgContainerFactory<IOgElement>(), m_Processor);
    }
    public IOgContainer<IOgElement> Build(string name, IDkProcess<OgContainerBuildContext>? process)
    {
        if(process != null) m_Processor.AddProcess(process);
        IOgContainer<IOgElement> element = m_OgContainerBuilder.Build(new(name));
        if(process != null) m_Processor.RemoveProcess(process);
        return element;
    }
}