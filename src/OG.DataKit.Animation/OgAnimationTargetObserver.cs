using DK.Observing.Abstraction.Generic;
namespace OG.DataKit.Animation;
public class OgAnimationTargetObserver<TValue>(OgAnimationTargetGetter<TValue> getter, TValue offTarget, TValue onTarget) : IDkObserver<bool> where TValue : notnull
{
    public void Update(bool state) => getter.Target = state ? onTarget : offTarget;
    public void Update(object state)
    {
        if(state is bool casted) Update(casted);
    }
}