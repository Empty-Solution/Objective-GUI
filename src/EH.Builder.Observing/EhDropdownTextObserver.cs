using DK.Binding.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using OG.DataKit.Animation;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Observing;
public class EhDropdownTextObserver(List<EhDropdownTextObserver> observers, int index, IDkGetProvider<Color> color, IDkGetProvider<Color> selectedColor,
    OgAnimationColorGetter getter, IDkBinding binding, IOgModalInteractable<IOgElement> interactable) : IDkObserver<bool>
{
    public void Update(bool state)
    {
        if(!state)
        {
            InternalUpdate(false);
            return;
        }
        for(int i = 0; i < observers.Count; i++) observers[i].InternalUpdate(i == index);
        binding.Sync();
        interactable.ShouldProcess = false;
        interactable.IsInteractingObserver?.Notify(false);
    }
    public void Update(object state)
    {
        if(state is bool value) Update(value);
    }
    private void InternalUpdate(bool state) => getter.TargetModifier = state ? selectedColor.Get() : color.Get();
}