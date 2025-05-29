using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Interactive;
using OG.DataTypes.BindType;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Factory.Interactive;
using System.Collections.Generic;
using UnityEngine;
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
public class EhInternalBindableBuilder<TValue>
{
    private readonly OgBindableBuilder<TValue>                   m_OgToggleBuilder;
    private readonly DkProcessor<OgBindableBuildContext<TValue>> m_Processor;
    public EhInternalBindableBuilder()
    {
        m_Processor       = new();
        m_OgToggleBuilder = new(new OgBindableFactory<TValue>(), m_Processor);
    }
    public IOgInteractableValueElement<IOgVisualElement, TValue> Build(string name, IDkObservableProperty<TValue> value,
        IDkValueOverride<TValue> valueOverride, IDkProperty<SortedSet<KeyCode>> bind, IDkGetProvider<EOgBindType> bindTypeGetProvider,
        IDkProcess<OgBindableBuildContext<TValue>> process)
    {
        m_Processor.AddProcess(process);
        IOgInteractableValueElement<IOgVisualElement, TValue>
            element = m_OgToggleBuilder.Build(new(name, value, valueOverride, bind, bindTypeGetProvider));
        m_Processor.RemoveProcess(process);
        return element;
    }
}