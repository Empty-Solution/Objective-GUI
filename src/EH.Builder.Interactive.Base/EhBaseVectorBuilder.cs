using DK.DataTypes;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Factory.Interactive;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhBaseVectorBuilder
{
    private readonly OgVectorBuilder                   m_OgVectorBuilder;
    private readonly DkProcessor<OgVectorBuildContext> m_Processor;
    public EhBaseVectorBuilder()
    {
        m_Processor       = new();
        m_OgVectorBuilder = new(new OgVectorFactory(), m_Processor);
    }
    public IOgVectorValueElement<IOgVisualElement> Build(string name, IDkObservableProperty<Vector2> value, Vector2 min, Vector2 max,
        IDkProcess<OgVectorBuildContext> process)
    {
        m_Processor.AddProcess(process);
        IOgVectorValueElement<IOgVisualElement> element = m_OgVectorBuilder.Build(new(name, value));
        element.Range = new DkRange<Vector2>(min, max);
        m_Processor.RemoveProcess(process);
        return element;
    }
}