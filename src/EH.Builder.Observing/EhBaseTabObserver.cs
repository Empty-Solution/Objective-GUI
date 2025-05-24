using DK.Observing.Abstraction.Generic;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Observing;
public abstract class EhBaseTabObserver() : IDkObserver<bool>
{
    private readonly        IOgContainer<IOgElement>? m_Source;
    private readonly        float m_ThumbSize;
    private readonly        OgAnimationRectGetter<OgTransformerRectGetter>? m_SeparatorSelectorGetter;
    private readonly List<EhBaseTabObserver> m_Observers = [];
    public                  IOgToggle<IOgVisualElement>? LinkedInteractable { get; set; }
    public                  OgTransformerRectGetter? RectGetter { get; set; }
    public                  IOgContainer<IOgElement>? Target { get; }
    public                  bool ShouldProcess { get; set; } = true; // сладенький костыль ибо я не хотел делать int currentTABBBBB
    public EhBaseTabObserver(List<EhBaseTabObserver> observers, IOgContainer<IOgElement> source, IOgContainer<IOgElement> target, float thumbSize,
        OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter) : this()
    {
        m_Source                  = source;
        m_ThumbSize               = thumbSize;
        m_SeparatorSelectorGetter = separatorSelectorGetter;
        Target                    = target;
        m_Observers               = observers;
        observers.Add(this);
    }
    public void Update(bool state)
    {
        if(!ShouldProcess)
        {
            ShouldProcess = true;
            return;
        }
        InternalUpdate(state);
        if(!state) return;
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach(EhBaseTabObserver observer in m_Observers)
        {
            if(observer.Target == Target) continue;
            observer.ShouldProcess = false;
            observer.InternalUpdate(false);
        }
    }
    public void Update(object state)
    {
        if(state is bool value) Update(value);
    }
    private void InternalUpdate(bool state)
    {
        if(!ShouldProcess)
        {
            LinkedInteractable!.Value.Set(state);
            return;
        }
        m_Source!.Clear();
        if(state) m_Source.Add(Target!);
        ShouldProcess = false;
        LinkedInteractable!.Value.Set(state);
        m_SeparatorSelectorGetter!.SetTime();
        m_SeparatorSelectorGetter.TargetModifier = GetRect(m_SeparatorSelectorGetter.TargetModifier, state, m_ThumbSize);
    }

    protected abstract Rect GetRect(Rect rect, bool state, float size);
}