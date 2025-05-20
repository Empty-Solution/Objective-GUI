using DK.Observing.Abstraction.Generic;
using System;
namespace OG.DataKit.Animation;
public class OgAnimationScriptableObserver<TValue, TObserverValue>(Func<TObserverValue, TValue> func) : IDkObserver<TObserverValue> where TValue : notnull
{
    public OgAnimationGetter<TValue>? Getter { get; set; }
    public void Update(TObserverValue state) => Getter!.Target = func.Invoke(state);
    public void Update(object state)
    {
        if(state is TObserverValue casted) Update(casted);
    }
}