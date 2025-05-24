using DK.Observing.Abstraction.Generic;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhTabObserver() : IDkObserver<bool>
{
    private readonly        IOgContainer<IOgElement>? m_Source;
    private readonly        float m_ThumbHeight;
    private readonly        OgAnimationRectGetter<OgTransformerRectGetter>? m_SeparatorSelectorGetter;
    private static readonly List<EhTabObserver> observers = [];
    public                  IOgToggle<IOgVisualElement>? LinkedInteractable { get; set; }
    public                  OgTransformerRectGetter? RectGetter { get; set; }
    public                  IOgContainer<IOgElement>? Target { get; }
    public                  bool ShouldProcess { get; set; } = true; // сладенький костыль ибо я не хотел делать int currentTABBBBB
    public EhTabObserver(IOgContainer<IOgElement> source, IOgContainer<IOgElement> target, float thumbHeight,
        OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter) : this()
    {
        m_Source                  = source;
        m_ThumbHeight             = thumbHeight;
        m_SeparatorSelectorGetter = separatorSelectorGetter;
        Target                    = target;
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
        foreach(EhTabObserver observer in observers)
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
        Rect rect = m_SeparatorSelectorGetter!.TargetModifier;
        m_SeparatorSelectorGetter.SetTime();
        rect.y                                   = RectGetter!.Get().y;
        rect.height                              = state ? m_ThumbHeight : 0;
        m_SeparatorSelectorGetter.TargetModifier = rect;
    }
}