using DK.Getting.Abstraction.Generic;
using System;
using System.Collections.Generic;
namespace OG.DataKit.Animation.Observer;
public class OgMultiplyAnimationScriptableObserver<TGetter, TValue, TObserverValue>(Func<OgAnimationGetter<TGetter, TValue>, TObserverValue, TValue> func)
    : OgAnimationObserver<TObserverValue> where TValue : notnull where TGetter : IDkGetProvider<TValue>
{
    private readonly HashSet<OgAnimationGetter<TGetter, TValue>> m_Getters = [];
    protected override void InternalUpdate(TObserverValue state)
    {
        foreach(OgAnimationGetter<TGetter, TValue> getter in m_Getters) getter.TargetModifier = func.Invoke(getter, state);
    }
    public void AddGetter(OgAnimationGetter<TGetter, TValue> getter) => m_Getters.Add(getter);
    public void RemoveGetter(OgAnimationGetter<TGetter, TValue> getter) => m_Getters.Remove(getter);
}