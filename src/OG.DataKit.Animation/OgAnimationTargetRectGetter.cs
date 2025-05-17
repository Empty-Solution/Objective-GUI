using DK.Getting.Abstraction.Generic;
using OG.Animator.Abstraction;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationTargetRectGetter(IDkGetProvider<Rect> originalGetter, IOgEventHandlerProvider provider, IOgAnimator<Rect> animator)
    : OgAnimationTargetGetter<Rect>(originalGetter, provider, animator)
{
    protected override Rect InternalGet(Rect originalValue, Rect animationValue) =>
        new(originalValue.position + animationValue.position, originalValue.size + animationValue.size);
}