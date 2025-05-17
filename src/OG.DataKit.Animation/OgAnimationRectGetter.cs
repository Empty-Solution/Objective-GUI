using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationRectGetter(IDkGetProvider<Rect> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<Rect> animator,
    IOgEventCallback<IOgRenderEvent>? renderCallback = null) : OgAnimationStateGetter<Rect>(originalGetter, provider, animator, renderCallback)
{
    protected override Rect InternalGet(Rect originalValue, Rect animationValue) =>
        new(originalValue.position + animationValue.position, originalValue.size + animationValue.size);
}