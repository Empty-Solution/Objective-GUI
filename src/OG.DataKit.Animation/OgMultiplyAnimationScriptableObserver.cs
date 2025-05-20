using DK.Observing.Abstraction.Generic;
using System;
using System.Collections.Generic;
namespace OG.DataKit.Animation;
public class OgMultiplyAnimationScriptableObserver<TValue, TObserverValue>(Func<TObserverValue, TValue> func)
    : IDkObserver<TObserverValue> where TValue : notnull
{
    private readonly HashSet<OgAnimationGetter<TValue>> m_Getters = [];
    public void Update(TObserverValue state)
    {
        foreach(OgAnimationGetter<TValue> getter in m_Getters) getter.Target = func.Invoke(state);
    }
    public void Update(object state)
    {
        if(state is TObserverValue casted) Update(casted);
    }
    public void AddGetter(OgAnimationGetter<TValue> getter) => m_Getters.Add(getter);
    public void RemoveGetter(OgAnimationGetter<TValue> getter) => m_Getters.Remove(getter);
}