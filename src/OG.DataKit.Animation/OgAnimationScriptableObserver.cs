using DK.Observing.Abstraction.Generic;
using System;
namespace OG.DataKit.Animation;
public class OgAnimationScriptableObserver<TValue, TObserverValue>(OgAnimationGetter<TValue> getter, Func<TObserverValue, TValue> func)
    : IDkObserver<TObserverValue> where TValue : notnull
{
    public void Update(TObserverValue state) => getter.Target = func.Invoke(state);
    public void Update(object state)
    {
        if(state is TObserverValue casted) Update(casted);
    }
}