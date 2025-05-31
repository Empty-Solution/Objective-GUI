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
public class EhTabObserver(List<EhTabObserver> observers, IOgContainer<IOgElement> sourceTabContainer, IOgContainer<IOgElement> targetTabContainer,
    IOgContainer<IOgElement> sourceToolbarContainer, IOgContainer<IOgElement> targetToolbarContainer, float thumbSize,
    OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter) : IDkObserver<bool>
{
    public IOgToggle<IOgVisualElement>? LinkedInteractable { get; set; }
    public OgTransformerRectGetter?     RectGetter         { get; set; }
    public IOgContainer<IOgElement>?    TargetTabContainer { get; }      = targetTabContainer;
    public bool                         ShouldProcess      { get; set; } = true;
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
            if(observer.TargetTabContainer == TargetTabContainer) continue;
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
        sourceTabContainer.Clear();
        sourceToolbarContainer.Clear();
        if(state)
        {
            sourceTabContainer.Add(TargetTabContainer!);
            sourceToolbarContainer.Add(targetToolbarContainer);
        }
        ShouldProcess = false;
        LinkedInteractable!.Value.Set(state);
        separatorSelectorGetter!.SetTime();
        separatorSelectorGetter.TargetModifier = GetRect(separatorSelectorGetter.TargetModifier, state, thumbSize);
    }
    private Rect GetRect(Rect rect, bool state, float size)
    {
        rect.y      = RectGetter!.Get().y;
        rect.height = state ? size : 0;
        return rect;
    }
}