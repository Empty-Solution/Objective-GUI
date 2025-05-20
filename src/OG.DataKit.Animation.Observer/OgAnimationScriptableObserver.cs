using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using System;
namespace OG.DataKit.Animation.Observer;
public class OgAnimationScriptableObserver<TGetter, TValue, TObserverValue>(Func<OgAnimationGetter<TGetter, TValue>, TObserverValue, TValue> func)
    : IDkObserver<TObserverValue> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    public OgAnimationGetter<TGetter, TValue>? Getter { get; set; }
    public void Update(TObserverValue state) => Getter!.TargetModifier = func.Invoke(Getter, state);
    public void Update(object state)
    {
        if(state is TObserverValue casted) Update(casted);
    }
}