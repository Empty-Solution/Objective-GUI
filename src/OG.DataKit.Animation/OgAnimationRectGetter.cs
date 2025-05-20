using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.DataKit.Animation;
public class OgAnimationRectGetter<TGetter>(TGetter originalGetter, IOgEventHandlerProvider provider)
    : OgAnimationGetter<TGetter, Rect>(originalGetter, provider) where TGetter : IDkGetProvider<Rect>
{
    protected override Rect InternalGet(Rect originalValue, Rect targetModifier, float time) =>
        new(originalValue.position + (targetModifier.position * time), originalValue.size + (targetModifier.size * time));
}