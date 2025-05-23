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
public class EhTabObserver(List<EhTabObserver> observers, IOgContainer<IOgElement> source, IOgContainer<IOgElement> target, float thumbHeight,
    OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter) : IDkObserver<bool>
{
    public IOgToggle<IOgVisualElement>? LinkedInteractable { get; set; }
    public OgTransformerRectGetter?     RectGetter         { get; set; }
    public IOgContainer<IOgElement>     Target             { get; }      = target;
    public bool                         ShouldProcess      { get; set; } = true; // сладенький костыль ибо я не хотел делать int currentTABBBBB
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
        source.Clear();
        if(state) source.Add(Target);
        ShouldProcess = false;
        LinkedInteractable!.Value.Set(state);
        Rect rect = separatorSelectorGetter.TargetModifier;
        separatorSelectorGetter.SetTime();
        rect.y                                 = RectGetter!.Get().y;
        rect.height                            = state ? thumbHeight : 0;
        separatorSelectorGetter.TargetModifier = rect;
    }
}