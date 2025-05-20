using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
namespace OG.DataKit.Animation;
public class OgAnimationObserver<TGetter, TValue>(OgAnimationGetter<TGetter, TValue> getter, TValue offTarget, TValue onTarget)
    : IDkObserver<bool> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    public void Update(bool state) => getter.TargetModifier = state ? onTarget : offTarget;
    public void Update(object state)
    {
        if(state is bool casted) Update(casted);
    }
}