using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using System;
using System.Collections.Generic;
namespace OG.DataKit.Animation.Observer;
public class OgMultiplyAnimationScriptableObserver<TGetter, TValue, TObserverValue>(Func<OgAnimationGetter<TGetter, TValue>, TObserverValue, TValue> func)
    : IDkObserver<TObserverValue> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    private readonly HashSet<OgAnimationGetter<TGetter, TValue>> m_Getters = [];
    public void Update(TObserverValue state)
    {
        foreach(OgAnimationGetter<TGetter, TValue> getter in m_Getters) getter.TargetModifier = func.Invoke(getter, state);
    }
    public void Update(object state)
    {
        if(state is TObserverValue casted) Update(casted);
    }
    public void AddGetter(OgAnimationGetter<TGetter, TValue> getter) => m_Getters.Add(getter);
    public void RemoveGetter(OgAnimationGetter<TGetter, TValue> getter) => m_Getters.Remove(getter);
}